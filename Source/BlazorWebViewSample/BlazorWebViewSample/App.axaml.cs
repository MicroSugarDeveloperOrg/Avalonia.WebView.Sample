using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using AvaloniaBlazorWebView;
using BlazorWebViewSample.ViewModels;
using BlazorWebViewSample.Views;
using Microsoft.Extensions.DependencyInjection;
using BlazorWebViewSampleShared;
using BlazorWebViewSampleShared.Data;
using BlazorWebViewSample.Extensions;

namespace BlazorWebViewSample;
public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void RegisterServices()
    {
        base.RegisterServices();

        AvaloniaBlazorWebViewBuilder.Initialize(default, setting =>
        {
            //this is setting for blazor 
            setting.ComponentType = typeof(AppWeb);
            setting.Selector = "#app";

            //because avalonia support the html css and js for resource ,so you must set the ResourceAssembly 
            setting.IsAvaloniaResource = true;
            setting.ResourceAssembly = typeof(AppWeb).Assembly;
        }, inject =>
        {
            //you can inject the resource in this
            inject.AddMasaBlazor(builder =>
            {
                builder.ConfigureTheme(theme =>
                {
                    theme.Themes.Light.Primary = "#4318FF";
                    theme.Themes.Light.Accent = "#4318FF";
                });
            });
            inject.AddService("wwwroot/nav/nav.json");
        });
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}