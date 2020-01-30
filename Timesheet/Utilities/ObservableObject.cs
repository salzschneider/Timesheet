using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;

namespace Timesheet.UI.Utilities
{
    /// <summary>
    /// Handling the PropertyChanged event raised when a property is changed on a component
    /// </summary>
    public class ObservableObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raising PropertyChange event combined by value setting
        /// </summary>
        /// <param name="name"></param>
        public void OnPropertyChanged<T>(ref T property, T value, [CallerMemberName] string propertyName = "")
        {
            property = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
