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
    internal class ChangeColorCommand : Command
    {
        private ExtendedTextBox _cell;
        private Color _color1, _color2;

        public ChangeColorCommand(ExtendedTextBox cell, Color color, string description)
        {
            // Set the value of each field.
            // The Description property defined in the Command base class.

            _cell = cell;
            _color1 = _cell.ForeColor;
            _color2 = color;
            this.Description = description;
        }

        public override bool MakesChanges()
        {
            // Returns true if something changes in the cell.
            // Returns false if nothing changes.

            return _color1 != _color2 ? true : false;
        }

        public override void Execute()
        {
            _cell.ForeColor = _color2;
        }

        public override void Undo()
        {
            _cell.ForeColor = _color1;
        }

        public override void Redo()
        {
            _cell.ForeColor = _color2;
        }
    }
}