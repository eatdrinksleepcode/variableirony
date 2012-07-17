using System.Collections.Generic;
using System.Linq;

namespace VariableIrony.Terminal
{
    public class AlphaLabelSequence : LabelSequence
    {
        private static readonly char[] AlphaChars = Enumerable.Range((int)'a', 26).Select(n => (char)n).ToArray();
        public static readonly AlphaLabelSequence Instance = new AlphaLabelSequence();

        public override string Pattern
        {
            get { return "[a-z]{1,9}"; }
        }

        public override IEnumerable<string> GetLabels()
        {
            foreach (var length in Enumerable.Range(1, 9))
            {
                foreach (var i in AlphaChars)
                {
                    yield return new string(i, length); // HACK: won't work correctly beyond 27
                }
            }
        }

        public override int ParseLabel(string label)
        {
            return GetLabels().TakeWhile(l => l != label).Count(); // HACK: parse this better
        }
    }
}
