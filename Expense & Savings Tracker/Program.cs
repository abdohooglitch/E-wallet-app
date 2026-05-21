using ExpenseSavingsTracker.Database;
using ExpenseSavingsTracker.Forms;

namespace ExpenseSavingsTracker
{
    /// <summary>
    /// Application entry point for the Expense & Savings Tracker WinForms app.
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Main method: initializes the SQLite database, configures the UI, and launches the login form.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Create database tables if they do not exist yet
            DatabaseHelper.InitializeDatabase();

            // Start Windows Forms application with the authentication screen
            ApplicationConfiguration.Initialize();
            Application.Run(new AuthForm());
        }
    }
}
