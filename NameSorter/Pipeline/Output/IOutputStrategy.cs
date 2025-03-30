namespace DD.NameSorter.Pipeline.Output;

/// <summary>
/// Defines a strategy for outputting a collection of Person objects.
/// </summary>
public interface IOutputStrategy
{
    void Output(IEnumerable<Person> people);
}