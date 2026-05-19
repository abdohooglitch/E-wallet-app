using System;
using System.Drawing;
using System.Windows.Forms;
using EWalletApp.Models;
using EWalletApp.Database;
using EWalletApp.UI;

namespace EWalletApp.Forms
{
    // Form 3: User Account Balance Form
    // This form shows the current balance and provides access to transactions.
    public class BalanceForm : Form
    {
        private int currentUserId;
        private Label lblWelcome;
        private Label lblBalanceText;
        private Label lblBalanceAmount;
        private Button btnTransaction;
        private Button btnTransactionHistory;
        private Button btnRefresh;
        private Button btnLogout;

        public BalanceForm(int userId)
        {
            currentUserId = userId;
            InitializeComponents();
            LoadUserData();
        }

        // Initialize UI Components
        private void InitializeComponents()
        {
            this.Text = "E-Wallet App - Account Balance";
            this.Size = new Size(420, 430);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            UiTheme.ApplyForm(this);

            lblWelcome = new Label
            {
                Text = "Welcome, User!",
                Location = new Point(40, 30),
                AutoSize = true
            };
            UiTheme.StyleTitle(lblWelcome);
            lblWelcome.Font = UiTheme.SubtitleFont;

            lblBalanceText = new Label
            {
                Text = "Current Balance",
                Location = new Point(40, 85),
                AutoSize = true
            };
            UiTheme.StyleLabel(lblBalanceText);

            lblBalanceAmount = new Label
            {
                Text = "$0.00",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = UiTheme.BalanceColor,
                Location = new Point(40, 115),
                AutoSize = true
            };

            btnTransaction = new Button { Text = "Send / Receive Money", Location = new Point(95, 185), Width = 220, Height = 42 };
            btnTransaction.Click += BtnTransaction_Click;
            UiTheme.StylePrimaryButton(btnTransaction);

            btnTransactionHistory = new Button { Text = "Transaction History", Location = new Point(95, 238), Width = 220, Height = 42 };
            btnTransactionHistory.Click += BtnTransactionHistory_Click;
            UiTheme.StyleAccentButton(btnTransactionHistory);

            btnRefresh = new Button { Text = "Refresh Balance", Location = new Point(95, 291), Width = 220, Height = 36 };
            btnRefresh.Click += BtnRefresh_Click;
            UiTheme.StyleSecondaryButton(btnRefresh);

            btnLogout = new Button { Text = "Logout", Location = new Point(95, 338), Width = 220, Height = 36 };
            btnLogout.Click += BtnLogout_Click;
            UiTheme.StyleDangerButton(btnLogout);

            // Add controls to form
            this.Controls.Add(lblWelcome);
            this.Controls.Add(lblBalanceText);
            this.Controls.Add(lblBalanceAmount);
            this.Controls.Add(btnTransaction);
            this.Controls.Add(btnTransactionHistory);
            this.Controls.Add(btnRefresh);
            this.Controls.Add(btnLogout);
        }

        // Function to load the user's latest balance from the database
        private void LoadUserData()
        {
            User user = DatabaseHelper.GetUserById(currentUserId);
            if (user != null)
            {
                lblWelcome.Text = $"Welcome, {user.Name}";
                lblBalanceAmount.Text = $"${user.Balance:0.00}";
            }
        }

        // Event handler for Transaction button
        private void BtnTransaction_Click(object sender, EventArgs e)
        {
            // Open the Send / Receive Money Form
            TransactionForm transactionForm = new TransactionForm(currentUserId);
            this.Hide();
            transactionForm.ShowDialog();
            this.Show(); // Show balance form again when transaction form is closed
            
            // Refresh balance after returning
            LoadUserData();
        }

        // Event handler for Refresh button
        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadUserData();
            MessageBox.Show("Balance updated.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Event handler for Transaction History button
        private void BtnTransactionHistory_Click(object sender, EventArgs e)
        {
            TransactionHistoryForm historyForm = new TransactionHistoryForm(currentUserId);
            this.Hide();
            historyForm.ShowDialog();
            this.Show();
        }

        // Event handler for Logout button
        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close(); // Closes this form and returns to AuthForm
        }
    }
}
