using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;

namespace NameSorter.Benchmarks;

[MemoryDiagnoser]
[CategoriesColumn]
[AllStatisticsColumn]
[RankColumn]
public abstract class BenchmarkBase
{
    public static readonly Config DefaultConfig = new();

    public class Config : ManualConfig
    {
        public Config()
        {
            AddJob(Job.Default
                .WithIterationCount(100)
                .WithWarmupCount(5));
            
            WithOptions(ConfigOptions.DisableOptimizationsValidator);
        }
    }
}