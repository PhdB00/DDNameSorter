using DD.NameSorter.Infrastructure;

namespace NameSorter.Tests.Infrastructure;

[TestFixture]
public class FileSystemTests
{
    private IFileSystem fileSystem;
    private string testDirectory;
    private string testFilePath;
    private readonly string[] testContent = ["Line 1", "Line 2", "Line 3"];

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        testDirectory = Path.Combine(Path.GetTempPath(), "FileSystemTests");
        Directory.CreateDirectory(testDirectory);
    }

    [SetUp]
    public void Setup()
    {
        fileSystem = new FileSystem();
        testFilePath = Path.Combine(testDirectory, $"test_{Guid.NewGuid()}.txt");
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(testFilePath))
        {
            File.Delete(testFilePath);
        }
    }

    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (Directory.Exists(testDirectory))
        {
            Directory.Delete(testDirectory, recursive: true);
        }
    }

    [Test]
    public void ReadAllLines_WhenFileExists_ReturnsCorrectContent()
    {
        // Arrange
        File.WriteAllLines(testFilePath, testContent);

        // Act
        var result = fileSystem.ReadAllLines(testFilePath);

        // Assert
        Assert.That(result, Is.EqualTo(testContent));
    }

    [Test]
    public void ReadAllLines_WhenFileDoesNotExist_ThrowsFileNotFoundException()
    {
        // Arrange
        var nonExistentFile = Path.Combine(testDirectory, "nonexistent.txt");

        // Act & Assert
        Assert.Throws<FileNotFoundException>(() => fileSystem.ReadAllLines(nonExistentFile));
    }

    [Test]
    public void WriteAllLines_CreatesFileWithCorrectContent()
    {
        // Act
        fileSystem.WriteAllLines(testFilePath, testContent);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(File.Exists(testFilePath), Is.True);
            Assert.That(File.ReadAllLines(testFilePath), Is.EqualTo(testContent));
        });
    }

    [Test]
    public void WriteAllLines_WithEmptyContent_CreatesEmptyFile()
    {
        // Arrange
        var emptyContent = Array.Empty<string>();

        // Act
        fileSystem.WriteAllLines(testFilePath, emptyContent);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(File.Exists(testFilePath), Is.True);
            Assert.That(File.ReadAllLines(testFilePath), Is.Empty);
        });
    }

    [Test]
    public void WriteAllLines_ToReadOnlyFile_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        File.WriteAllText(testFilePath, "Initial content");
        File.SetAttributes(testFilePath, FileAttributes.ReadOnly);

        // Act & Assert
        Assert.Throws<UnauthorizedAccessException>(() => 
            fileSystem.WriteAllLines(testFilePath, testContent));

        // Cleanup
        File.SetAttributes(testFilePath, FileAttributes.Normal);
    }

    [Test]
    public void Exists_WithExistingFile_ReturnsTrue()
    {
        // Arrange
        File.WriteAllText(testFilePath, "Test content");

        // Act
        var result = fileSystem.Exists(testFilePath);

        // Assert
        Assert.That(result, Is.True);
    }

    [Test]
    public void Exists_WithNonExistentFile_ReturnsFalse()
    {
        // Arrange
        var nonExistentFile = Path.Combine(testDirectory, "nonexistent.txt");

        // Act
        var result = fileSystem.Exists(nonExistentFile);

        // Assert
        Assert.That(result, Is.False);
    }

    [Test]
    public void WriteAllLines_WithInvalidPath_ThrowsDirectoryNotFoundException()
    {
        // Arrange
        var invalidPath = Path.Combine(testDirectory, "nonexistent_dir", "test.txt");

        // Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => 
            fileSystem.WriteAllLines(invalidPath, testContent));
    }

    [Test]
    public void ReadAllLines_WithInvalidPath_ThrowsDirectoryNotFoundException()
    {
        // Arrange
        var invalidPath = Path.Combine(testDirectory, "nonexistent_dir", "test.txt");

        // Act & Assert
        Assert.Throws<DirectoryNotFoundException>(() => 
            fileSystem.ReadAllLines(invalidPath));
    }
    
    [TestCase("")]
    [TestCase(" ")]
    public void ReadAllLines_WithInvalidPathString_ThrowsArgumentException(string invalidPath)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => fileSystem.ReadAllLines(invalidPath));
    }
    
    [TestCase("")]
    [TestCase(" ")]
    public void WriteAllLines_WithInvalidPathString_ThrowsArgumentException(string invalidPath)
    {
        // Act & Assert
        Assert.Throws<ArgumentException>(() => 
            fileSystem.WriteAllLines(invalidPath, testContent));
    }
}