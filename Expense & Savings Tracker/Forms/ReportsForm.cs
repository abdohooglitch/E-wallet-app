using ExpenseSavingsTracker.Database;

namespace ExpenseSavingsTracker.Forms
{
    /// <summary>
    /// Category breakdown report: spending per category for a selected month and year.
    /// </summary>
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

        /// <summary>
        /// Fills month and year dropdowns (current month selected; last 3 years available).
        /// </summary>
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

        /// <summary>
        /// Loads grouped category totals and percentage share for the chosen period.
        /// </summary>
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

        /// <summary>Reloads report when user changes month or year and clicks Load.</summary>
        private void BtnLoad_Click(object? sender, EventArgs e) => LoadReport();

        /// <summary>Closes the form and returns to the dashboard.</summary>
        private void BtnBack_Click(object? sender, EventArgs e) => Close();
    }
}
