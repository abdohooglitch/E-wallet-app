using System;
using System.Drawing;
using System.Windows.Forms;
using EWalletApp.Database;
using EWalletApp.UI;

namespace EWalletApp.Forms
{
    // Form 2: Send and Receive Money Form
    // This form handles money transfers and self deposits.
    public class TransactionForm : Form
    {
        private int currentUserId;
        private TabControl tabControl;
        
        // Send Money Tab Components
        private TabPage tabSend;
        private Label lblRecipientMobile;
        private TextBox txtRecipientMobile;
        private Label lblSendAmount;
        private TextBox txtSendAmount;
        private Button btnSend;

        // Receive Money (Deposit) Tab Components
        private TabPage tabReceive;
        private Label lblDepositAmount;
        private TextBox txtDepositAmount;
        private Button btnDeposit;
        
        private Button btnBack;

        public TransactionForm(int userId)
        {
            currentUserId = userId;
            InitializeComponents();
        }

        // Initialize UI Components
        private void InitializeComponents()
        {
            this.Text = "E-Wallet App - Send & Receive";
            this.Size = new Size(420, 380);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            UiTheme.ApplyForm(this);

            tabControl = new TabControl { Location = new Point(25, 25), Size = new Size(355, 235), Font = UiTheme.BodyFont };

            // Send Money Tab
            tabSend = new TabPage("Send Money") { BackColor = Color.White };
            
            lblRecipientMobile = new Label { Text = "Recipient Mobile:", Location = new Point(20, 25), AutoSize = true };
            txtRecipientMobile = new TextBox { Location = new Point(20, 48), Width = 295 };
            
            lblSendAmount = new Label { Text = "Amount to Send ($):", Location = new Point(20, 90), AutoSize = true };
            txtSendAmount = new TextBox { Location = new Point(20, 113), Width = 295 };
            
            btnSend = new Button { Text = "Send Money", Location = new Point(20, 158), Width = 295, Height = 42 };
            btnSend.Click += BtnSend_Click;
            UiTheme.StylePrimaryButton(btnSend);
            UiTheme.StyleLabel(lblRecipientMobile);
            UiTheme.StyleLabel(lblSendAmount);
            UiTheme.StyleTextBox(txtRecipientMobile);
            UiTheme.StyleTextBox(txtSendAmount);

            tabSend.Controls.Add(lblRecipientMobile);
            tabSend.Controls.Add(txtRecipientMobile);
            tabSend.Controls.Add(lblSendAmount);
            tabSend.Controls.Add(txtSendAmount);
            tabSend.Controls.Add(btnSend);

            // Receive Money Tab (Deposit)
            tabReceive = new TabPage("Receive (Deposit)") { BackColor = Color.White };
            
            lblDepositAmount = new Label { Text = "Amount to Deposit ($):", Location = new Point(20, 45), AutoSize = true };
            txtDepositAmount = new TextBox { Location = new Point(20, 68), Width = 295 };
            
            btnDeposit = new Button { Text = "Deposit Money", Location = new Point(20, 120), Width = 295, Height = 42 };
            btnDeposit.Click += BtnDeposit_Click;
            UiTheme.StyleSuccessButton(btnDeposit);
            UiTheme.StyleLabel(lblDepositAmount);
            UiTheme.StyleTextBox(txtDepositAmount);

            tabReceive.Controls.Add(lblDepositAmount);
            tabReceive.Controls.Add(txtDepositAmount);
            tabReceive.Controls.Add(btnDeposit);

            // Back Button
            btnBack = new Button { Text = "Go Back to Balance", Location = new Point(95, 285), Width = 220, Height = 36 };
            btnBack.Click += (sender, e) => { this.Close(); };
            UiTheme.StyleSecondaryButton(btnBack);

            tabControl.TabPages.Add(tabSend);
            tabControl.TabPages.Add(tabReceive);

            this.Controls.Add(tabControl);
            this.Controls.Add(btnBack);
        }

        // Event handler for Send Cash button
        private void BtnSend_Click(object sender, EventArgs e)
        {
            string recipientMobile = txtRecipientMobile.Text.Trim();
            
            if (string.IsNullOrEmpty(recipientMobile) || string.IsNullOrEmpty(txtSendAmount.Text))
            {
                MessageBox.Show("Please enter recipient mobile and amount.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtSendAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Call database helper to send cash
            bool success = DatabaseHelper.SendCash(currentUserId, recipientMobile, amount);
            
            if (success)
            {
                MessageBox.Show($"Successfully sent ${amount} to {recipientMobile}!", "Transaction Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtRecipientMobile.Text = "";
                txtSendAmount.Text = "";
            }
            else
            {
                MessageBox.Show("Transaction failed. Check your balance or the recipient's mobile number.", "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for Deposit Cash button
        private void BtnDeposit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDepositAmount.Text))
            {
                MessageBox.Show("Please enter an amount to deposit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtDepositAmount.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid amount greater than zero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Call database helper to deposit cash
            try
            {
                DatabaseHelper.DepositCash(currentUserId, amount);
                MessageBox.Show($"Successfully deposited ${amount} to your account!", "Deposit Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDepositAmount.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Deposit failed: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
