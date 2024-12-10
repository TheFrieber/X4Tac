using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Services.Interfaces.Interaction;
using X4Tac.Src.Services.Interfaces.Storage;
using X4Tac.Src.Services.Interfaces.UI;

namespace X4Tac.Src.Services.Implementations.Interaction
{
    /// <summary>
    /// Implementierung der Spiellogik für das 4x4-Tic-Tac-Toe-Spiel.
    /// Verwaltet das Spielfeld, Spielerwechsel und den Spielstatus.
    /// Nutzt den GameStorageService zum Speichern und Laden des Spielstands.
    /// </summary>
    public class GameHandlerService : IGameHandlerService
    {
        private readonly IGameStorageService _gameStorageService;
        private readonly IUIInteractionService _uiInteractionService;
        private const int BoardSize = 4;


        public string CurrentPlayer { get; private set; } = "X";
        public string[,] Board { get; private set; } = new string[BoardSize, BoardSize];

        private bool isAgainstAI;



        // Dependency Injection des GameStorageService
        public GameHandlerService(IGameStorageService gameStorageService, IUIInteractionService uIInteractionService)
        {
            _gameStorageService = gameStorageService;
            _uiInteractionService = uIInteractionService;
            ResetGame();
        }

        /// <inheritdoc />
        public void SetMode(GameMode gameMode)
        {
            isAgainstAI = gameMode != 0;
        }

        /// <inheritdoc />
        public async Task<bool> MakeMoveAsync(int row, int col, bool isAI)
        {
            if (CurrentPlayer == "O" && !isAI && isAgainstAI)
            {
                await Application.Current.MainPage.DisplayAlert("Das verstößt gegen die Regeln!", "Die KI ist am Zug.", "OK");
                return false;
            }

            if (row < 0 || row >= BoardSize || col < 0 || col >= BoardSize || !string.IsNullOrEmpty(Board[row, col]))
            {
                await Application.Current.MainPage.DisplayAlert("Das verstößt gegen die Regeln!", "Das Feld ist bereits besetzt.", "OK");
                return false;
            }

            Board[row, col] = CurrentPlayer;

            // UI aktualisieren
            UpdateUI(row, col, CurrentPlayer);

            CurrentPlayer = CurrentPlayer == "X" ? "O" : "X";

            // Wenn gegen die KI gespielt wird und der KI-Zug ansteht, führe ihn aus
            if (isAgainstAI && CurrentPlayer == "O" && CheckGameStatus() == "In Progress")
            {
                // Indicator Overlay sichtbar machen
                _uiInteractionService.UpdateProperty<VisualElement>("GameOverlayAIIndicator", overlay =>
                {
                    overlay.IsVisible = true;
                });

                (int aiRow, int aiCol) = (-1, -1);

                // Überprüfen, ob der erste KI-Zug oder nur ein "X" vorhanden ist
                if (IsFirstMove() || CountMarks("X") == 1)
                {
                    (aiRow, aiCol) = GetRandomEmptyField();
                }
                else
                {
                    // Warten auf den besten Zug der KI (Minimax)
                    (aiRow, aiCol) = await FindBestMove().ConfigureAwait(false);
                }

                Application.Current.Dispatcher.Dispatch(() =>{
                    // Overlay wieder ausblenden
                    _uiInteractionService.UpdateProperty<VisualElement>("GameOverlayAIIndicator", overlay =>
                    {
                        overlay.IsVisible = false;
                    });
                });


                if (aiRow != -1 && aiCol != -1)
                {
                    await MakeMoveAsync(aiRow, aiCol, true).ConfigureAwait(false); // KI-Zug
                }
            }

            return true;
        }



        /// <summary>
        /// Hilfsmethode: Überprüft, ob das Spielfeld leer ist (erster Zug)
        /// </summary>
        /// <returns></returns>
        private bool IsFirstMove()
        {
            foreach (var cell in Board)
            {
                if (!string.IsNullOrEmpty(cell))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Zählt die Anzahl der Markierungen eines Spielers
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        private int CountMarks(string player)
        {
            int count = 0;
            foreach (var cell in Board)
            {
                if (cell == player)
                    count++;
            }
            return count;
        }

        /// <summary>
        /// Wählt ein zufälliges leeres Feld aus
        /// </summary>
        /// <returns></returns>
        private (int, int) GetRandomEmptyField()
        {
            var emptyFields = new List<(int, int)>();

            for (int row = 0; row < BoardSize; row++)
            {
                for (int col = 0; col < BoardSize; col++)
                {
                    if (string.IsNullOrEmpty(Board[row, col]))
                    {
                        emptyFields.Add((row, col));
                    }
                }
            }

            if (emptyFields.Count > 0)
            {
                var random = new Random();
                int index = random.Next(emptyFields.Count);
                return emptyFields[index];
            }

            return (-1, -1); // Kein leeres Feld gefunden
        }

        private void UpdateUI(int row, int col, string playerMark)
        {
            string elementName = $"Cell{row}{col}";
            _uiInteractionService.UpdateProperty<VisualElement>(elementName, element =>
            {
                Application.Current.Dispatcher.Dispatch(() =>
                {
                    string nextPlayerMark = playerMark == "X" ? "O" : "X";
                    _uiInteractionService.SetUITitle("Aktueller Spieler: " + nextPlayerMark);
                    if (element is Button button)
                    {
                        button.Text = playerMark; // Markiert das Feld mit "X" oder "O"
                        button.IsEnabled = false; // Verhindert weitere Klicks
                    }
                });
            });
        }

        /// <inheritdoc />
        public string CheckGameStatus()
        {
            // Prüft Zeilen, Spalten und Diagonalen
            if (HasLost("X")) return "X lost";
            if (HasLost("O")) return "O lost";

            // Prüfen, ob das Spiel unentschieden ist
            if (IsBoardFull()) return "Draw";


            return "In Progress";
        }

        private bool HasLost(string player)
        {
            // Zeilen und Spalten
            for (int i = 0; i < BoardSize; i++)
            {
                if (CountConsecutive(player, i, 0, 0, 1) >= 3 || CountConsecutive(player, 0, i, 1, 0) >= 3)
                    return true;
            }

            // Diagonalen von oben links nach unten rechts
            for (int i = 0; i < BoardSize - 2; i++) // Nur bis BoardSize - 2, damit genug Platz für 3 Felder ist
            {
                if (CountConsecutive(player, i, 0, 1, 1) >= 3 || CountConsecutive(player, 0, i, 1, 1) >= 3)
                    return true;
            }

            // Diagonalen von oben rechts nach unten links
            for (int i = 0; i < BoardSize - 2; i++) // Auch hier nur bis BoardSize - 2
            {
                if (CountConsecutive(player, i, BoardSize - 1, 1, -1) >= 3 || CountConsecutive(player, 0, BoardSize - 1 - i, 1, -1) >= 3)
                    return true;
            }

            return false;
        }

        private int CountConsecutive(string player, int startRow, int startCol, int rowInc, int colInc)
        {
            int count = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                int row = startRow + i * rowInc;
                int col = startCol + i * colInc;
                if (row >= 0 && row < BoardSize && col >= 0 && col < BoardSize && Board[row, col] == player)
                {
                    count++;
                }
                else
                {
                    count = 0; // Reset wenn kein direkter Nachbar
                }

                if (count >= 3) return count;
            }
            return count;
        }

        private bool IsBoardFull()
        {
            foreach (var cell in Board)
                if (string.IsNullOrEmpty(cell))
                    return false;
            return true;
        }

        /// <inheritdoc />
        public void ResetGame()
        {
            for (int row = 0; row < BoardSize; row++)
                for (int col = 0; col < BoardSize; col++)
                    Board[row, col] = string.Empty;

            CurrentPlayer = "X";
            _uiInteractionService.SetUITitle("");

            // UI zurücksetzen
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    string elementName = $"Cell{row}{col}";
                    _uiInteractionService.UpdateProperty<VisualElement>(elementName, element =>
                    {
                        if (element is Button button)
                        {
                            button.Text = string.Empty;
                            button.IsEnabled = true;
                        }
                    });
                }
            }
        }

        /// <inheritdoc />
        public void SaveGameState()
        {
            _gameStorageService.SaveGameState(Board, CurrentPlayer, isAgainstAI);
        }

        /// <inheritdoc />
        public void LoadGameState()
        {
            // Lade den gespeicherten Spielstand
            var (loadedBoard, loadedCurrentPlayer, isAI) = _gameStorageService.LoadGameState();
            Board = loadedBoard;
            CurrentPlayer = loadedCurrentPlayer;
            isAgainstAI = isAI;

            // Aktualisiere die UI basierend auf dem geladenen Spielstand
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    string elementName = $"Cell{row}{col}";

                    // Aktualisiere den Button entsprechend dem geladenen Spielstand
                    _uiInteractionService.UpdateProperty<VisualElement>(elementName, element =>
                    {
                        if (element is Button button)
                        {
                            string cellValue = Board[row, col]; // Geladener Wert aus dem Board

                            // Setze den Text des Buttons auf den geladenen Wert (falls leer, bleibt es leer)
                            button.Text = string.IsNullOrEmpty(cellValue) ? string.Empty : cellValue;

                            // Deaktiviere den Button, wenn das Feld bereits besetzt ist
                            button.IsEnabled = string.IsNullOrEmpty(cellValue);
                        }
                    });
                }
            }

            // Aktualisiere den Titel der UI, um den aktuellen Spieler anzuzeigen
            _uiInteractionService.SetUITitle($"Aktueller Spieler: {CurrentPlayer}");
        }



        /// <summary>
        /// Findet den besten Zug für die KI basierend auf dem Minimax-Algorithmus mit Alpha-Beta-Pruning.
        /// Die Methode durchsucht das gesamte Spielfeld und bewertet alle möglichen Züge, um den optimalen
        /// Spielzug zu ermitteln.
        /// </summary>
        /// <returns>
        /// Ein Tupel bestehend aus:
        /// - der Zeile (int) des besten Zugs,
        /// - der Spalte (int) des besten Zugs.
        /// Gibt (-1, -1) zurück, wenn kein gültiger Zug möglich ist.
        /// </returns>
        private async Task<(int, int)> FindBestMove()
        {
            int bestScore = int.MinValue; // Startwert für die beste Bewertung
            int bestRow = -1;            // Zeile des besten Zugs
            int bestCol = -1;            // Spalte des besten Zugs

            var tasks = new List<Task<(int, int, int)>>(); // Liste der parallelen Aufgaben (Tasks)

            // Quadrantengröße berechnen (Spielfeld wird in 4 Quadranten aufgeteilt)
            int quadrantSize = BoardSize / 2;

            // Starte 4 parallele Tasks, die unterschiedliche Quadranten bearbeiten
            for (int quadrant = 0; quadrant < 4; quadrant++)
            {
                int startRow = (quadrant / 2) * quadrantSize;    // Startzeile für den aktuellen Quadranten
                int startCol = (quadrant % 2) * quadrantSize;    // Startspalte für den aktuellen Quadranten
                int endRow = startRow + quadrantSize;           // Endzeile (exklusiv)
                int endCol = startCol + quadrantSize;           // Endspalte (exklusiv)


                // Task für die Berechnung im Quadranten starten

                // Anmerkung: Die Spaltung in Threads ermöglicht eine parallele Berechnung, 
                // wodurch die Suche nach dem besten Zug schneller wird. Die Genauigkeit des
                // Minimax-Algorithmus bleibt davon jedoch unbeeinflusst.
                tasks.Add(Task.Run(() =>
                {
                    // Finde den besten Zug innerhalb dieses Quadranten
                    return FindBestMoveInQuadrant(startRow, endRow, startCol, endCol);
                }));
            }

            // Ergebnisse aller Tasks abwarten
            var results = await Task.WhenAll(tasks).ConfigureAwait(false);

            // Wähle den besten Zug aus allen Ergebnissen der Quadranten
            foreach (var (score, row, col) in results)
            {
                if (score > bestScore) // Wenn eine bessere Bewertung gefunden wurde
                {
                    bestScore = score;
                    bestRow = row;
                    bestCol = col;
                }
            }

            return (bestRow, bestCol); // Rückgabe des besten Zugs
        }

        /// <summary>
        /// Berechnet den besten Zug für die KI innerhalb eines angegebenen Quadranten des Spielfelds.
        /// Die Methode verwendet den Minimax-Algorithmus, um die beste Bewertung und Position zu finden.
        /// </summary>
        /// <param name="startRow">Die Startzeile des Quadranten.</param>
        /// <param name="endRow">Die Endzeile des Quadranten (exklusiv).</param>
        /// <param name="startCol">Die Startspalte des Quadranten.</param>
        /// <param name="endCol">Die Endspalte des Quadranten (exklusiv).</param>
        /// <returns>
        /// Ein Tupel bestehend aus:
        /// - dem besten Score (int),
        /// - der Zeile (int) des besten Zugs,
        /// - der Spalte (int) des besten Zugs.
        /// </returns>
        private (int, int, int) FindBestMoveInQuadrant(int startRow, int endRow, int startCol, int endCol)
        {
            int bestScore = int.MinValue; // Startwert für die beste Bewertung
            int bestRow = -1;            // Zeile des besten Zugs im Quadranten
            int bestCol = -1;            // Spalte des besten Zugs im Quadranten

            // Erstelle eine Kopie des Spielfelds für Thread-Sicherheit
            var boardCopy = (string[,])Board.Clone();

            // Iteriere über alle Zellen im aktuellen Quadranten
            for (int row = startRow; row < endRow; row++)
            {
                for (int col = startCol; col < endCol; col++)
                {
                    // Prüfe, ob das Feld leer ist
                    if (string.IsNullOrEmpty(boardCopy[row, col]))
                    {
                        boardCopy[row, col] = "O"; // Temporär "O" platzieren (KI-Zug)
                        int score = Minimax(0, true, int.MinValue, int.MaxValue); // Bewertung berechnen
                        boardCopy[row, col] = string.Empty; // Zug zurücknehmen

                        // Aktualisiere den besten Zug, wenn eine bessere Bewertung gefunden wurde
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestRow = row;
                            bestCol = col;
                        }
                    }
                }
            }

            return (bestScore, bestRow, bestCol); // Rückgabe des besten Zugs im Quadranten
        }



        /// <summary>
        /// Komplizieres Algorithmus...
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="isMaximizing"></param>
        /// <returns></returns>
        private int Minimax(int depth, bool isMaximizing, int alpha, int beta)
        {
            string result = CheckGameStatus();
            if (result == "X lost") return 10 - depth;
            if (result == "O lost") return depth - 10;
            if (result == "Draw") return 0;
            if (IsBoardFull()) return 0;

            // Maximal erlaubte Tiefe
            const int MaxDepth = 6;

            if (depth >= MaxDepth)
            {
                return EvaluateBoard(); // Bewertet den aktuellen Zustand (z. B. Anzahl gewinnender Reihen).
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;

                for (int row = 0; row < BoardSize; row++)
                {
                    for (int col = 0; col < BoardSize; col++)
                    {
                        if (string.IsNullOrEmpty(Board[row, col]))
                        {
                            Board[row, col] = "O";
                            int score = Minimax(depth + 1, false, alpha, beta);
                            Board[row, col] = string.Empty;

                            bestScore = Math.Max(score, bestScore);
                            alpha = Math.Max(alpha, bestScore);

                            // Abschneiden, wenn der Minimizing-Player diesen Zweig ohnehin ignorieren wird
                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int row = 0; row < BoardSize; row++)
                {
                    for (int col = 0; col < BoardSize; col++)
                    {
                        if (string.IsNullOrEmpty(Board[row, col]))
                        {
                            Board[row, col] = "X";
                            int score = Minimax(depth + 1, true, alpha, beta);
                            Board[row, col] = string.Empty;

                            bestScore = Math.Min(score, bestScore);
                            beta = Math.Min(beta, bestScore);

                            // Abschneiden, wenn der Maximizing-Player diesen Zweig ohnehin ignorieren wird
                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                return bestScore;
            }
        }

        private int EvaluateBoard()
        {
            // Bewertet das aktuelle Spielfeld
            // Beispiel: +10 für jede fast vollständige Zeile/Spalte/Diagonale des Maximizing-Players,
            // -10 für jede des Minimizing-Players.
            int score = 0;

            // Für Reihen:
            for (int row = 0; row < BoardSize; row++)
            {
                int maximizingCount = 0;
                int minimizingCount = 0;
                for (int col = 0; col < BoardSize; col++)
                {
                    if (Board[row, col] == "O") maximizingCount++;
                    if (Board[row, col] == "X") minimizingCount++;
                }
                if (maximizingCount > 0 && minimizingCount == 0) score += maximizingCount;
                if (minimizingCount > 0 && maximizingCount == 0) score -= minimizingCount;
            }

            return score;
        }



    }
}
