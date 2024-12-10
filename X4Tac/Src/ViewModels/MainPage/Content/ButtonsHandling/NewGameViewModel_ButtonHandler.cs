using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Services.Interfaces.Interaction;
using X4Tac.Src.Views.MainPage.Content;

namespace X4Tac.Src.ViewModels.MainPage.Content
{
    public partial class NewGameViewModel
    {
        public RelayCommand OnPlayAgainstPlayerClicked => new RelayCommand(OnPlayAgainstPlayer);
        public RelayCommand OnPlayAgainstAIClicked => new RelayCommand(OnPlayAgainstAI);

        public void OnPlayAgainstPlayer()
        {
            LaunchGame(GameMode.Spieler);
        }

        public void OnPlayAgainstAI()
        {
            LaunchGame(GameMode.KI);
        }



        private void LaunchGame(GameMode gameMode)
        {
            _gameHandlerService.ResetGame();
            _gameHandlerService.SetMode(gameMode);
            _uIInteractionService.SetContentView(_viewFactory.GetView<GameBoardView>());
        }
    }
}
