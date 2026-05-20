using ExpenseSavingsTracker.Database;
using ExpenseSavingsTracker.Models;

namespace ExpenseSavingsTracker.Forms
{
    public partial class SavingsForm : Form
    {
        private readonly int _userId;
        private List<SavingGoal> _goals = new();

        public SavingsForm(int userId)
        {
            _userId = userId;
            InitializeComponent();
            LoadGoals();
        }

        private void LoadGoals()
        {
            _goals = DatabaseHelper.GetSavingGoals(_userId);
            dgvGoals.DataSource = _goals.Select(g => new
            {
                g.Id,
                g.Title,
                Saved = $"Rs {g.SavedAmount:N0}",
                Target = $"Rs {g.TargetAmount:N0}",
                Progress = $"{g.ProgressPercent:0}%"
            }).ToList();

            if (dgvGoals.Columns.Contains("Id"))
                dgvGoals.Columns["Id"]!.Visible = false;
        }

        private SavingGoal? GetSelectedGoal()
        {
            if (dgvGoals.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a savings goal first.", "Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return null;
            }

            int goalId = Convert.ToInt32(dgvGoals.SelectedRows[0].Cells["Id"].Value);
            return _goals.FirstOrDefault(g => g.Id == goalId);
        }

        private void BtnCreateGoal_Click(object? sender, EventArgs e)
        {
            string title = txtGoalTitle.Text.Trim();
            if (!decimal.TryParse(txtTargetAmount.Text.Trim(), out decimal target) || target <= 0)
            {
                MessageBox.Show("Enter a valid target amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseHelper.CreateSavingGoal(_userId, title, target))
            {
                MessageBox.Show("Could not create goal. Check the title and amount.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtGoalTitle.Clear();
            txtTargetAmount.Clear();
            LoadGoals();
            MessageBox.Show("Savings goal created!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnDeposit_Click(object? sender, EventArgs e)
        {
            var goal = GetSelectedGoal();
            if (goal == null) return;

            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (goal.SavedAmount >= goal.TargetAmount)
            {
                MessageBox.Show("This goal is already complete!", "Goal Complete",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!DatabaseHelper.DepositToGoal(goal.Id, amount))
            {
                MessageBox.Show("Deposit failed.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtAmount.Clear();
            LoadGoals();
            MessageBox.Show($"Rs {amount:N0} added to '{goal.Title}'.", "Deposit",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnWithdraw_Click(object? sender, EventArgs e)
        {
            var goal = GetSelectedGoal();
            if (goal == null) return;

            if (!decimal.TryParse(txtAmount.Text.Trim(), out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Enter a valid amount.", "Validation",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!DatabaseHelper.WithdrawFromGoal(goal.Id, amount))
            {
                MessageBox.Show("Withdraw failed. Amount may exceed saved balance.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtAmount.Clear();
            LoadGoals();
            MessageBox.Show($"Rs {amount:N0} withdrawn from '{goal.Title}'.", "Withdraw",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnBack_Click(object? sender, EventArgs e) => Close();
    }
}
