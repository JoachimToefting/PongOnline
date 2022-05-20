using PongOnlineMobile.Exceptions;
using PongOnlineMobile.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PongOnlineMobile.ViewModels
{
	public class GameItemViewModel : BaseViewModel
	{
		public ObservableCollection<Game> Games { get; }
		public Command LoadItemsCommand { get; }
		public Command<Game> ItemTapped { get; }
		private Game _selectedItem;
		public Game SelectedItem
		{
			get => _selectedItem;
			set
			{
				SetProperty(ref _selectedItem, value);
				OnItemSelected(value);
			}
		}

		public GameItemViewModel()
		{
			Title = "Games";
			Games = new ObservableCollection<Game>();
			LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			ItemTapped = new Command<Game>(OnItemSelected);
		}

		private async void OnItemSelected(Game item)
		{
			if (item == null)
			{
				return;
			}
			//TODO ADD detailspage
			await Shell.Current.GoToAsync($"/GameItemDetailPage");
		}

		public void OnAppearing()
		{
			IsBusy = true;
			SelectedItem = null;
		}
		async Task ExecuteLoadItemsCommand()
		{
			IsBusy = true;

			try
			{
				Games.Clear();
				var items = await _gameItemService.GetGameItems();
				foreach (var game in items)
				{
					Games.Add(game);
				}
			}
			catch(ServiceAuthenticationException e)
			{
				await Shell.Current.GoToAsync("//AboutPage");
			}
			catch (Exception)
			{

				throw;
			}
			finally
			{
				IsBusy = false;
			}
		}
	}
}
