using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4Tac.Src.Services.Interfaces.Storage
{
    /// <summary>
    /// Schnittstelle für das Speichern und Laden des Spielstands.
    /// </summary>
    public interface IGameStorageService
    {
        /// <summary>
        /// Speichert den aktuellen Spielstand.
        /// </summary>
        void SaveGameState(string[,] boardState, string currentPlayer, bool isAI);

        /// <summary>
        /// Lädt den gespeicherten Spielstand.
        /// </summary>
        /// <returns>
        /// Ein Tupel bestehend aus dem Spielfeld (2D-Array), dem aktuellen Spieler und dem isAI-Wert.
        /// </returns>
        (string[,] boardState, string currentPlayer, bool isAI) LoadGameState();
    }
}
