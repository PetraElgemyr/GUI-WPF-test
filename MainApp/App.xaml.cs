using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shared.Services;
using System.IO;
using System.Windows;

namespace MainApp;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IHost _host;

    public App()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory; //där applikationen körs   
        string filePath = Path.Combine(baseDirectory, "customers.json");
        /*
         * Här säger vi att när projektet startas upp så ska vi säga vad som ska användas. Om vi har middleware eller något. 
         * Vi konfigurerar vad som ska vara med. Detta är builderen som är "builder.build" eller något i en ASP.NET Core applikation.
         */
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => {
                services.AddSingleton<CustomerService>();
                services.AddSingleton(new FileService(filePath));
                services.AddSingleton<MainWindow>();
            }).Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        await _host.StartAsync();

        var mainWindow = _host.Services.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }
}
