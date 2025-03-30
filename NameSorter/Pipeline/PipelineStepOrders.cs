namespace DD.NameSorter.Pipeline;

/// <summary>
/// Provides ordering constants for pipeline steps.
/// These constants define the sequence in which pipeline steps should be executed.
/// The pipeline will execute steps in ascending order.
/// </summary>
public static class PipelineStepOrders
{
    public const int Read = 100;
    public const int Sort = 200;
    public const int Output = 300;
}