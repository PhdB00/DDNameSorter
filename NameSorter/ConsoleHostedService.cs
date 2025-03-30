using DD.NameSorter.Configuration;
using DD.NameSorter.Pipeline;
using Microsoft.Extensions.Hosting;

namespace DD.NameSorter;

/// <summary>
/// A hosted service that represents the primary execution pipeline for the application.
/// </summary>
/// <remarks>
/// This service will validate the command-line configuration and may terminate if they are not valid.
/// The service will initialize and execute the processing pipeline constructed by the <see cref="IPipelineBuilder"/>
/// and terminating the application after the pipeline processing is complete.
/// </remarks>
public class ConsoleHostedService(
    IHostApplicationLifetime lifetime,
    ICommandLineConfig config,
    IPipelineBuilder pipelineBuilder
    ) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (!config.IsValid || config is { InputFile: null })
        {
            Console.WriteLine("Command line arguments are not valid.");
            lifetime.StopApplication();
            return Task.CompletedTask;
        }

        var pipeline = pipelineBuilder.Build();
        pipeline.ProcessPipeline();
        
        lifetime.StopApplication();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}