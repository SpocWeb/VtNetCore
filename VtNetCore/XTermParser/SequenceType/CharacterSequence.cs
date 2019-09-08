﻿namespace VtNetCore.XTermParser.SequenceType
{
    public class CharacterSequence : TerminalSequence
    {
        public char Character { get; set; }
        public override string ToString()
        {
            return "Character - '" + Character + "' [" + ((int)Character).ToString("X2") + "]";
        }

		public CharacterSequence(Operator op = 0) : base(op) { }

	}
}
