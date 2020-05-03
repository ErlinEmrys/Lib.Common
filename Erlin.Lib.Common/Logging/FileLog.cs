using System;
using System.Diagnostics;
using System.IO;
using System.Text;

using Erlin.Lib.Common.Reflection;
using Erlin.Lib.Common.Time;

namespace Erlin.Lib.Common.Logging
{
    /// <summary>
    /// System for logging to file
    /// </summary>
    public class FileLog : ILog
    {
        private readonly string _logDirectory;
        private readonly object _syncRoot = new object();
        private DateTime _currentFileDate;

        private bool _disposed;
        private FileStream? _stream;
        private StreamWriter? _writer;

        /// <summary>
        /// Minimal log threshold level
        /// </summary>
        public TraceLevel MinLogThreshold { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="minLogThreshold">Minimal log threshold level</param>
        /// <param name="logDirectory">Directory for log files</param>
        public FileLog(TraceLevel minLogThreshold, string? logDirectory = null)
        {
            MinLogThreshold = minLogThreshold;
            if (string.IsNullOrEmpty(logDirectory))
            {
                _logDirectory = Path.Combine(AssemblyHelper.BaseLocation, "Log");
            }
            else
            {
                _logDirectory = logDirectory;
            }
        }

        /// <summary>
        /// Release all resources
        /// </summary>
        public void Dispose()
        {
            lock (_syncRoot)
            {
                _disposed = true;
                CloseFile();
            }
        }

        /// <summary>
        /// Log any exception
        /// </summary>
        /// <param name="level">Level of the event</param>
        /// <param name="eventTime">Time of the event</param>
        /// <param name="message">Error message to log</param>
        public void Log(TraceLevel level, DateTime eventTime, string message)
        {
            if (!_disposed && MinLogThreshold != TraceLevel.Off && level <= MinLogThreshold)
            {
                string text = $"[{level.ToString().Substring(0, 1)}][{eventTime.ToString(DateTimeHelper.FORMAT_TIME_TICKS)}]{message}";
                lock (_syncRoot)
                {
                    if (!_disposed)
                    {
                        OpenFileToWrite(eventTime.Date);
                        _writer?.WriteLine(text);
                        _writer?.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// Ensure open log file based on date
        /// </summary>
        /// <param name="date">Date</param>
        private void OpenFileToWrite(DateTime date)
        {
            if (date != _currentFileDate)
            {
                CloseFile();

                if (!Directory.Exists(_logDirectory))
                {
                    Directory.CreateDirectory(_logDirectory);
                }

                string fileName = $"Log_{date.ToString(DateTimeHelper.FORMAT_DATE).Replace('.', '_')}.txt";
                string logPath = Path.Combine(_logDirectory, fileName);
                _stream = File.Open(logPath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
                _stream.Seek(0, SeekOrigin.End);
                _writer = new StreamWriter(_stream, Encoding.UTF8);
                _currentFileDate = date;
            }
        }

        /// <summary>
        /// Close current opened log file
        /// </summary>
        private void CloseFile()
        {
            _writer?.Flush();
            _stream?.Flush();
            _writer?.Dispose();
            _stream?.Dispose();
            _stream = null;
            _writer = null;
        }
    }
}