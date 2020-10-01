using System;
using System.Collections.Generic;
using System.Linq;

namespace VariableIrony.Terminal
{
    public class AlphaLabelSequence : LabelSequence
    {
        private static readonly char[] AlphaChars = Enumerable.Range((int)'a', 26).Select(n => (char)n).ToArray();
        public static readonly AlphaLabelSequence Instance = new AlphaLabelSequence();

        public override string Pattern => "[a-z]{1,9}";

        public override IEnumerable<string> GetLabels()
        {
            return
                from length in Enumerable.Range(1, 9)
                from i in AlphaChars
                select new string(i, length);
        }

        public override int ParseLabel(string label)
        {
            return GetLabels().TakeUntil(l => l.Equals(label, StringComparison.InvariantCultureIgnoreCase)).Count(); // HACK: parse this better
        }
    }
}
