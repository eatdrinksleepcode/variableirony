using System.Collections.Generic;

namespace VariableIrony.Terminal
{
    public abstract class LabelSequence
    {
        public abstract string Pattern { get; }
        public abstract IEnumerable<string> GetLabels();
        public abstract int ParseLabel(string label);
    }
}
