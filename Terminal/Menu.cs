using System.Collections.Generic;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace VariableIrony.Terminal {
	public class Menu : MenuItem {

        public IEnumerable<LabelSequence> Labels { get; set; }

        private void Display(IList<MenuItem> items, int depth, IList<LabelSequence> sequences)
        {
            if (items.Count > 0)
            {
                var sequence = sequences[depth].GetLabels().GetEnumerator();
                foreach (var item in items)
                {
                    sequence.MoveNext();
                    Console.WriteLine("{0}{1}: {2}", new string(' ', depth), sequence.Current, item.Text);
                    Display(item.Items, depth + 1, sequences);
                }
            }
        }

        private int GetDepth(MenuItem item)
        {
            return item.Items.Select(GetDepth).DefaultIfEmpty().Max() + 1;
        }

        public T Choose<T>() {
            return (T)Choose().State;
        }

        public MenuItem Choose()
        {
            var sequences = Labels.Take(GetDepth(this)).ToList();
            var pattern = new Regex($"^{sequences.Select(s => $"({s.Pattern})").Aggregate((x, y) => x + y)}$", RegexOptions.IgnoreCase);
            MenuItem result;
            do
            {
                Console.Clear();
                Display(Items, 0, sequences);
                Console.WriteLine();
                Console.Write(Text);
                Console.Write(' ');
                var input = Console.ReadLine();
                var match = pattern.Match(input);
                if (match.Success)
                {
                    result = this;
                    var depth = 0;
                    while (null != result)
                    {
                        if (result.Items.Count == 0)
                            break;
                        var index = sequences[depth].ParseLabel(match.Groups[depth + 1].Value);
                        result = index < result.Items.Count ? result.Items[index] : null;
                        depth++;
                    }
                }
                else
                {
                    result = null;
                }
            }
            while (null == result);
            return result;
        }
	}
}
