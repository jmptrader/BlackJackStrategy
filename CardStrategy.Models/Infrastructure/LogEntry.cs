using System;
using System.Collections.Generic;
using System.Text;

namespace CardStrategy.Models
{
    public class LogEntry
    {
        public LogEntry(string message)
        {
            Message = message;
            TimeStamp = DateTime.Now;
        }

        public DateTime TimeStamp { get; set; }
        public string Message { get; set; }
    }
}
