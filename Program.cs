using G_MBIVautoTester.UI.Forms;
using G_MBIVautoTester.UI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G_MBIVautoTester
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new Basic_Test_Screen());
            //   Application.Run(new FormSerial());
            //  Application.Run(new Form1());
            Application.Run(new Form_Page4());

        }
    }
}
