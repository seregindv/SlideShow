using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SlideShow
{
    static class Program
    {
        public static string[] CmdLineArgs;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            CmdLineArgs = args;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new CartoonForm());
        }
    }
}