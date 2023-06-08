using Avalonia.Platform;
using BlazorWebViewSampleShared;
using BlazorWebViewSampleShared.Global;
using BlazorWebViewSampleShared.Global.Config;
using BlazorWebViewSampleShared.Global.Nav.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;

namespace BlazorWebViewSample.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddService(this IServiceCollection services, string assetPath)
    {

        services.AddSingleton<List<NavModel>>(provider => 
        {
            var uriString = $"avares://{typeof(AppWeb).Assembly.GetName().Name!}/{assetPath}";
            var stream = AssetLoader.Open(new Uri(uriString));
            var navList = JsonSerializer.Deserialize<List<NavModel>>(stream);
            return navList!;
        });
        services.AddScoped<NavHelper>();
        services.AddScoped<GlobalConfig>();
        return services;
    }
}
