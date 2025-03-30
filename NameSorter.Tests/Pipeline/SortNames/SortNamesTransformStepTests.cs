using DD.NameSorter;
using DD.NameSorter.Pipeline.SortNames;
using NSubstitute;

namespace NameSorter.Tests.Pipeline.SortNames;

[TestFixture]
public class SortNamesTransformStepTests
{
    [Test]
    public void Process_Should_SortNames()
    {
        // Arrange
        var nameSorter = Substitute.For<INameSorter>();  
        var step = new SortNamesTransformStep(nameSorter);
        
        // Act
        step.Process(new List<Person>());
        
        // Assert
        nameSorter.Received().Sort(Arg.Any<List<Person>>());
    }
}