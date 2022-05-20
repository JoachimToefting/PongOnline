using PongOnlineMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PongOnlineMobile.ViewModels
{
	[QueryProperty(nameof(ItemId), nameof(ItemId))]
	public class GameItemDetailsViewModel : BaseViewModel
	{
		private int itemId;
		private Game game;
		private string winner;
		public Game Game
		{
			get => game;
			set => SetProperty(ref game, value);
		}
		public int ItemId
		{
			get => itemId;
			set
			{
				itemId = value;
				LoadItemId(value);
			}
		}
		public string Winner
		{
			get => winner;
			set
			{
				winner = value;
				SetProperty(ref winner, value);
			}
		}

		public async void LoadItemId(int value)
		{
			try
			{
				var game = await _gameItemService.GetGameItemByIdAsync(value);
				Game = game;
				if (game.Player1.Win)
				{
					Winner = "Player1 with playernumber: " + game.Player1.PlayerNumber;
				}
				else if (game.Player2.Win)
				{
					winner = "Player2 with playernumber: " + game.Player2.PlayerNumber;
				}
				else
				{
					winner = "None";
				}
			}
			catch (Exception)
			{

				throw;
			}
		}
	}
}
