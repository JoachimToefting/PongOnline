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
	public partial class GameItemPage : ContentPage
	{
		GameItemViewModel _gameItemViewModel;
		public GameItemPage()
		{
			InitializeComponent();
			BindingContext = _gameItemViewModel = new GameItemViewModel();
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			_gameItemViewModel.OnAppearing();
		}
	}
}