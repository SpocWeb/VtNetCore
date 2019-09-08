namespace VtNetCore.XTermParser.SequenceType
{
    public class DcsSequence : TerminalSequence
    {

		public DcsSequence(Operator op) : base(op) { }

		public override string ToString()
        {
            return "DCS - " + base.ToString();
        }
    }
}
