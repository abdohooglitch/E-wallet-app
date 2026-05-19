using System;
using System.Drawing;
using System.Windows.Forms;
using EWalletApp.Database;
using EWalletApp.UI;

namespace EWalletApp.Forms
{
    public class TransactionHistoryForm : Form
    {
        private int currentUserId;
        private DataGridView dgvTransactions;
        private Button btnBack;
        private Label lblTitle;

        public TransactionHistoryForm(int userId)
        {
            currentUserId = userId;
            InitializeComponents();
            LoadTransactions();
        }

        private void InitializeComponents()
        {
            this.Text = "E-Wallet App - Transaction History";
            this.Size = new Size(620, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            UiTheme.ApplyForm(this);

            lblTitle = new Label
            {
                Text = "Your Transaction History",
                Location = new Point(25, 25),
                AutoSize = true
            };
            UiTheme.StyleTitle(lblTitle);

            dgvTransactions = new DataGridView
            {
                Location = new Point(25, 70),
                Size = new Size(555, 300),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                RowHeadersVisible = false
            };
            UiTheme.StyleDataGrid(dgvTransactions);

            btnBack = new Button { Text = "Back to Dashboard", Location = new Point(195, 390), Width = 220, Height = 42 };
            btnBack.Click += BtnBack_Click;
            UiTheme.StyleSecondaryButton(btnBack);

            this.Controls.Add(lblTitle);
            this.Controls.Add(dgvTransactions);
            this.Controls.Add(btnBack);
        }

        private void LoadTransactions()
        {
            var transactions = DatabaseHelper.GetTransactions(currentUserId);
            dgvTransactions.DataSource = transactions;
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
