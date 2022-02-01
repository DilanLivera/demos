using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WeatherApi;

public class MemoryHealthCheck : IHealthCheck
{
    //private const long Threshold = 1024L * 1024L * 1024L;
    private const long Threshold = 1000000;


    public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default)
    {
        var allocatedBytesInManagedMemory = GC.GetTotalMemory(forceFullCollection: false);
        var data = new Dictionary<string, object>()
        {
            { "AllocatedBytesInManagedMemory", allocatedBytesInManagedMemory },
            { "NumberOfTimesGarbageCollectionOccurredForGen0Collection", GC.CollectionCount(0) },
            { "NumberOfTimesGarbageCollectionOccurredForGen1Collection", GC.CollectionCount(1) },
            { "NumberOfTimesGarbageCollectionOccurredForGen2Collection", GC.CollectionCount(2) }
        };

        if (allocatedBytesInManagedMemory > Threshold)
        {
            var result = new HealthCheckResult(
                status: context.Registration.FailureStatus,
                description: $"Allocated bytes in managed memory is > {Threshold}",
                data: data);

            return Task.FromResult(result);
        }

        var healthyResult = new HealthCheckResult(
            status: HealthStatus.Healthy,
            description: $"Allocated bytes in managed memory is <= {Threshold}",
            data: data);

        return Task.FromResult(healthyResult);
    }
}
