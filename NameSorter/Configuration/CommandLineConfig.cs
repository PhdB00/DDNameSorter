using System.CommandLine;

namespace DD.NameSorter.Configuration;

/// <summary>
/// Represents the configuration parsed from the command line for the application.
/// </summary>
/// <remarks>
/// This interface defines the contract for accessing the Input File (names to be sorted),
/// Output File (where the sorted names should be written), and the validity status of the
/// provided command-line arguments.
/// </remarks>
public interface ICommandLineConfig
{
    /// <summary>
    /// Gets the Input file path provided by the user via command line argument.
    /// Represents the file that contains the data to be processed by the application.
    /// Check IsValid before accessing InputFile to ensure it's usable.
    /// </summary>
    string InputFile { get; }
    /// <summary>
    /// Gets the Output file path provided by the user via command line option.
    /// Represents the file where the application's processed data will be written.
    /// Check IsValid before accessing OutputFile to ensure it's usable.
    /// </summary>
    string OutputFile { get; }
    /// <summary>
    /// Indicates whether the supplied command line arguments are considered valid.
    /// NOTE: Does not indicate that files exist or that format is correct, just that
    /// command-line arguments have been provided. 
    /// </summary>
    bool IsValid { get; }
}

/// <summary>
/// Handles command-line configuration for the application, per the ICommandLineConfig interface.
/// </summary>
public class CommandLineConfig : ICommandLineConfig
{
    public string InputFile { get; }
    public string OutputFile { get; }
    public bool IsValid { get; }

    public CommandLineConfig(string[] args)
    {
        var command = new NameSorterCommand();
        var result = command.Invoke(args);
        
        InputFile = command.InputFile;
        OutputFile = command.OutputFile;
        
        IsValid = result == 0 && 
                  !string.IsNullOrEmpty(InputFile) && 
                  !string.IsNullOrEmpty(OutputFile);
    }
}