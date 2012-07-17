using System.Collections.Generic;
using System.Collections.Specialized;

namespace VariableIrony.ObjectModel
{
    public class ObservableCollection<T> : System.Collections.ObjectModel.ObservableCollection<T>
    {
        private bool _inUpdate;

        public void AddRange(IEnumerable<T> range)
        {
            BeginUpdate();
            foreach (var item in range)
            {
                Add(item);
            }
            EndUpdate();
        }

        private void BeginUpdate()
        {
            _inUpdate = true;
        }

        private void EndUpdate()
        {
            if (_inUpdate)
            {
                _inUpdate = false;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (!_inUpdate)
            {
                base.OnCollectionChanged(e);
            }
        }
    }
}
