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
        IsValidFilePath(path);
        return File.ReadAllLines(path);
    }

    public void WriteAllLines(string path, IEnumerable<string> contents)
    {
        IsValidFilePath(path);
        File.WriteAllLines(path, contents);
    }

    public bool Exists(string path)
    {
        IsValidFilePath(path);
        return File.Exists(path);
    }

    private static void IsValidFilePath(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Path cannot be null or whitespace.", nameof(path));

        try
        {
            // GetFullPath will throw an exception if there are invalid characters
            Path.GetFullPath(path);
            
            string? directoryName = Path.GetDirectoryName(path);
            if (directoryName != null && directoryName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
                throw new ArgumentException("Path contains invalid characters.", nameof(path));
            
            string fileName = Path.GetFileName(path);
            if (string.IsNullOrWhiteSpace(fileName) ||
                fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0)
                throw new ArgumentException("Path contains invalid characters.", nameof(path));
            
        }
        catch (Exception)
        {
            throw new ArgumentException("Path is invalid.", nameof(path));
        }
    }
}