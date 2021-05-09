/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2021 Emanuel-Codrin Netca                            *
 *  E-mail:      emanuelnetca@gmail.com                                   *
 *  Description: Spreadsheet application with Command pattern.            *
 *                                                                        *
 **************************************************************************/

using System;
using System.Collections.Generic;

namespace Spreadsheet
{
    public class Invoker
    {
        private TextBoxGrid _grid;
        private Stack<Command> _commands;
        private Stack<Command> _redoCommands;

        public Invoker(TextBoxGrid grid) : base()
        {
            _grid = grid;
            _commands = new Stack<Command>();
            _redoCommands = new Stack<Command>();
        }

        public List<string> UndoDescriptions
        {
            get
            {
                List<string> descriptions = new List<string>();
                foreach (Command cmd in _commands)
                    descriptions.Add(cmd.Description);
                return descriptions;
            }
        }

        public List<string> RedoDescriptions
        {
            get
            {
                List<string> descriptions = new List<string>();
                foreach (Command cmd in _redoCommands)
                    descriptions.Add(cmd.Description);
                return descriptions;
            }
        }

        public void SetAndExecute(Command command)
        {
            // Executes the received command as a parameter and adds it to the command stack.

            command.Execute();
            _commands.Push(command);
            _redoCommands.Clear();
        }

        public void Undo()
        {
            Command command = _commands.Pop();
            command.Undo();
            _redoCommands.Push(command);
        }

        public void Redo()
        {
            Command command = _redoCommands.Pop();
            command.Redo();
            _commands.Push(command);
        }

        public void Clear()
        {
            this._commands.Clear();
            this._redoCommands.Clear();
        }
    }
}