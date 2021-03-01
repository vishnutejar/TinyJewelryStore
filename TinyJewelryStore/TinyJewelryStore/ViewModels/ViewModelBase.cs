using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TinyJewelryStore.ViewModels
{
   public class ViewModelBase : INotifyPropertyChanged
	{
		#region INotifyPropertyChanged implementation

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		protected void Notify(string propertyName)
		{
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
	}
}
