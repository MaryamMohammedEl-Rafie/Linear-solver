using System;
using System.Windows.Forms;

namespace linear_solver
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            // Enables modern Windows theme rendering for controls.
            Application.EnableVisualStyles();

            // Uses GDI+ for better text rendering.
            Application.SetCompatibleTextRenderingDefault(false);

            // Runs the main form.
            Application.Run(new MainForm());
        }
    }
}
