using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Views.MainPage.Content;

namespace X4Tac.Src.ViewModels.MainPage.Head
{
    public partial class MainPageViewModel
    {
        public RelayCommand OnNewGameClicked => new RelayCommand(OnNewGame);
        public RelayCommand OnLoadGameClicked => new RelayCommand(OnLoadGame);
        public RelayCommand OnSaveGameClicked => new RelayCommand(OnSaveGame);

        private void OnNewGame()
        {
            CurrentContentView = _viewFactory.GetView<NewGameView>();
        }

        private void OnLoadGame()
        {
            _gameHandlerService.LoadGameState();
            Application.Current.MainPage.DisplayAlert("Laden", "Das Spiel wurde geladen!", "OK");
            CurrentContentView = _viewFactory.GetView<GameBoardView>();
        }

        private void OnSaveGame()
        {
            _gameHandlerService.SaveGameState();
            Application.Current.MainPage.DisplayAlert("Speichern", "Das Spiel wurde gespeichert!", "OK");
            CurrentContentView = _viewFactory.GetView<GameBoardView>();
        }
    }
}
