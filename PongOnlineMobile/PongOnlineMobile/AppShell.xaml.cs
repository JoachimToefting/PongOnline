using PongOnlineMobile.ViewModels;
using PongOnlineMobile.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PongOnlineMobile
{
	public partial class AppShell : Xamarin.Forms.Shell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(nameof(GameItemDetailPage), typeof(GameItemDetailPage));
		}
	}
}
