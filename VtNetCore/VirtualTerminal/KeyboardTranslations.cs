using System;
using System.Collections.Generic;
using System.Linq;

namespace VtNetCore.VirtualTerminal
{
	/// <summary>
    /// Provides static data for translating keystrokes from textual format to VT100... format
    /// </summary>
    /// <remarks>
    /// There are 144 Keys defined in <see cref="ConsoleKey"/>; only 54 are mapped here!
    /// </remarks>
    internal static class KeyboardTranslations
    {
        private static byte [] To8(this string str)
        {
            return str.Select(x => (byte)x).ToArray();
        }

        /// <summary>
        /// Translates a key sequence from text to DEC VT byte codes
        /// </summary>
        /// <param name="key">The key formatted as text as seen in the dictionary below.</param>
        /// <param name="modifiers">Keyboard Modifiers (<see cref="ConsoleModifiers.Alt"/> is not handled)</param>
        /// <param name="applicationMode">Set to true if the application mode mapping is desired</param>
        /// <returns></returns>
        public static byte[] GetKeySequence(ConsoleKey key, ConsoleModifiers modifiers, bool applicationMode)
        {
            if (!KeyTranslations.TryGetValue(key, out KeyboardTranslation translation)) {
                return null;
            }
            if (applicationMode && translation.ShiftOnApplication && !string.IsNullOrEmpty(translation.Shift))
                return translation.Shift.To8();

            if (0 != (modifiers & ConsoleModifiers.Shift))
                return string.IsNullOrEmpty(translation.Shift) ? null : translation.Shift.To8();

            if (0 != (modifiers & ConsoleModifiers.Control))
                return string.IsNullOrEmpty(translation.Control) ? null : translation.Control.To8();

            return string.IsNullOrEmpty(translation.Normal) ? null : translation.Normal.To8();

        }

        /// <summary>
        /// The translation table itself.
        /// </summary>
        private static readonly Dictionary<ConsoleKey, KeyboardTranslation> KeyTranslations = new Dictionary<ConsoleKey, KeyboardTranslation>
        {
            // ## Function keys (Normal or Application Mode)
            // | Key    | Normal  | Shift   | Control   |
            // |--------|---------|---------|-----------|
            // | F1     | CSI 11~ | CSI 23~ | CSI 11~   |
            // | F2     | CSI 12~ | CSI 24~ | CSI 12~   |
            // | F3     | CSI 13~ | CSI 25~ | CSI 13~   |
            // | F4     | CSI 14~ | CSI 26~ | CSI 14~   |
            // | F5     | CSI 15~ | CSI 28~ | CSI 15~   |
            // | F6     | CSI 17~ | CSI 29~ | CSI 17~   |
            // | F7     | CSI 18~ | CSI 31~ | CSI 18~   |
            // | F8     | CSI 19~ | CSI 32~ | CSI 19~   |
            // | F9     | CSI 20~ | CSI 33~ | CSI 20~   |
            // | F10    | CSI 21~ | CSI 24~ | CSI 21~   |
            // | F11    | CSI 23~ | CSI 23~ | CSI 23~   |
            // | F12    | CSI 24~ | CSI 24~ | CSI 24~   |
            { ConsoleKey.F1, new KeyboardTranslation { Normal = "\u001b[11~", Shift = "\u001b[23~", Control = "\u001b[11~" } },
            { ConsoleKey.F2,  new KeyboardTranslation { Normal = "\u001b[12~", Shift = "\u001b[24~", Control = "\u001b[12~" } },
            { ConsoleKey.F3,  new KeyboardTranslation { Normal = "\u001b[13~", Shift = "\u001b[25~", Control = "\u001b[13~" } },
            { ConsoleKey.F4,  new KeyboardTranslation { Normal = "\u001b[14~", Shift = "\u001b[26~", Control = "\u001b[14~" } },
            { ConsoleKey.F5,  new KeyboardTranslation { Normal = "\u001b[15~", Shift = "\u001b[28~", Control = "\u001b[15~" } },
            { ConsoleKey.F6,  new KeyboardTranslation { Normal = "\u001b[17~", Shift = "\u001b[29~", Control = "\u001b[17~" } },
            { ConsoleKey.F7,  new KeyboardTranslation { Normal = "\u001b[18~", Shift = "\u001b[31~", Control = "\u001b[18~" } },
            { ConsoleKey.F8,  new KeyboardTranslation { Normal = "\u001b[19~", Shift = "\u001b[32~", Control = "\u001b[19~" } },
            { ConsoleKey.F9,  new KeyboardTranslation { Normal = "\u001b[20~", Shift = "\u001b[33~", Control = "\u001b[20~" } },
            { ConsoleKey.F10, new KeyboardTranslation { Normal = "\u001b[21~", Shift = "\u001b[24~", Control = "\u001b[21~" } },
            { ConsoleKey.F11, new KeyboardTranslation { Normal = "\u001b[23~", Shift = "\u001b[23~", Control = "\u001b[23~" } },
            { ConsoleKey.F12, new KeyboardTranslation { Normal = "\u001b[24~", Shift = "\u001b[24~", Control = "\u001b[24~" } },

            // ## Arrow keys
            // | Key    | Normal | Shift  | Control  | Application |
            // |--------|--------|--------|----------|-------------|
            // | Up     | CSI A  | Esc OA | Esc OA   | Esc OA      |
            // | Down   | CSI B  | Esc OB | Esc OB   | Esc OB      |
            // | Right  | CSI C  | Esc OC | Esc OC   | Esc OC      |
            // | Left   | CSI D  | Esc OD | Esc OD   | Esc OD      |
            // | Home   | CSI 1~ | CSI 1~ |          | CSI 1~      |
            // | Ins    | CSI 2~ |        |          | CSI 2~      |
            // | Del    | CSI 3~ | CSI 3~ |          | CSI 3~      |
            // | End    | CSI 4~ | CSI 4~ |          | CSI 4~      |
            // | PgUp   | CSI 5~ | CSI 5~ |          | CSI 5~      |
            // | PgDn   | CSI 6~ | CSI 6~ |          | CSI 6~      |
            { ConsoleKey.UpArrow,       new KeyboardTranslation { Normal = "\u001b[A", Shift = "\u001bOA", Control = "\u001bOA", ShiftOnApplication = true } },
            { ConsoleKey.DownArrow,     new KeyboardTranslation { Normal = "\u001b[B", Shift = "\u001bOB", Control = "\u001bOB", ShiftOnApplication = true } },
            { ConsoleKey.RightArrow,    new KeyboardTranslation { Normal = "\u001b[C", Shift = "\u001bOC", Control = "\u001bOC", ShiftOnApplication = true } },
            { ConsoleKey.LeftArrow,     new KeyboardTranslation { Normal = "\u001b[D", Shift = "\u001bOD", Control = "\u001bOD", ShiftOnApplication = true } },
            { ConsoleKey.Home,          new KeyboardTranslation { Normal = "\u001b[1~", Shift = "\u001b[1~" } },
            { ConsoleKey.Insert,        new KeyboardTranslation { Normal = "\u001b[2~" } },
            { ConsoleKey.Delete,        new KeyboardTranslation { Normal = "\u001b[3~", Shift = "\u001b[3~" } },
            { ConsoleKey.End,           new KeyboardTranslation { Normal = "\u001b[4~", Shift = "\u001b[4~" } },
            { ConsoleKey.PageUp,        new KeyboardTranslation { Normal = "\u001b[5~", Shift = "\u001b[5~" } },
            { ConsoleKey.PageDown,      new KeyboardTranslation { Normal = "\u001b[6~", Shift = "\u001b[6~" } },

            // ## Number Keys (No num lock)
            // | Key       | Normal | Shift  | NumLock |
            // |-----------|--------|--------|---------|
            // | 0 (Ins)   | CSI 2~ |        | 0       |
            // | . (Del)   | CSI 3~ | CSI 3~ | .       |
            // | 1 (End)   | CSI 4~ | CSI 4~ | 1       |
            // | 2 (Down)  | CSI B  | ESC OB | 2       |
            // | 3 (PgDn)  | CSI 6~ |        | 3       |
            // | 4 (Left)  | CSI D  | Esc OD | 4       |
            // | 5         | CSI G  | Esc OG | 5       |
            // | 6 (Right) | CSI C  | Esc OC | 6       |
            // | 7 (Home)  | CSI 1~ | CSI 1~ | 7       |
            // | 8 (Up)    | CSI A  | Esc OA | 8       |
            // | 9 (PgUp)  | CSI 5~ |        | 9       |
            // | /         | /      | /      | /       |
            // | *         | *      | *      | *       |
            // | -         | -      | -      | -       |
            // | +         | +      | +      | +       |
            // | Enter     | \r\n   | \r\n   | \r\n    |

            // ## Main keyboard
            // | Key    | Normal | Shift  | Control  |
            // |--------|--------|--------|----------|
            // | Bksp   | \x7F   | \b     | \x7f     |
            // | Tab    | \t     | CSI Z  |          |
            // | Enter  | \r\n   | \r\n   | \r\n     |
            // | Esc    | Esc    | Esc    | Esc      |
            // | A      | a      | A      | \x01     |
            // | B      | b      | B      | \x02     |
            // | C      | c      | C      | \x03     |
            // | D      | d      | D      | \x04     |
            // | E      | e      | E      | \x05     |
            // | F      | f      | F      | \x06     |
            // | G      | g      | G      | \x07     |
            // | H      | h      | H      | \x08     |
            // | I      | i      | I      | \x09     |
            // | J      | j      | J      | \x0a     |
            // | K      | k      | K      | \x0b     |
            // | L      | l      | L      | \x0c     |
            // | M      | m      | M      | \x0d     |
            // | N      | n      | N      | \x0e     |
            // | O      | o      | O      | \x0f     |
            // | P      | p      | P      | \x10     |
            // | Q      | q      | Q      | \x11     |
            // | R      | r      | R      | \x12     |
            // | S      | s      | S      | \x13     |
            // | T      | t      | T      | \x14     |
            // | U      | u      | U      | \x15     |
            // | V      | v      | V      | \x16     |
            // | W      | w      | W      | \x17     |
            // | X      | x      | X      | \x18     |
            // | Y      | y      | Y      | \x19     |
            // | Z      | z      | Z      | \x1a     |
            { ConsoleKey.Backspace,    new KeyboardTranslation { Normal = "\u007F", Shift = "\b", Control = "\u007F" } },
            { ConsoleKey.Tab,     new KeyboardTranslation { Normal = "\t", Shift = "\u001b[Z" } },
            { ConsoleKey.Enter,   new KeyboardTranslation { Normal = "\n", Shift = "\n", Control = "\n" } },
            //{ ConsoleKey.Return,  new KeyboardTranslation { Normal = "\n", Shift = "\n", Control = "\n" } },
            { ConsoleKey.Escape,  new KeyboardTranslation { Normal = "\u001b\u001b", Shift = "\u001b\u001b", Control = "\u001b\u001b" } },
            { ConsoleKey.A,       new KeyboardTranslation { Control = "\u0001" } },
            { ConsoleKey.B,       new KeyboardTranslation { Control = "\u0002" } },
            { ConsoleKey.C,       new KeyboardTranslation { Control = "\u0003" } },
            { ConsoleKey.D,       new KeyboardTranslation { Control = "\u0004" } },
            { ConsoleKey.E,       new KeyboardTranslation { Control = "\u0005" } },
            { ConsoleKey.F,       new KeyboardTranslation { Control = "\u0006" } },
            { ConsoleKey.G,       new KeyboardTranslation { Control = "\u0007" } },
            { ConsoleKey.H,       new KeyboardTranslation { Control = "\u0008" } },
            { ConsoleKey.I,       new KeyboardTranslation { Control = "\u0009" } },
            { ConsoleKey.J,       new KeyboardTranslation { Control = "\u000a" } },
            { ConsoleKey.K,       new KeyboardTranslation { Control = "\u000b" } },
            { ConsoleKey.L,       new KeyboardTranslation { Control = "\u000c" } },
            { ConsoleKey.M,       new KeyboardTranslation { Control = "\u000d" } },
            { ConsoleKey.N,       new KeyboardTranslation { Control = "\u000e" } },
            { ConsoleKey.O,       new KeyboardTranslation { Control = "\u000f" } },
            { ConsoleKey.P,       new KeyboardTranslation { Control = "\u0010" } },
            { ConsoleKey.Q,       new KeyboardTranslation { Control = "\u0011" } },
            { ConsoleKey.R,       new KeyboardTranslation { Control = "\u0012" } },
            { ConsoleKey.S,       new KeyboardTranslation { Control = "\u0013" } },
            { ConsoleKey.T,       new KeyboardTranslation { Control = "\u0014" } },
            { ConsoleKey.U,       new KeyboardTranslation { Control = "\u0015" } },
            { ConsoleKey.V,       new KeyboardTranslation { Control = "\u0016" } },
            { ConsoleKey.W,       new KeyboardTranslation { Control = "\u0017" } },
            { ConsoleKey.X,       new KeyboardTranslation { Control = "\u0018" } },
            { ConsoleKey.Y,       new KeyboardTranslation { Control = "\u0019" } },
            { ConsoleKey.Z,       new KeyboardTranslation { Control = "\u001a" } }
        };
    }
}
