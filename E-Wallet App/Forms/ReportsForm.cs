using FinanceTracker.Database;

namespace FinanceTracker.Forms
{
    public partial class ReportsForm : Form
    {
        private readonly int _userId;

        public ReportsForm(int userId)
        {
            _userId = userId;
            InitializeComponent();
            InitPeriodSelectors();
            LoadReport();
        }

        private void InitPeriodSelectors()
        {
            cmbMonth.Items.AddRange(new object[]
            {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });
            cmbMonth.SelectedIndex = DateTime.Now.Month - 1;

            int currentYear = DateTime.Now.Year;
            for (int y = currentYear; y >= currentYear - 2; y--)
                cmbYear.Items.Add(y);
            cmbYear.SelectedIndex = 0;
        }

        private void LoadReport()
        {
            int month = cmbMonth.SelectedIndex + 1;
            int year = (int)cmbYear.SelectedItem!;

            var report = DatabaseHelper.GetExpensesByCategory(_userId, year, month);
            dgvReport.DataSource = report.Select(r => new
            {
                r.Category,
                Amount = $"Rs {r.Total:N0}",
                Share = $"{r.Percentage}%"
            }).ToList();

            decimal total = report.Sum(r => r.Total);
            lblGrandTotal.Text = $"Grand Total: Rs {total:N0}";
        }

        private void BtnLoad_Click(object? sender, EventArgs e) => LoadReport();

        private void BtnBack_Click(object? sender, EventArgs e) => Close();
    }
}
