namespace Persistence;

internal static class Logger<T>
    where T : class
{
    
    public static void LogSuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"{typeof(T).Name}: {message}");
        Console.ResetColor();
    }

    public static void LogInfo(string message)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"{typeof(T).Name}: {message}");
        Console.ResetColor();
    }

    public static void LogError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"{typeof(T).Name}: {message}");
        Console.ResetColor();
    }

}