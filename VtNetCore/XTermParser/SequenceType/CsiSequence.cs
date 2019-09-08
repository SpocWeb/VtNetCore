namespace VtNetCore.XTermParser.SequenceType
{
    public class CsiSequence : TerminalSequence
    {

		public CsiSequence(Operator op) : base(op) { }

		public override string ToString()
        {
            return "CSI - " + base.ToString();
        }
    }
}
