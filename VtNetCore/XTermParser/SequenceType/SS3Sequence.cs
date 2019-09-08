namespace VtNetCore.XTermParser.SequenceType
{
    public class SS3Sequence : TerminalSequence
    {
        public override string ToString()
        {
            return "SS3 - " + base.ToString();
        }

		public SS3Sequence(Operator op = 0) : base(op) { }

	}
}
