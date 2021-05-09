/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2021 Emanuel-Codrin Netca                            *
 *  E-mail:      emanuelnetca@gmail.com                                   *
 *  Description: Spreadsheet application with Command pattern.            *
 *                                                                        *
 **************************************************************************/

using System;
using System.Drawing;

namespace Spreadsheet
{
    public class ChangeFormatCommand : Command
    {
        private ExtendedTextBox _cell;
        private FontStyle _format1, _format2;

        public ChangeFormatCommand(ExtendedTextBox cell, FontStyle format, string description)
        {
            // Set the value of each field.
            // The Description property defined in the Command base class.

            _cell = cell;
            _format1 = _cell.Font.Style;
            _format2 = format;
            this.Description = description;
        }

        public override bool MakesChanges()
        {
            // Returns true if something changes in the cell.
            // Returns false if nothing changes.

            return _format1 != _format2 ? true : false;
        }

        public override void Execute()
        {
            _cell.Font = new Font(_cell.Font, _format2);
        }

        public override void Undo()
        {
            _cell.Font = new Font(_cell.Font, _format1);
        }

        public override void Redo()
        {
            _cell.Font = new Font(_cell.Font, _format2);
        }
    }
}