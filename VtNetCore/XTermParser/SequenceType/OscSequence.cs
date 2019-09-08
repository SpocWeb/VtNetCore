namespace VtNetCore.XTermParser.SequenceType
{
    public class OscSequence : TerminalSequence
    {
        public override string ToString()
        {
            return "OSC - " + base.ToString();
        }

		public OscSequence(Operator op) : base(op) { }

	}
}
