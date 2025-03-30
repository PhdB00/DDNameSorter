namespace DD.NameSorter.Infrastructure;

/// <summary>
/// Defines the operations for interacting with the file system.
/// This interface provides methods for reading, writing,
/// and verifying the existence of files.
/// </summary>
/// <remarks>
/// We are essentially just wrapping the System.IO.File; this abstraction
/// is useful if we wish to subsequently change the output mechanism or mock it for testing.
/// </remarks>
public interface IFileSystem
{
    string[] ReadAllLines(string path);
    void WriteAllLines(string path, IEnumerable<string> contents);
    bool Exists(string path);
}

/// <summary>
/// Provides functionality to read/write output to the file system, implementing the IFileSystem.
/// </summary>
public class FileSystem : IFileSystem
{
    public string[] ReadAllLines(string path)
    {
        return File.ReadAllLines(path);
    }

    public void WriteAllLines(string path, IEnumerable<string> contents)
    {
        File.WriteAllLines(path, contents);
    }

    public bool Exists(string path)
    {
        return File.Exists(path);
    }
}