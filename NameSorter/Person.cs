namespace DD.NameSorter;

/// <summary>
/// Represents a person with a Last Name and between one and three Given Names.
/// Overrides ToString() to get their full name as a string.
/// </summary>
public class Person
{
    public IReadOnlyList<string> GivenNames { get; }
    public string LastName { get; }

    public Person(IEnumerable<string> givenNames, string lastName)
    {
        if (givenNames == null) throw new ArgumentNullException(nameof(givenNames));
        if (string.IsNullOrWhiteSpace(lastName)) throw new ArgumentNullException(nameof(lastName));

        var givenNamesList = givenNames.ToList();
        if (givenNamesList.Count == 0) throw new ArgumentException("At least one given name is required.", nameof(givenNames));
        if (givenNamesList.Count > 3) throw new ArgumentException("Maximum three given names are allowed.", nameof(givenNames));

        GivenNames = givenNamesList;
        LastName = lastName;
    }

    public override string ToString()
    {
        return $"{string.Join(" ", GivenNames)} {LastName}";
    }
}