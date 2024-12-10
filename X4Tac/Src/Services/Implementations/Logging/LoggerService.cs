using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Models.Backend.Logging;
using X4Tac.Src.Services.Interfaces.Logging;

namespace X4Tac.Src.Services.Implementations.Logging
{
    /// <summary>
    /// Wird in diesem Projekt nicht verwendet(unnötig/unpassend/ungeeignet) ist aber eine extra Implementation von mir!
    /// </summary>
    public class LoggerService : ILoggerService
    {
        private readonly List<LogEntry> _logPool;

        public LoggerService()
        {
            _logPool = new List<LogEntry>();
        }

        public void Log(string message, LogLevel logLevel = LogLevel.Info)
        {
            var logEntry = new LogEntry(message, logLevel);
            _logPool.Add(logEntry);

            // Ausgabe im Debug-Fenster während der Entwicklung.
            // Benutze ich persönlich nie, weil Ich eher mit Breakpoints debugge. Ausnahmen sind bei anderen Programmiersprachen.
#if DEBUG
            Debug.WriteLine($"[{logEntry.Timestamp}] [{logEntry.Level}] {logEntry.Message}");
#endif
        }

        public List<LogEntry> GetLogs()
        {
            return new List<LogEntry>(_logPool); // Rückgabe einer Kopie, um Mutationen zu vermeiden
        }

        public void ClearLogs()
        {
            _logPool.Clear();
        }
    }
}
