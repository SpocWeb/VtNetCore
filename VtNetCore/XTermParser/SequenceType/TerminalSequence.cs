using System.Collections.Generic;
using System.Linq;

namespace VtNetCore.XTermParser.SequenceType
{
	public class TerminalSequence
    {

		public TerminalSequence(Operator op = 0) {
			Op = op;
		}
		public List<int> Parameters { get; set; }
		public Operator Op { get; } 
        public List<TerminalSequence> ProcessFirst { get; set; }
		public string Command { get; set; }

		public enum Operator {

			Query = 1,
			Send,
			Bang,
			Equals

		}

        public override string ToString() {
			return
                "(" + Op.AsString() +
                ((Parameters != null && Parameters.Count > 0) ?
                    "[" + string.Join(",", Parameters.Select(x => x.ToString())) + "]" : ""
                ) +
                "'" + Command + "'" +
                "(" + string.Join(".",Command.Select(x => ((int)x).ToString("X2"))) + ")" + 
                ")";
		}

	}
}
