using System;
using System.Windows.Forms;
using EWalletApp.Forms;
using EWalletApp.Database;

namespace EWalletApp
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Initialize the SQLite database
            DatabaseHelper.InitializeDatabase();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Launch the first form which is the Authentication Form
            Application.Run(new AuthForm());
        }
    }
}
