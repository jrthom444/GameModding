using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StatiK.Utils
{
    public interface ILogger
    {
        void Info(string value);
        void Debug(string value);
        void Trace(string value);
        void Error(string value);
    }

    public enum LogLevels { ERROR = 0, INFO = 1, DEBUG = 2, TRACE = 3};

    public class StatikLogManager
    {
        private LogLevels _logLevel;
        public static StatikLogManager _instance;
        private readonly Dictionary<string, Logger> _loggers;
        private List<LogEntry> _entries;
        private string _preProcessedOutput;
        private StringBuilder _strBuilder;
    
        private StatikLogManager() 
        {
            _preProcessedOutput = "";
            _loggers = new Dictionary<string, Logger>();
            _logLevel = LogLevels.INFO;
            _entries = new List<LogEntry>();
            _strBuilder = new StringBuilder();
        }

        public LogLevels LogLevel
        {
            get
            {
                return _logLevel;
            }
            set
            {
                _logLevel = value;
                foreach (KeyValuePair<string, Logger> entry in _loggers)
                {
                    entry.Value.CurrentLogLevel = _logLevel;
                }
            }
        }
        
        public static StatikLogManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StatikLogManager();
                }
                return _instance;
            }
        }

        public void OnLogEvent(object sender, LogEntry entry)
        {
            _strBuilder.Append(entry.ToString());
            _preProcessedOutput = _strBuilder.ToString();
        }

        public void Clear()
        {
            _strBuilder.Length = 0;
            _preProcessedOutput = "";
        }

        public string GetLogEntriesAsString()
        {
            return _preProcessedOutput;
        }
        
        public ILogger GetLogger(string logName)
        {
            if(_loggers.ContainsKey(logName))
            {
                return _loggers[logName];
            }
            else
            {
                Logger logger = new Logger(logName);
                logger.LogEvent += OnLogEvent;
                logger.CurrentLogLevel = _logLevel;
                _loggers.Add(logName, logger);
                return logger;
            }
        }
        
    }

    public class LogEntry : EventArgs
    {
        public string Name { get; set; }
        public LogLevels Type { get; set; }
        public string TimeStamp { get; set; }
        public string Content { get; set; }

        public override string ToString()
        {
            return String.Format("[{0} - {1}][{2}] : {3}\r\n", Name, StrLogLevel(), TimeStamp, Content);
        }

        private string StrLogLevel()
        {
            switch (Type)
            {
                case LogLevels.DEBUG:
                    return "DEBUG";
                case LogLevels.INFO:
                    return "INFO";
                case LogLevels.TRACE:
                    return "TRACE";
                default:
                    return "ERROR";
            }
        }
    }

    public class Logger : ILogger
    {
        string _name;

        public event EventHandler<LogEntry> LogEvent;

        public Logger(string name)
        {
            _name = name;
        }

        public LogLevels CurrentLogLevel { get; set; }

        private void AddLogEntry(LogLevels type, string value)
        {
            
            LogEntry entry = new LogEntry
                {
                    Name = _name,
                    Type = type,
                    TimeStamp = DateTime.Now.ToString("hh:mm:ss"),
                    Content = value
                };
            
            if (LogEvent != null)
            {
                LogEvent.Invoke(this, entry);
            }
            UnityEngine.Debug.Log(entry.ToString());

        }

        public void Info(string value)
        {
            
            if (LogLevels.INFO <= CurrentLogLevel)
            {
                AddLogEntry(LogLevels.INFO, value);
            };
        }

        public void Debug(string value)
        {
            if (LogLevels.DEBUG <= CurrentLogLevel)
            {
                AddLogEntry(LogLevels.DEBUG, value);
            };
        }

        public void Trace(string value)
        {
            if (LogLevels.TRACE <= CurrentLogLevel)
            {
                AddLogEntry(LogLevels.TRACE, value);
            };
        }

        public void Error(string value)
        {
            if (LogLevels.ERROR <= CurrentLogLevel)
            {
                AddLogEntry(LogLevels.ERROR, value);
            };
        }

    }

}
