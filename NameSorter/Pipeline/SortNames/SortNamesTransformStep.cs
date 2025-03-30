namespace DD.NameSorter.Pipeline.SortNames;

/// <summary>
/// Represents a pipeline step for sorting a collection of names.
/// The class utilizes an instance of <see cref="INameSorter"/> to sort a list of
/// <see cref="Person"/> objects.
/// </summary>
/// <remarks>
/// This class is part of a processing pipeline and defines sorting as its specific operation.
/// The order of this step is defined by the <see cref="PipelineStepOrderAttribute"/> with a value indicating it as a sorting step.
/// </remarks>
[PipelineStepOrder(PipelineStepOrders.Sort)]
public class SortNamesTransformStep(INameSorter nameSorter) 
    : IPipelineStep, IPipelineTransformStep
{
    public IEnumerable<Person> Process(IEnumerable<Person> people)
    {
        return nameSorter.Sort(people);
    }
}