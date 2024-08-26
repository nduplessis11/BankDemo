namespace CurrentAccount.Api;

public static class HostEnvironmentExtensions
{
    public static bool IsLocal(this IHostEnvironment hostingEnvironment)
    {
        return hostingEnvironment.IsEnvironment("Local");
    }

    public static bool IsDevelopmentOrLocal(this IHostEnvironment hostingEnvironment)
    {
        return hostingEnvironment.IsDevelopment() || hostingEnvironment.IsEnvironment("Local");
    }
}