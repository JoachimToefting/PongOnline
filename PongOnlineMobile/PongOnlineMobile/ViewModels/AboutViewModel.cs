using PongOnlineMobile.Services;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PongOnlineMobile.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public AboutViewModel()
		{
			Title = "About";
			OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
		}

		public ICommand OpenWebCommand { get; }

		private Command resetPong;
		public ICommand ResetPong => resetPong ??= new Command(PerformResetPong);

		private void PerformResetPong()
		{
			MQTTMobileService.ConnectAndSendReset();
		}
	}
}