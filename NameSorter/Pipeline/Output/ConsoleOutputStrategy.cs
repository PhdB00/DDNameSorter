using DD.NameSorter.Infrastructure;

namespace DD.NameSorter.Pipeline.Output;

/// <summary>
/// ConsoleOutputStrategy implements the <see cref="IOutputStrategy"/> 
/// and is responsible for outputting the names of a collection of Person objects to the console.
/// </summary>
/// <remarks>
/// This class utilizes an <see cref="IConsoleWriter"/> to abstract console writing, making
/// it easier to unit test and allowing for dependency injection of custom console writers.
/// </remarks>
public class ConsoleOutputStrategy(IConsoleWriter consoleWriter) 
    : IOutputStrategy
{
    public void Output(IEnumerable<Person> people)
    {
        foreach (var person in people)
        {
            consoleWriter.WriteLine(person.ToString());
        }
    }
}