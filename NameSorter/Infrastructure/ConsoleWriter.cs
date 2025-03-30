namespace DD.NameSorter.Infrastructure;

/// <summary>
/// Represents an interface for writing output to the console.
/// </summary>
/// <remarks>
/// We are essentially just wrapping the Console WriteLine; this abstraction
/// is useful if we wish to subsequently change the output mechanism or mock it for testing.
/// </remarks>
public interface IConsoleWriter
{
    void WriteLine(string line);
}

/// <summary>
/// Provides functionality to write output to the console, implementing the IConsoleWriter.
/// </summary>
public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string line)
    {
        Console.WriteLine(line);
    }
}