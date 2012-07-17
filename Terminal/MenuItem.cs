using System.Collections.Generic;

namespace VariableIrony.Terminal {
	public class MenuItem {
		public string Text { get; set; }
		public object State { get; set; }

        private readonly List<MenuItem> _items = new List<MenuItem>();
        public IList<MenuItem> Items { get { return _items; } }
    }
}
