namespace DD.NameSorter.Pipeline.Output;

/// <summary>
/// Represents a pipeline step responsible for processing a collection of Person objects through
/// all available output strategies.
/// </summary>
/// <remarks>
/// The OutputNamesTransformStep is an implementation of the IPipelineStep and
/// IPipelineTransformStep interfaces.
/// </remarks>
[PipelineStepOrder(PipelineStepOrders.Output)]
public class OutputNamesTransformStep(IEnumerable<IOutputStrategy> outputStrategies) 
    : IPipelineStep, IPipelineTransformStep
{
    public IEnumerable<Person> Process(IEnumerable<Person> people)
    {
        var peopleList = people.ToList();
        foreach (var strategy in outputStrategies)
        {
            strategy.Output(peopleList);
        }
        return peopleList;
    }
}