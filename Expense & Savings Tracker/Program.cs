using ExpenseSavingsTracker.Database;
using ExpenseSavingsTracker.Forms;

namespace ExpenseSavingsTracker
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
