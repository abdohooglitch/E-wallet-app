using FinanceTracker.Database;
using FinanceTracker.Forms;

namespace FinanceTracker
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            DatabaseHelper.InitializeDatabase();

            ApplicationConfiguration.Initialize();
            Application.Run(new AuthForm());
        }
    }
}
