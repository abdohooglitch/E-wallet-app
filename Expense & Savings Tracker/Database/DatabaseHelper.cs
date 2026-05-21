using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Data.Sqlite;
using ExpenseSavingsTracker.Models;

namespace ExpenseSavingsTracker.Database
{
    /// <summary>
    /// Central data-access layer: SQLite connection, schema setup, and CRUD for users, expenses, and savings.
    /// </summary>
    public static class DatabaseHelper
    {
        // Database file stored next to the executable
        private static readonly string DbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "finance.db");
        private static readonly string ConnectionString = $"Data Source={DbPath}";

        /// <summary>Predefined expense categories shown in the Add Expense dropdown.</summary>
        public static readonly string[] ExpenseCategories =
        {
            "Food & Dining",
            "Transport",
            "Bills & Utilities",
            "Shopping",
            "Entertainment",
            "Health",
            "Education",
            "Other"
        };

        /// <summary>
        /// Creates Users, Expenses, and SavingGoals tables if they do not exist, then runs legacy migrations.
        /// </summary>
        public static void InitializeDatabase()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            // User accounts with mobile login and optional monthly budget
            ExecuteNonQuery(connection, @"
                CREATE TABLE IF NOT EXISTS Users (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL DEFAULT '',
                    MobileNumber TEXT UNIQUE NOT NULL,
                    Password TEXT NOT NULL,
                    MonthlyBudget DECIMAL NOT NULL DEFAULT 0
                );");

            // Individual expense records linked to a user
            ExecuteNonQuery(connection, @"
                CREATE TABLE IF NOT EXISTS Expenses (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER NOT NULL,
                    Category TEXT NOT NULL,
                    Amount DECIMAL NOT NULL,
                    ExpenseDate TEXT NOT NULL,
                    Note TEXT NOT NULL DEFAULT '',
                    FOREIGN KEY(UserId) REFERENCES Users(Id)
                );");

            // Savings goals with target and current saved amounts
            ExecuteNonQuery(connection, @"
                CREATE TABLE IF NOT EXISTS SavingGoals (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    UserId INTEGER NOT NULL,
                    Title TEXT NOT NULL,
                    TargetAmount DECIMAL NOT NULL,
                    SavedAmount DECIMAL NOT NULL DEFAULT 0,
                    CreatedAt TEXT NOT NULL,
                    FOREIGN KEY(UserId) REFERENCES Users(Id)
                );");

            MigrateLegacyUsersTable(connection);
        }

        /// <summary>
        /// Adds missing columns to older database files without failing if columns already exist.
        /// </summary>
        private static void MigrateLegacyUsersTable(SqliteConnection connection)
        {
            try
            {
                ExecuteNonQuery(connection, "ALTER TABLE Users ADD COLUMN MonthlyBudget DECIMAL NOT NULL DEFAULT 0;");
            }
            catch (SqliteException) { }

            try
            {
                ExecuteNonQuery(connection, "ALTER TABLE Users ADD COLUMN Name TEXT NOT NULL DEFAULT '';");
            }
            catch (SqliteException) { }
        }

        /// <summary>Runs a SQL command that does not return rows (CREATE, INSERT, UPDATE, etc.).</summary>
        private static void ExecuteNonQuery(SqliteConnection connection, string sql)
        {
            using var cmd = connection.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Inserts a new user. Returns false if mobile number is already registered (unique constraint).
        /// </summary>
        public static bool RegisterUser(string name, string mobileNumber, string password)
        {
            try
            {
                using var connection = new SqliteConnection(ConnectionString);
                connection.Open();
                using var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Users (Name, MobileNumber, Password, MonthlyBudget) VALUES (@name, @mobile, @pwd, 0)";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@mobile", mobileNumber);
                cmd.Parameters.AddWithValue("@pwd", password);
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (SqliteException)
            {
                return false;
            }
        }

        /// <summary>
        /// Validates credentials and returns the matching user, or null if login fails.
        /// </summary>
        public static User? Login(string mobileNumber, string password)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, MobileNumber, Password, MonthlyBudget FROM Users WHERE MobileNumber = @mobile AND Password = @pwd";
            cmd.Parameters.AddWithValue("@mobile", mobileNumber);
            cmd.Parameters.AddWithValue("@pwd", password);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapUser(reader) : null;
        }

        /// <summary>Looks up a user by mobile number only (used before login to check registration).</summary>
        public static User? GetUserByMobile(string mobileNumber)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, MobileNumber, Password, MonthlyBudget FROM Users WHERE MobileNumber = @mobile";
            cmd.Parameters.AddWithValue("@mobile", mobileNumber);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapUser(reader) : null;
        }

        /// <summary>Loads a single user record by primary key.</summary>
        public static User? GetUserById(int id)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT Id, Name, MobileNumber, Password, MonthlyBudget FROM Users WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);

            using var reader = cmd.ExecuteReader();
            return reader.Read() ? MapUser(reader) : null;
        }

        /// <summary>Updates the user's monthly spending budget on the dashboard.</summary>
        public static void UpdateMonthlyBudget(int userId, decimal budget)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE Users SET MonthlyBudget = @budget WHERE Id = @id";
            cmd.Parameters.AddWithValue("@budget", budget);
            cmd.Parameters.AddWithValue("@id", userId);
            cmd.ExecuteNonQuery();
        }

        /// <summary>Inserts a new expense row for the given user.</summary>
        public static void AddExpense(int userId, string category, decimal amount, DateTime date, string note)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO Expenses (UserId, Category, Amount, ExpenseDate, Note)
                                VALUES (@userId, @category, @amount, @date, @note)";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@category", category);
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@date", date.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@note", note);
            cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Returns expenses for a user, optionally filtered by year and month (newest first).
        /// </summary>
        public static List<Expense> GetExpenses(int userId, int? year = null, int? month = null)
        {
            var list = new List<Expense>();
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();

            var sql = "SELECT Id, UserId, Category, Amount, ExpenseDate, Note FROM Expenses WHERE UserId = @userId";
            // Optional filter for expense history and reports
            if (year.HasValue && month.HasValue)
            {
                sql += " AND strftime('%Y', ExpenseDate) = @year AND strftime('%m', ExpenseDate) = @month";
                cmd.Parameters.AddWithValue("@year", year.Value.ToString());
                cmd.Parameters.AddWithValue("@month", month.Value.ToString("D2"));
            }
            sql += " ORDER BY ExpenseDate DESC, Id DESC";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@userId", userId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Expense
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    Category = reader.GetString(2),
                    Amount = reader.GetDecimal(3),
                    ExpenseDate = DateTime.Parse(reader.GetString(4)),
                    Note = reader.GetString(5)
                });
            }
            return list;
        }

        /// <summary>Sum of all expense amounts for the given user in a specific month.</summary>
        public static decimal GetMonthlyExpenses(int userId, int year, int month)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT COALESCE(SUM(Amount), 0) FROM Expenses
                                WHERE UserId = @userId
                                AND strftime('%Y', ExpenseDate) = @year
                                AND strftime('%m', ExpenseDate) = @month";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@year", year.ToString());
            cmd.Parameters.AddWithValue("@month", month.ToString("D2"));
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        /// <summary>
        /// Groups expenses by category for a month and calculates each category's share of total spending.
        /// </summary>
        public static List<CategoryReport> GetExpensesByCategory(int userId, int year, int month)
        {
            var list = new List<CategoryReport>();
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT Category, SUM(Amount) as Total FROM Expenses
                                WHERE UserId = @userId
                                AND strftime('%Y', ExpenseDate) = @year
                                AND strftime('%m', ExpenseDate) = @month
                                GROUP BY Category ORDER BY Total DESC";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@year", year.ToString());
            cmd.Parameters.AddWithValue("@month", month.ToString("D2"));

            decimal grandTotal = 0;
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var total = reader.GetDecimal(1);
                grandTotal += total;
                list.Add(new CategoryReport { Category = reader.GetString(0), Total = total });
            }

            // Compute percentage of monthly total for each category
            foreach (var item in list)
            {
                item.Percentage = grandTotal <= 0 ? 0 : Math.Round((item.Total / grandTotal) * 100, 1);
            }
            return list;
        }

        /// <summary>Creates a new savings goal with zero saved amount. Returns false if input is invalid.</summary>
        public static bool CreateSavingGoal(int userId, string title, decimal targetAmount)
        {
            if (string.IsNullOrWhiteSpace(title) || targetAmount <= 0) return false;

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"INSERT INTO SavingGoals (UserId, Title, TargetAmount, SavedAmount, CreatedAt)
                                VALUES (@userId, @title, @target, 0, @created)";
            cmd.Parameters.AddWithValue("@userId", userId);
            cmd.Parameters.AddWithValue("@title", title.Trim());
            cmd.Parameters.AddWithValue("@target", targetAmount);
            cmd.Parameters.AddWithValue("@created", DateTime.Now.ToString("o"));
            cmd.ExecuteNonQuery();
            return true;
        }

        /// <summary>Returns all savings goals for a user, newest first.</summary>
        public static List<SavingGoal> GetSavingGoals(int userId)
        {
            var list = new List<SavingGoal>();
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT Id, UserId, Title, TargetAmount, SavedAmount, CreatedAt
                                FROM SavingGoals WHERE UserId = @userId ORDER BY CreatedAt DESC";
            cmd.Parameters.AddWithValue("@userId", userId);

            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new SavingGoal
                {
                    Id = reader.GetInt32(0),
                    UserId = reader.GetInt32(1),
                    Title = reader.GetString(2),
                    TargetAmount = reader.GetDecimal(3),
                    SavedAmount = reader.GetDecimal(4),
                    CreatedAt = DateTime.Parse(reader.GetString(5))
                });
            }
            return list;
        }

        /// <summary>Loads one savings goal by its Id.</summary>
        public static SavingGoal? GetSavingGoalById(int goalId)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT Id, UserId, Title, TargetAmount, SavedAmount, CreatedAt
                                FROM SavingGoals WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", goalId);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) return null;

            return new SavingGoal
            {
                Id = reader.GetInt32(0),
                UserId = reader.GetInt32(1),
                Title = reader.GetString(2),
                TargetAmount = reader.GetDecimal(3),
                SavedAmount = reader.GetDecimal(4),
                CreatedAt = DateTime.Parse(reader.GetString(5))
            };
        }

        /// <summary>
        /// Adds money to a goal; saved amount is capped at the target so the goal cannot exceed 100%.
        /// </summary>
        public static bool DepositToGoal(int goalId, decimal amount)
        {
            if (amount <= 0) return false;
            var goal = GetSavingGoalById(goalId);
            if (goal == null) return false;

            decimal newSaved = goal.SavedAmount + amount;
            if (newSaved > goal.TargetAmount)
                newSaved = goal.TargetAmount;

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE SavingGoals SET SavedAmount = @saved WHERE Id = @id";
            cmd.Parameters.AddWithValue("@saved", newSaved);
            cmd.Parameters.AddWithValue("@id", goalId);
            cmd.ExecuteNonQuery();
            return true;
        }

        /// <summary>
        /// Removes money from a goal only if the saved balance is sufficient.
        /// </summary>
        public static bool WithdrawFromGoal(int goalId, decimal amount)
        {
            if (amount <= 0) return false;
            var goal = GetSavingGoalById(goalId);
            if (goal == null || goal.SavedAmount < amount) return false;

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE SavingGoals SET SavedAmount = SavedAmount - @amount WHERE Id = @id";
            cmd.Parameters.AddWithValue("@amount", amount);
            cmd.Parameters.AddWithValue("@id", goalId);
            cmd.ExecuteNonQuery();
            return true;
        }

        /// <summary>Sum of SavedAmount across all goals for the user (dashboard total savings).</summary>
        public static decimal GetTotalSavings(int userId)
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();
            using var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT COALESCE(SUM(SavedAmount), 0) FROM SavingGoals WHERE UserId = @userId";
            cmd.Parameters.AddWithValue("@userId", userId);
            return Convert.ToDecimal(cmd.ExecuteScalar());
        }

        /// <summary>
        /// Builds dashboard figures: name, budget, current month expenses, and total savings.
        /// </summary>
        public static DashboardSummary GetDashboardSummary(int userId)
        {
            var user = GetUserById(userId);
            var now = DateTime.Now;

            return new DashboardSummary
            {
                UserName = user?.Name ?? "User",
                MonthlyBudget = user?.MonthlyBudget ?? 0,
                MonthlyExpenses = GetMonthlyExpenses(userId, now.Year, now.Month),
                TotalSavings = GetTotalSavings(userId)
            };
        }

        /// <summary>Maps a SQLite data reader row to a User model instance.</summary>
        private static User MapUser(SqliteDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                MobileNumber = reader.GetString(2),
                Password = reader.GetString(3),
                MonthlyBudget = reader.GetDecimal(4)
            };
        }
    }
}
