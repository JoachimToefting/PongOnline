using PongOnlineMobile.Repository;
using PongOnlineMobile.Services;
using PongOnlineMobile.Views;
using System;
using TinyIoC;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PongOnlineMobile
{
	public partial class App : Application
	{

		public App()
		{
			InitializeComponent();
			MonkeyCache.SQLite.Barrel.ApplicationId = "MyApp";
			var container = TinyIoCContainer.Current;
			container.Register<IGenericRepository, GenericRepository>();
			MainPage = new AppShell();
			//MainPage = new LoginPage();
		}

		protected override void OnStart()
		{
		}

		protected override void OnSleep()
		{
		}

		protected override void OnResume()
		{
		}
	}
}
