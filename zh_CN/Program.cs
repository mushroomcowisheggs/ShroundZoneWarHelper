using ShroundZoneHelper.Forms;
using System;
using System.Windows.Forms;

namespace ShroundZoneHelper
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new MainForm());
        }
    }
}