﻿namespace VtNetCore.XTermParser.SequenceType
{
    public class Vt52MoveCursorSequence : TerminalSequence
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public override string ToString()
        {
            return "VT52 Move Cursor (r=" + Row + ",c=" + Column + ")";
        }

		public Vt52MoveCursorSequence(Operator op = 0) : base(op) { }

	}
}
