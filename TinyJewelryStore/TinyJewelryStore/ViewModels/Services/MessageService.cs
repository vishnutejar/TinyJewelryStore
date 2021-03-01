using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TinyJewelryStore.ViewModels.Services
{
	public class MessageService : IMessageService
	{
		#region IMessageService implementation

		public async System.Threading.Tasks.Task ShowAsync(string message)
		{
			await Application.Current.MainPage.DisplayAlert("", message, "ok");
		}

		#endregion

		public MessageService()
		{
		}
	}
}
