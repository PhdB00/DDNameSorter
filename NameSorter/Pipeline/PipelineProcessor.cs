namespace DD.NameSorter.Pipeline;

/// <summary>
/// Defines the contract for a pipeline processor that is responsible for executing
/// a series of pipeline steps in the specified order. The processor coordinates
/// the extraction and transformation of data through these steps.
/// </summary>
public interface IPipelineProcessor
{
    void ProcessPipeline();
}

/// <summary>
/// Implements the <see cref="IPipelineProcessor"/> interface to handle the execution
/// of a series of pipeline steps specified as <see cref="IPipelineStep"/> implementations.
/// </summary>
/// <remarks>
/// The <see cref="PipelineProcessor"/> processes a sequence of pipeline steps, which
/// can include both extraction and transformation steps. Each step is executed in order,
/// and the output of one step can be passed as input to the next step in the pipeline.
/// If no data remains after execution of a step, the pipeline terminates.
/// </remarks>
public class PipelineProcessor(
    IEnumerable<IPipelineStep> steps) 
    : IPipelineProcessor
{
    public void ProcessPipeline()
    {
        IEnumerable<Person>? people = null;
        foreach (var step in steps)
        {
            switch (step)
            {
                case IPipelineExtractStep extractStep:
                    Extract(extractStep);
                    break;
                case IPipelineTransformStep transformStep:
                    Transform(transformStep);
                    break;
            }

            if (people is null || !people.Any())
            {
                break;
            }
        }

        return;

        void Extract(IPipelineExtractStep extract) =>
            people = extract.Process();

        void Transform(IPipelineTransformStep transform) =>
            people = transform.Process(people);
    }
}