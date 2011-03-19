using System;
using System.Reflection;
using System.Linq.Expressions;
using System.ComponentModel;
using System.Windows;

namespace Rosette.Windows
{
    /// <summary>A class which is a view model of a window.</summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>Event when a property changes.</summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>Calls the property changed event for the ViewModel, with the specified property name.</summary>
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        /// <summary>Calls the property changed event for the ViewModel with a property.</summary>
        public void OnPropertyChanged<T>(Expression<Func<object, T>> propertyAction)
        {
            var expression = (MemberExpression)propertyAction.Body;
            var propertyName = expression.Member.Name;
            OnPropertyChanged(propertyName);
        }
    }
}
