using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using EWalletApp.Models;

namespace EWalletApp.Database
{
    public static class DatabaseHelper
    {
        private static string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "app.db");
        private static string connectionString = $"Data Source={dbPath}";

        public static void InitializeDatabase()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var createUsersTableCmd = connection.CreateCommand();
                createUsersTableCmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Users (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL DEFAULT '',
                        MobileNumber TEXT UNIQUE NOT NULL,
                        Password TEXT NOT NULL,
                        Balance DECIMAL NOT NULL DEFAULT 0
                    );
                ";
                createUsersTableCmd.ExecuteNonQuery();

                var createTransactionsTableCmd = connection.CreateCommand();
                createTransactionsTableCmd.CommandText = @"
                    CREATE TABLE IF NOT EXISTS Transactions (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        UserId INTEGER NOT NULL,
                        Type TEXT NOT NULL,
                        Amount DECIMAL NOT NULL,
                        Timestamp DATETIME NOT NULL,
                        Details TEXT NOT NULL,
                        FOREIGN KEY(UserId) REFERENCES Users(Id)
                    );
                ";
                createTransactionsTableCmd.ExecuteNonQuery();

                // Database migration for existing tables to add Name column
                try
                {
                    var alterTableCmd = connection.CreateCommand();
                    alterTableCmd.CommandText = "ALTER TABLE Users ADD COLUMN Name TEXT NOT NULL DEFAULT '';";
                    alterTableCmd.ExecuteNonQuery();
                }
                catch (SqliteException)
                {
                    // Ignore exception if column already exists
                }
            }
        }

        public static bool RegisterUser(string name, string mobileNumber, string password)
        {
            try
            {
                using (var connection = new SqliteConnection(connectionString))
                {
                    connection.Open();
                    var cmd = connection.CreateCommand();
                    cmd.CommandText = "INSERT INTO Users (Name, MobileNumber, Password, Balance) VALUES (@name, @mobile, @pwd, 0)";
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@mobile", mobileNumber);
                    cmd.Parameters.AddWithValue("@pwd", password);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqliteException)
            {
                // Most likely unique constraint violation
                return false;
            }
        }

        public static User Login(string mobileNumber, string password)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT Id, Name, MobileNumber, Password, Balance FROM Users WHERE MobileNumber = @mobile AND Password = @pwd";
                cmd.Parameters.AddWithValue("@mobile", mobileNumber);
                cmd.Parameters.AddWithValue("@pwd", password);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            MobileNumber = reader.GetString(2),
                            Password = reader.GetString(3),
                            Balance = reader.GetDecimal(4)
                        };
                    }
                }
            }
            return null;
        }

        public static User GetUserByMobile(string mobileNumber)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT Id, Name, MobileNumber, Password, Balance FROM Users WHERE MobileNumber = @mobile";
                cmd.Parameters.AddWithValue("@mobile", mobileNumber);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            MobileNumber = reader.GetString(2),
                            Password = reader.GetString(3),
                            Balance = reader.GetDecimal(4)
                        };
                    }
                }
            }
            return null;
        }

        public static User GetUserById(int id)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT Id, Name, MobileNumber, Password, Balance FROM Users WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new User
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            MobileNumber = reader.GetString(2),
                            Password = reader.GetString(3),
                            Balance = reader.GetDecimal(4)
                        };
                    }
                }
            }
            return null;
        }

        public static void DepositCash(int userId, decimal amount)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var updateCmd = connection.CreateCommand();
                        updateCmd.CommandText = "UPDATE Users SET Balance = Balance + @amount WHERE Id = @userId";
                        updateCmd.Parameters.AddWithValue("@amount", amount);
                        updateCmd.Parameters.AddWithValue("@userId", userId);
                        updateCmd.ExecuteNonQuery();

                        var insertCmd = connection.CreateCommand();
                        insertCmd.CommandText = "INSERT INTO Transactions (UserId, Type, Amount, Timestamp, Details) VALUES (@userId, 'Deposit', @amount, @timestamp, 'Self Deposit')";
                        insertCmd.Parameters.AddWithValue("@userId", userId);
                        insertCmd.Parameters.AddWithValue("@amount", amount);
                        insertCmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                        insertCmd.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static bool SendCash(int senderId, string recipientMobile, decimal amount)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                
                // Get sender to check balance
                var sender = GetUserById(senderId);
                if (sender == null || sender.Balance < amount)
                    return false;

                // Get recipient
                var recipient = GetUserByMobile(recipientMobile);
                if (recipient == null || recipient.Id == senderId)
                    return false; // Can't send to self or non-existent

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Deduct from sender
                        var updateSenderCmd = connection.CreateCommand();
                        updateSenderCmd.CommandText = "UPDATE Users SET Balance = Balance - @amount WHERE Id = @senderId";
                        updateSenderCmd.Parameters.AddWithValue("@amount", amount);
                        updateSenderCmd.Parameters.AddWithValue("@senderId", senderId);
                        updateSenderCmd.ExecuteNonQuery();

                        // Add to recipient
                        var updateRecipientCmd = connection.CreateCommand();
                        updateRecipientCmd.CommandText = "UPDATE Users SET Balance = Balance + @amount WHERE Id = @recipientId";
                        updateRecipientCmd.Parameters.AddWithValue("@amount", amount);
                        updateRecipientCmd.Parameters.AddWithValue("@recipientId", recipient.Id);
                        updateRecipientCmd.ExecuteNonQuery();

                        // Sender transaction record
                        var insertSenderTx = connection.CreateCommand();
                        insertSenderTx.CommandText = "INSERT INTO Transactions (UserId, Type, Amount, Timestamp, Details) VALUES (@userId, 'Send', @amount, @timestamp, @details)";
                        insertSenderTx.Parameters.AddWithValue("@userId", senderId);
                        insertSenderTx.Parameters.AddWithValue("@amount", amount);
                        insertSenderTx.Parameters.AddWithValue("@timestamp", DateTime.Now);
                        insertSenderTx.Parameters.AddWithValue("@details", $"To {recipient.MobileNumber}");
                        insertSenderTx.ExecuteNonQuery();

                        // Recipient transaction record
                        var insertRecipientTx = connection.CreateCommand();
                        insertRecipientTx.CommandText = "INSERT INTO Transactions (UserId, Type, Amount, Timestamp, Details) VALUES (@userId, 'Receive', @amount, @timestamp, @details)";
                        insertRecipientTx.Parameters.AddWithValue("@userId", recipient.Id);
                        insertRecipientTx.Parameters.AddWithValue("@amount", amount);
                        insertRecipientTx.Parameters.AddWithValue("@timestamp", DateTime.Now);
                        insertRecipientTx.Parameters.AddWithValue("@details", $"From {sender.MobileNumber}");
                        insertRecipientTx.ExecuteNonQuery();

                        transaction.Commit();
                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public static List<Transaction> GetTransactions(int userId)
        {
            var list = new List<Transaction>();
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT Id, UserId, Type, Amount, Timestamp, Details FROM Transactions WHERE UserId = @userId ORDER BY Timestamp DESC";
                cmd.Parameters.AddWithValue("@userId", userId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Transaction
                        {
                            Id = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            Type = reader.GetString(2),
                            Amount = reader.GetDecimal(3),
                            Timestamp = reader.GetDateTime(4),
                            Details = reader.GetString(5)
                        });
                    }
                }
            }
            return list;
        }
    }
}
