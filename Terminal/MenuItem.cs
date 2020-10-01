using System.Collections.Generic;

namespace VariableIrony.Terminal {
	public class MenuItem {
		public string Text { get; set; }
		public object State { get; set; }
		public IList<MenuItem> Items { get; } = new List<MenuItem>();
	}
}
