namespace DD.NameSorter.Pipeline.SortNames;

/// <summary>
/// Provides functionality for sorting a collection of <see cref="Person"/> objects.
/// </summary>
public interface INameSorter
{
    IEnumerable<Person> Sort(IEnumerable<Person> people);
}

/// <summary>
/// Implements the <see cref="INameSorter"/> interface to provide functionality for sorting a
/// collection of <see cref="Person"/> objects by last name and given names in ascending order.
/// </summary>
public class NameSorter : INameSorter
{
    public IEnumerable<Person> Sort(IEnumerable<Person> people)
    {
        return people
            .OrderBy(p => p.LastName)
            .ThenBy(p => string.Join(" ", p.GivenNames));
    }
}