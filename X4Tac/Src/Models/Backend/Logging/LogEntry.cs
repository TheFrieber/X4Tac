using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X4Tac.Src.Services.Interfaces.Logging;

namespace X4Tac.Src.Models.Backend.Logging
{
    /// <summary>
    /// Eine Log-Nachricht mit Zeitstempel und Ebene.
    /// </summary>
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
        public LogLevel Level { get; set; }

        public LogEntry(string message, LogLevel level)
        {
            Timestamp = DateTime.Now;
            Message = message;
            Level = level;
        }
    }
}
