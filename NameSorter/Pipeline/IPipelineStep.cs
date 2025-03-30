namespace DD.NameSorter.Pipeline;

/// <summary>
/// Represents a step in the pipeline.
/// Steps must have unique order values defined by <see cref="PipelineStepOrderAttribute"/>.
/// </summary>
public interface IPipelineStep;

/// <summary>
/// Represents a pipeline step responsible for extracting data from a source.
/// Extraction steps are used to gather initial data and return it as a type <see cref="IEnumerable{Person}"/>
/// for further processing in the pipeline.
/// </summary>
public interface IPipelineExtractStep
{
    IEnumerable<Person> Process();
}

/// <summary>
/// Represents a pipeline step responsible for transforming data.
/// Transformation steps take an input of type <see cref="IEnumerable{Person}"/> and
/// return a transformed output of the same type.
/// </summary>
public interface IPipelineTransformStep
{
    IEnumerable<Person> Process(IEnumerable<Person> people);
}