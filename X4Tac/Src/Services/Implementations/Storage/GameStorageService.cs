using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Services.Interfaces.Storage;

namespace X4Tac.Src.Services.Implementations.Storage
{
    /// <summary>
    /// Implementierung des GameStorageService für das Speichern und Laden des Spielstands.
    /// Nutzt Preferences von .NET MAUI für das Speichern der Daten.
    /// </summary>
    public class GameStorageService : IGameStorageService
    {
        private const int BoardSize = 4;

        /// <inheritdoc />
        public void SaveGameState(string[,] boardState, string currentPlayer, bool isAI)
        {
            // Umwandeln des Spielfelds in eine flache Zeichenkette
            var flatBoard = string.Join(",", boardState.Cast<string>());
            Preferences.Set("BoardState", flatBoard);
            Preferences.Set("CurrentPlayer", currentPlayer);
            Preferences.Set("IsAI", isAI);
        }

        /// <inheritdoc />
        public (string[,] boardState, string currentPlayer, bool isAI) LoadGameState()
        {
            // Laden des gespeicherten Spielstands
            var savedBoardState = Preferences.Get("BoardState", null);
            var flatBoard = savedBoardState?.Split(',');
            string[,] boardState = new string[BoardSize, BoardSize];

            if (flatBoard != null)
            {
                // Das Spielfeld wiederherstellen
                for (int row = 0; row < BoardSize; row++)
                {
                    for (int col = 0; col < BoardSize; col++)
                    {
                        boardState[row, col] = flatBoard[row * BoardSize + col];
                    }
                }
            }

            // Den aktuellen Spieler laden
            string currentPlayer = Preferences.Get("CurrentPlayer", "X");

            // Den isAI-Wert laden
            bool isAI = Preferences.Get("IsAI", false);

            return (boardState, currentPlayer, isAI);
        }
    }
}
