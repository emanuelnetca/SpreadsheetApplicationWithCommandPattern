/**************************************************************************
 *                                                                        *
 *  Copyright:   (c) 2021 Emanuel-Codrin Netca                            *
 *  E-mail:      emanuelnetca@gmail.com                                   *
 *  Description: Spreadsheet application with Command pattern.            *
 *                                                                        *
 **************************************************************************/

namespace Spreadsheet
{
    public abstract class Command
    {
        public abstract bool MakesChanges();

        public abstract void Execute();

        public abstract void Undo();

        public abstract void Redo();

        public string Description { get; protected set; }
    }
}