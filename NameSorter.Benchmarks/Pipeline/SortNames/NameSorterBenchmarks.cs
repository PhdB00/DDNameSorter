using BenchmarkDotNet.Attributes;
using DD.NameSorter;
using DD.NameSorter.Pipeline.SortNames;

namespace NameSorter.Benchmarks.Pipeline.SortNames;

[MemoryDiagnoser]
[CategoriesColumn]
[AllStatisticsColumn]
public class NameSorterBenchmarks
{
    private INameSorter sorter;
    private List<Person> orderedNames;
    private List<Person> reverseOrderedNames;
    private List<Person> randomNames;
    private List<Person> sameLastNames;
    private List<Person> mixedNames;

    [Params(100, 1000, 10000)]
    public int NameCount { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        sorter = new DD.NameSorter.Pipeline.SortNames.NameSorter();
        
        // Generate test data
        orderedNames = GenerateOrderedNames();
        reverseOrderedNames = GenerateReverseOrderedNames();
        randomNames = GenerateRandomNames();
        sameLastNames = GenerateSameLastNames();
        mixedNames = GenerateMixedNames();
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Ordered")]
    public void SortOrderedNames()
    {
        _ = sorter.Sort(orderedNames).ToList();
    }

    [Benchmark]
    [BenchmarkCategory("Reverse")]
    public void SortReverseOrderedNames()
    {
        _ = sorter.Sort(reverseOrderedNames).ToList();
    }

    [Benchmark]
    [BenchmarkCategory("Random")]
    public void SortRandomNames()
    {
        _ = sorter.Sort(randomNames).ToList();
    }

    [Benchmark]
    [BenchmarkCategory("SameLastName")]
    public void SortSameLastNames()
    {
        _ = sorter.Sort(sameLastNames).ToList();
    }

    [Benchmark]
    [BenchmarkCategory("Mixed")]
    public void SortMixedNames()
    {
        _ = sorter.Sort(mixedNames).ToList();
    }

    private List<Person> GenerateOrderedNames()
    {
        return Enumerable.Range(0, NameCount)
            .Select(i => new Person(
                new[] { $"FirstName{i:D5}" },
                $"LastName{i:D5}"))
            .ToList();
    }

    private List<Person> GenerateReverseOrderedNames()
    {
        return GenerateOrderedNames()
            .OrderByDescending(p => p.LastName)
            .ThenByDescending(p => string.Join(" ", p.GivenNames))
            .ToList();
    }

    private List<Person> GenerateRandomNames()
    {
        var random = new Random(42);
        return Enumerable.Range(0, NameCount)
            .Select(_ => new Person(
                new[] 
                { 
                    $"FirstName{random.Next(NameCount):D5}",
                    $"MiddleName{random.Next(NameCount):D5}"
                },
                $"LastName{random.Next(NameCount):D5}"))
            .ToList();
    }

    private List<Person> GenerateSameLastNames()
    {
        var random = new Random(42);
        return Enumerable.Range(0, NameCount)
            .Select(_ => new Person(
                new[] 
                { 
                    $"FirstName{random.Next(NameCount):D5}",
                    $"MiddleName{random.Next(NameCount):D5}"
                },
                "Smith"))
            .ToList();
    }

    private List<Person> GenerateMixedNames()
    {
        var random = new Random(42);
        var names = new List<Person>(NameCount);
        
        // Add some ordered names
        names.AddRange(GenerateOrderedNames().Take(NameCount / 3));
        
        // Add some random names
        names.AddRange(GenerateRandomNames().Take(NameCount / 3));
        
        // Add some same last names
        names.AddRange(GenerateSameLastNames().Take(NameCount / 3));
        
        // Shuffle the list
        return names.OrderBy(_ => random.Next()).ToList();
    }
}