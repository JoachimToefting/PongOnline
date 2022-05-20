using PongOnlineMobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PongOnlineMobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameItemDetailPage : ContentPage
	{
		private GameItemDetailsViewModel _gameItemDetailsViewModel;
		public GameItemDetailPage()
		{
			InitializeComponent();
			BindingContext = _gameItemDetailsViewModel = new GameItemDetailsViewModel();
		}
	}
}