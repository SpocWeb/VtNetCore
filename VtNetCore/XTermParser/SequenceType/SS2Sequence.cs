namespace VtNetCore.XTermParser.SequenceType
{
    class SS2Sequence : TerminalSequence
    {
        public override string ToString()
        {
            return "SSs - " + base.ToString();
        }

		public SS2Sequence() : base(0) { }

	}
}
