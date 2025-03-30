namespace DD.NameSorter.Pipeline;

/// <summary>
/// This attribute is applied to classes that implement <see cref="IPipelineStep"/> and
/// specifies the order in which a pipeline step should be executed.
/// </summary>
/// <remarks>
/// The <see cref="Order"/> property determines the sequence of execution for pipeline steps
/// and the values for the Order are maintained in <see cref="PipelineStepOrders"/>.
/// </remarks>
[AttributeUsage(AttributeTargets.Class)]
public class PipelineStepOrderAttribute(int order) 
    : Attribute
{
    public int Order { get; } = order;
}