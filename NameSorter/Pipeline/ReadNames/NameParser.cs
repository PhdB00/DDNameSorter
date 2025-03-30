namespace DD.NameSorter.Pipeline.ReadNames;

/// <summary>
/// Defines a contract for parsing a string, represeting a full name, into a <see cref="Person"/> object.
/// </summary>
public interface INameParser
{
    Person ParseName(string fullName);
}

/// <summary>
/// Implements the <see cref="INameParser"/> interface to parse a string into a <see cref="Person"/>.
/// </summary>
/// <remarks>
/// Parses the input string to separate given names and the last name.
/// Ensures the name follows requirements such as containing at least two parts (a given name and a last name),
/// and restricts the total number of parts that may comprise a given name.
/// </remarks>
public class NameParser : INameParser
{
    public Person ParseName(string fullName)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Full name cannot be empty.", nameof(fullName));

        var nameParts = fullName.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        
        if (nameParts.Length < 2)
            throw new ArgumentException("Name must contain at least one given name and one last name.", nameof(fullName));
        
        if (nameParts.Length > 4)
            throw new ArgumentException("Name cannot contain more than three given names and one last name.", nameof(fullName));

        var lastName = nameParts[^1];
        var givenNames = nameParts[..^1].ToList();

        return new Person(givenNames, lastName);
    }
}
