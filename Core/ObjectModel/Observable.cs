using System;
using System.ComponentModel;
using System.Diagnostics;

namespace VariableIrony.ObjectModel
{
    /// <summary>
    /// Base class for components which can be observed using the <see cref="INotifyPropertyChanged" /> interface.
    /// </summary>
    public abstract class Observable : INotifyPropertyChanged
    {
        /// <summary>
        /// Notifies observers that a property value has changed.
        /// </summary>
        /// <remarks>See <see cref="PropertyChanged" /> for more information.</remarks>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the <see cref="PropertyChanged" /> event using the specified property name.
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed, or an empty string to indicate that the entire object has changed.</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = this.PropertyChanged;
            if (propertyChanged != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                propertyChanged(this, e);
            }
        }

        /// <summary>
        /// Verifies that a public property name exists on this instance.
        /// </summary>
        /// <param name="propertyName">The property name to lookup.</param>
        /// <remarks>
        /// <para>This method is used during debugging to verify that a property with a given name exists on an object. This can serve as a sanity check to ensure that events are being raised with the appropriate name.</para>
        /// <para>This method does not look directly at the properties defined on the type of the object; rather, it looks at the <see cref="PropertyDescriptor">PropertyDescriptors</see> returned by <see cref="TypeDescriptor" />.</para>
        /// <para>Calls to this method are not compiled into release builds.</para>
        /// </remarks>
        /// <seealso cref="TypeDescriptor" />
        /// <seealso cref="OnPropertyChanged(String)" />
        [DebuggerStepThrough, Conditional("DEBUG")]
        protected void VerifyPropertyName(string propertyName)
        {
            if (!string.IsNullOrEmpty(propertyName) && (TypeDescriptor.GetProperties(this)[propertyName] == null))
            {
                throw new ArgumentException($"Invalid property name: {propertyName}", nameof(propertyName));
            }
        }
    }
}
