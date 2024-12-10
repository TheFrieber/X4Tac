using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Views.MainPage.Content;

namespace X4Tac.Src.ViewModels.MainPage.Content
{
    public partial class GameBoardViewModel
    {
        public RelayCommand<string> OnCellClicked => new RelayCommand<string>(HandleCellClick);

        private async void HandleCellClick(string cellCoordinates)
        {
            // CellCoordinates sind im Format "row,col", z.B. "0,1"
            var parts = cellCoordinates.Split(',');
            if (parts.Length != 2 || !int.TryParse(parts[0], out int row) || !int.TryParse(parts[1], out int col))
                return;

            // Spiellogik aufrufen
            bool moveSuccessful = await _gameHandlerService.MakeMoveAsync(row, col, false);

            if (!moveSuccessful)
            {
                return;
            }

            // Spielstatus prüfen
            string gameStatus = _gameHandlerService.CheckGameStatus();
            if (gameStatus != "In Progress")
            {
                await Application.Current.MainPage.DisplayAlert("Game Over", gameStatus, "OK");
                _gameHandlerService.ResetGame();
                await Task.Delay(500);
                _uiInteractionService.SetContentView(_viewFactory.GetView<NewGameView>());
            }
        }
    }
}
