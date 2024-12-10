using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Models.Backend.Logging;

namespace X4Tac.Src.Services.Interfaces.Logging
{
    public interface ILoggerService
    {
        /// <summary>
        /// Fügt eine Nachricht zum Log-Pool hinzu.
        /// </summary>
        /// <param name="message">Die zu loggende Nachricht.</param>
        /// <param name="logLevel">Die Log-Ebene (z. B. Info, Warn, Error).</param>
        void Log(string message, LogLevel logLevel = LogLevel.Info);

        /// <summary>
        /// Ruft die aktuellen Logs ab.
        /// </summary>
        /// <returns>Eine Liste aller gespeicherten Logs.</returns>
        List<LogEntry> GetLogs();

        /// <summary>
        /// Löscht alle gespeicherten Logs.
        /// </summary>
        void ClearLogs();
    }

    /// <summary>
    /// Stellt verschiedene Log-Ebenen bereit.
    /// </summary>
    public enum LogLevel
    {
        Info,
        Warning,
        Error,
        Debug
    }
}
