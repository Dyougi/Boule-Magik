using System.IO;

public static class LogManager {

    static string pathLog = "log";

    public static void LogMessageToFile(string msg, bool newLine = true)
    {
        StreamWriter sw = File.AppendText(pathLog);
        try
        {
            sw.Write(newLine == true ? msg + "\n" : msg);
        }
        finally
        {
            sw.Close();
        }
    }

    public static void LogFileClean()
    {
        File.WriteAllText(pathLog, string.Empty);
    }
}
