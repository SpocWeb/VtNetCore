using System;
using VtNetCore.VirtualTerminal;
using VtNetCore.XTermParser.SequenceType;

namespace VtNetCore.XTermParser
{
	public class SequenceHandler
    {

		public SequenceHandler(TerminalSequence.Operator op = 0) {
			Op = op;
		}

		public enum ESequenceType
        {
            Character,
            CSI,            // Control Sequence Intro
            OSC,            // Operating System Command
            DCS,            // Device Control String
            SS3,            // Signal Shift Select 3
            VT52mc,         // VT52 Move Cursor
            Compliance,     // Compliance
            CharacterSet,   // Character set
            Escape,
            CharacterSize,
            Unicode
        }

        public enum Vt52Mode
        {
            Irrelevant,
            Yes,
            No
        }

        public string Description { get; set; }
        public ESequenceType SequenceType { get; set; }
        public int ExactParameterCount { get; set; } = -1;
        public int ExactParameterCountOrDefault { get; set; } = -1;
        public int DefaultParamValue { get; set; } = 1;
        public int MinimumParameterCount { get; set; } = 0;
		public TerminalSequence.Operator Op { get; }
        public int[] Param0 { get; set; } = { };
        public int[] ValidParams { get; set; } = { };
        public string CsiCommand { get; set; }
        public string OscText { get; set; }
        public Action<TerminalSequence, IVirtualTerminalController> Handler { get; set; }
        public Vt52Mode Vt52 { get; set; } = Vt52Mode.Irrelevant;
    }
}
