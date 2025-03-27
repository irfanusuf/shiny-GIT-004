using System;

namespace WebApplication1.Middlewares;

// public static class console
// {
//     public static void log(string ex ,string message)
//     {
//         string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
//         Directory.CreateDirectory(logPath); // Ensure folder exists
        
//         string logFile = Path.Combine(logPath, $"log-{DateTime.Today:yyyyMMdd}.txt");
//         string logMessage = $"{DateTime.Now:HH:mm:ss} [ERROR] {message}\n{ex}\n\n";
        
//         File.AppendAllText(logFile, logMessage);
//     }
// }   



public static class console
{
    private static readonly object _lock = new object();

    public static void log(string ex , string message)
    {
        lock (_lock) // Ensures only one thread writes at a time
        {
            string logPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            Directory.CreateDirectory(logPath);
            
            string logFile = Path.Combine(logPath, $"log-{DateTime.Today:yyyyMMdd}.txt");
            string logMessage = $"{DateTime.Now:HH:mm:ss} [ERROR] {message}\n{ex}\n\n";
            
            File.AppendAllText(logFile, logMessage);
        }
    }
}

