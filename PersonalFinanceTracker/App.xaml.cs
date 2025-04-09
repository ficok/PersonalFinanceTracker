using System.Configuration;
using System.Data;
using System.Windows;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using PersonalFinanceTracker.data;
using PersonalFinanceTracker.data.repositories;
using PersonalFinanceTracker.ui;
using PersonalFinanceTracker.data.context;
using System.CodeDom;
using PersonalFinanceTracker.data.interfaces;

namespace PersonalFinanceTracker
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // Register DbContext with PostgreSQL using the connection string from appsettings.json
                    services.AddDbContext<Database>(options =>
                        options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection")));

                    // Register repositories with the same scope as the database context
                    services.AddScoped(typeof(IQuerier<>), typeof(Querier<>));

                    // Register the DatabaseTester service for connection testing
                    services.AddTransient<DatabaseTester>();

                    // Register your main window (if needed)
                    services.AddTransient<MainWindow>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();

            var tester = AppHost.Services.GetRequiredService<DatabaseTester>();
            bool isConnected = await tester.TestConnectionAsync();

            if (!isConnected)
            {
                MessageBox.Show("Failed to connect to the database. Exiting application",
                    "Database Connection Status", MessageBoxButton.OK, MessageBoxImage.Error);

                Shutdown();
                return;
            }
            else
            {
                MessageBox.Show("Sucessfully connected to the database.",
                    "Database Connection Status", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            var mainWindow = AppHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost.StopAsync();
            base.OnExit(e);
        }
    }

}
