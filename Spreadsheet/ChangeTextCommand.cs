/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2021 Emanuel-Codrin Netca                            *
 *  E-mail:      emanuelnetca@gmail.com                                   *
 *  Description: Spreadsheet application with Command pattern.            *
 *                                                                        *
 **************************************************************************/

using System;

namespace Spreadsheet
{
    public class ChangeTextCommand : Command
    {
        private ExtendedTextBox _cell;
        private string _text1, _text2;

        public ChangeTextCommand(ExtendedTextBox cell, string description)
        {
            // Set the value of each field.
            // The Description property defined in the Command base class.

            // The new text already exists in cell.Text so I will use the cell.PreviousText property to implement the command.

            _cell = cell;
            _text1 = cell.PreviousText;
            _text2 = cell.Text;
            this.Description = description;
        }

        public override bool MakesChanges()
        {
            // Returns true if something changes in the cell.
            // Returns false if nothing changes.

            return _text1 != _text2 ? true : false;
        }

        public override void Execute()
        {
        }

        public override void Undo()
        {
            _cell.Text = _text1;
        }

        public override void Redo()
        {
            _cell.Text = _text2;
        }
    }
}