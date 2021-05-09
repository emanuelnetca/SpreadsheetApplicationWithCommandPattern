/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2021 Emanuel-Codrin Netca                            *
 *  E-mail:      emanuelnetca@gmail.com                                   *
 *  Description: Spreadsheet application with Command pattern.            *
 *                                                                        *
 **************************************************************************/

using System;
using System.Windows.Forms;

namespace Spreadsheet
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}