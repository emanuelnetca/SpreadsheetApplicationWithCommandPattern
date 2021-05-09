/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2021 Emanuel-Codrin Netca                            *
 *  E-mail:      emanuelnetca@gmail.com                                   *
 *  Description: Spreadsheet application with Command pattern.            *
 *                                                                        *
 **************************************************************************/

using System.Windows.Forms;

namespace Spreadsheet
{
    public class ExtendedTextBox : TextBox
    {
        public string PreviousText { get; set; }

        public ExtendedTextBox()
            : base()
        {
            PreviousText = "";
        }
    }
}