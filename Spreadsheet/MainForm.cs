/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2021 Emanuel-Codrin Netca                            *
 *  E-mail:      emanuelnetca@gmail.com                                   *
 *  Description: Spreadsheet application with Command pattern.            *
 *                                                                        *
 **************************************************************************/

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spreadsheet
{
    public partial class MainForm : Form
    {
        private TextBoxGrid _grid;
        private Invoker _invoker;
        private int _selected; // The number of the cell in which an action is performed.

        public MainForm()
        {
            InitializeComponent();

            _grid = new TextBoxGrid(Controls, 27, 140, new EventHandler(textBox_Enter), new EventHandler(textBox_Leave), new KeyEventHandler(textBox_KeyDown));
            _invoker = new Invoker(_grid);

            UpdateUndoRedoCombos();
        }

        private void textBox_Enter(object sender, EventArgs e)
        {
            Control ac = this.ActiveControl;
            ExtendedTextBox tb = (ExtendedTextBox)sender;

            tb.PreviousText = tb.Text;
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            Control ac = this.ActiveControl;
            ExtendedTextBox tb = (ExtendedTextBox)sender;
            _selected = Convert.ToInt32(tb.Name.Substring(2));

            ChangeTextCommand changeTextCommand = new ChangeTextCommand(tb, string.Format("Change text on {0}: {1}", _grid.GetCoords(_selected), tb.Text));
            if (!changeTextCommand.MakesChanges())
            {
                return;
            }
            _invoker.SetAndExecute((Command) changeTextCommand);
            this.ActiveControl = ac;
            UpdateUndoRedoCombos();
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // When ENTER is pressed in a cell.
            {
                ExtendedTextBox tb = (ExtendedTextBox)sender;
                _selected = Convert.ToInt32(tb.Name.Substring(2));
                this.ActiveControl = _grid.GetSuccessor(_selected);
            }
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() != DialogResult.OK)
                return;

            Color c = colorDialog.Color;
            buttonColor.ForeColor = c;

            ChangeColorCommand changeColorCommand = new ChangeColorCommand(_grid.GetCell(_selected), c, string.Format("Change color on {0}: {1}", _grid.GetCoords(_selected), c.Name));
            if (!changeColorCommand.MakesChanges())
            {
                return;
            }
            _invoker.SetAndExecute((Command)changeColorCommand);
            UpdateUndoRedoCombos();

        }

        private void buttonNormal_Click(object sender, EventArgs e)
        {
            ChangeFormatCommand changeFormatCommand = new ChangeFormatCommand(_grid.GetCell(_selected), FontStyle.Regular, string.Format("Change format on {0}: Regular", _grid.GetCoords(_selected)));
            if (!changeFormatCommand.MakesChanges())
            {
                return;
            }
            _invoker.SetAndExecute((Command) changeFormatCommand);
            UpdateUndoRedoCombos();
        }

        private void buttonBold_Click(object sender, EventArgs e)
        {
            ChangeFormatCommand changeFormatCommand = new ChangeFormatCommand(_grid.GetCell(_selected), FontStyle.Bold, string.Format("Change format on {0}: Bold", _grid.GetCoords(_selected)));
            if (!changeFormatCommand.MakesChanges())
            {
                return;
            }
            _invoker.SetAndExecute((Command)changeFormatCommand);
            UpdateUndoRedoCombos();
        }

        private void buttonItalic_Click(object sender, EventArgs e)
        {
            ChangeFormatCommand changeFormatCommand = new ChangeFormatCommand(_grid.GetCell(_selected), FontStyle.Italic, string.Format("Change format on {0}: Italic", _grid.GetCoords(_selected)));
            if (!changeFormatCommand.MakesChanges())
            {
                return;
            }
            _invoker.SetAndExecute((Command)changeFormatCommand);
            UpdateUndoRedoCombos();
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            _invoker.Undo();
            UpdateUndoRedoCombos();
        }

        private void buttonRedo_Click(object sender, EventArgs e)
        {
             _invoker.Redo();
            UpdateUndoRedoCombos();
        }

        private void UpdateUndoRedoCombos()
        {
            comboBoxUndo.Items.Clear();
            foreach (string s in _invoker.UndoDescriptions)
                if(s != null)
                    comboBoxUndo.Items.Add(s);

            if (comboBoxUndo.Items.Count > 0)
            {
                comboBoxUndo.SelectedIndex = 0;
                buttonUndo.Enabled = true;
            }
            else
            {
                buttonUndo.Enabled = false;
            }

            comboBoxRedo.Items.Clear();
            foreach (string s in _invoker.RedoDescriptions)
                comboBoxRedo.Items.Add(s);

            if (comboBoxRedo.Items.Count > 0)
            {
                comboBoxRedo.SelectedIndex = 0;
                buttonRedo.Enabled = true;
            }
            else
            {
                buttonRedo.Enabled = false;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            _grid.Clear();
            _invoker.Clear();
            UpdateUndoRedoCombos();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void buttonAbout_Click(object sender, EventArgs e)
        {
            const string copyright =
               "Command pattern\r\n" +
               "(c)2021 Emanuel-Codrin Netca\r\n";

            MessageBox.Show(copyright, "About spreadsheet");
        }
    }
}