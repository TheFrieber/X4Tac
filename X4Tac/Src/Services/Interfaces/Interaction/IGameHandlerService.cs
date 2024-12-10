using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace X4Tac.Src.Services.Interfaces.Interaction
{
    /// <summary>
    /// Schnittstelle für die Spiellogik des 4x4-Tic-Tac-Toe-Spiels.
    /// Bietet Funktionen zur Zugausführung, Statusprüfung,
    /// Rücksetzung und zum Speichern sowie Laden des Spielstands.
    /// </summary>
    public interface IGameHandlerService
    {
        /// <summary>
        /// Gibt den aktuellen Spieler zurück ("X" oder "O").
        /// </summary>
        string CurrentPlayer { get; }

        /// <summary>
        /// Gibt das aktuelle Spielfeld zurück, dargestellt als 2D-Array.
        /// </summary>
        string[,] Board { get; }

        /// <summary>
        /// Setzt den Spiel-Modi
        /// </summary>
        /// <param name="gameMode">Das ausgewähle Spiel-Modi</param>
        void SetMode(GameMode gameMode);

        /// <summary>
        /// Führt einen Zug für den aktuellen Spieler aus.
        /// </summary>
        /// <param name="row">Die Zeile des Spielfelds.</param>
        /// <param name="col">Die Spalte des Spielfelds.</param>
        /// <param name="isAI">Ob gegen KI gespielt wird.</param>
        /// <returns>True, wenn der Zug erfolgreich war; andernfalls False.</returns>
        Task<bool> MakeMoveAsync(int row, int col, bool isAI);

        /// <summary>
        /// Überprüft den aktuellen Spielstatus.
        /// </summary>
        /// <returns>"X lost", "O lost", "Draw" oder "In Progress".</returns>
        string CheckGameStatus();

        /// <summary>
        /// Setzt das Spiel zurück und leert das Spielfeld.
        /// </summary>
        void ResetGame();

        /// <summary>
        /// Speichert den aktuellen Spielstand in den Preferences.
        /// </summary>
        void SaveGameState();

        /// <summary>
        /// Lädt einen gespeicherten Spielstand aus den Preferences.
        /// </summary>
        void LoadGameState();
    }

    public enum GameMode
    {
        Spieler = 0,
        KI = 1
    }
}
