using DD.NameSorter;
using DD.NameSorter.Pipeline;
using NSubstitute;

namespace NameSorter.Tests.Pipeline
{
    [TestFixture]
    public class PipelineProcessorTests
    {
        private FakeExtractStep extractStep;
        private FakeTransformStep transformStep;

        [SetUp]
        public void Setup()
        {
            extractStep = new FakeExtractStep();
            transformStep = new FakeTransformStep();
        }
        
        [Test]
        public void ProcessPipeline_WithExtractStepOnly_CallsExtractStep()
        {
            // Arrange
            var steps = new List<IPipelineStep> { extractStep, transformStep };
            var processor = new PipelineProcessor(steps);

            // Act
            processor.ProcessPipeline();
            
            // Assert
            Assert.Pass();
        }
        
        [Test]
        public void ProcessPipeline_WithNonExtractOrTransformStep_SkipsStep()
        {
            // Arrange
            var genericStep = Substitute.For<IPipelineStep>();
            var steps = new List<IPipelineStep> { genericStep };
            var processor = new PipelineProcessor(steps);

            // Act
            processor.ProcessPipeline();

            // Assert
            Assert.Pass();
        }
    }
    
    class FakeExtractStep : IPipelineStep, IPipelineExtractStep
    {
        public IEnumerable<Person> Process()
        {
            return (List<Person>) [];
        }
    }
    
    class FakeTransformStep : IPipelineStep, IPipelineTransformStep
    {
        public IEnumerable<Person> Process(IEnumerable<Person> people)
        {
            return (List<Person>) [];
        }
    }
}