using Microsoft.Extensions.DependencyInjection;

namespace StressCheckAvalonia.Services;

public static class ServiceLocator
{
    public static IServiceProvider Provider { get; set; } = null!;

    public static T GetRequired<T>() where T : notnull => Provider.GetRequiredService<T>();
}
