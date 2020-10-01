using System.Collections.Generic;
using System.Linq;

namespace VariableIrony.Terminal
{
    public class NumericLabelSequence : LabelSequence
    {
        public static readonly NumericLabelSequence Instance = new NumericLabelSequence();

        public override string Pattern => @"\d{1,9}";

        public override IEnumerable<string> GetLabels()
        {
            return Enumerable.Range(1, 999999999).Select(n => n.ToString());
        }

        public override int ParseLabel(string label)
        {
            return int.Parse(label) - 1;
        }
    }
}
