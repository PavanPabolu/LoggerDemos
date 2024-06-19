//using Microsoft.Extensions.Logging;

using System.Text;

namespace Logger.HTTPlogging.WebAPI.Common
{
    public class FileLogger : ILogger
    {
        private readonly string _filePath; //****
        private static object _lock = new object(); //***

        public FileLogger(string filePath) //***
        {
            _filePath = filePath;
        }

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (formatter != null) //******
            {
                lock (_lock)
                {
                    var msg = $"\n{DateTime.Now}:{formatter(state, exception)} {Environment.NewLine}";
                    File.AppendAllText(_filePath, msg);
                }
            }
        }
    }

    //-------------------------------------------------
    public class FileLoggerProvider : ILoggerProvider
    {
        private readonly string _filePath;

        public FileLoggerProvider(string filePath) //**
        {
            _filePath = filePath;
            CreateFileWithDirectories(_filePath);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(_filePath); //***
        }

        public void Dispose() { }




        private void CreateFileWithDirectories(string path)
        {
            path = path ?? $"{Path.GetTempPath()}/Logs/app-{DateTime.Now.ToString("mmss")}.txt";
            string directoryPath = Path.GetDirectoryName(path);

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
            }
        }

        private void CreateFileWithDirectories(string path, string content)
        {
            path = path ?? $"{Path.GetTempPath()}/Logs/app-{DateTime.Now.ToString("mmss")}.txt";

            // Get the directory path from the file path
            string directoryPath = Path.GetDirectoryName(path);

            // Check if the directory exists, if not, create it
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            // Create the file and write content to it
            File.WriteAllText(path, content);
        }


        private void CreateLogFileIfNotExists(string path)
        {
            path = path ?? $"{Path.GetTempPath()}/Logs/app-{DateTime.Now.ToString("mmss")}.txt";

            // Check if the file exists
            if (!File.Exists(path))
            {
                // Create the file
                using (FileStream fs = File.Create(path))
                {
                    // Optionally, write some text to the file
                    byte[] info = new UTF8Encoding(true).GetBytes("This is some text in the file.");
                    fs.Write(info, 0, info.Length);
                }

                Console.WriteLine("File created successfully.");
            }
        }
    }
}
