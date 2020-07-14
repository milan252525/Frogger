//Frogger - zápočtový program
//Milan Abrahám
//NPRG031 Programování 2

using System;
using System.Windows.Forms;

namespace Frogger
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
