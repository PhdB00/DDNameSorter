using System.CommandLine;

namespace DD.NameSorter.Configuration;

/// <summary>
/// Implementation of the arguments & options that the application expects in the
/// command line.
/// </summary>
/// <remarks>
/// The arguments have limited validatation and/or default values to ensure that,
/// as minimum, *values* are available to the exposed properties.
/// </remarks>
public class NameSorterCommand : RootCommand
{
    /// <summary>
    /// Gets the path to the input file containing the names to be sorted.
    /// </summary>
    /// <remarks>
    /// The value is populated from the command-line arguments provided by the user
    /// when running the application. It represents the file to read unsorted names from.
    /// </remarks>
    public string InputFile { get; private set; } = null!;

    /// <summary>
    /// Gets the path to the output file where the sorted names will be written.
    /// </summary>
    /// <remarks>
    /// This value is determined based on the command-line option provided by the user.
    /// If not specified, a default file name ("sorted-names-list.txt"), is used.
    /// 
    /// While specifying the output file via command-line was not part of the
    /// specification we make it an option with default value here to maintain flexibility
    /// and follow the principle of "least surprise" for developers :-).
    /// </remarks>
    public string OutputFile { get; private set; } = null!;

    public NameSorterCommand()
        : base("Name Sorter application")
    {
        var sourceFileArgument = new Argument<string>(
            name: "file",
            description: "The file containing names to sort");
        
        var outputFileOption = new Option<string>(
            name: "output",
            description: "The file containing the sorted names",
            getDefaultValue: () => "sorted-names-list.txt"
        )
        {
            IsRequired = false
        };

        AddArgument(sourceFileArgument);
        AddOption(outputFileOption);
        
        this.SetHandler((sourceFile, outputFile) =>
        {
            InputFile = sourceFile.Trim();
            OutputFile = outputFile.Trim();
        }, 
            sourceFileArgument, 
            outputFileOption);
    }
}