using AppQuickLaunch.Troubleshooting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Windows;

namespace AppQuickLaunch
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            ServiceProvider.GetRequiredService<MainWindow>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddTransient(typeof(MainWindow));
            services.AddTransient<IConfigComponent, ConfigComponent>();
            services.AddTransient<IProcessComponent, ProcessComponent>();
            services.AddTransient<IStatusComponent, StatusComponent>();
            services.AddTransient<IUiSetupComponent, UiSetupComponent>();
            services.AddTransient<IDebuggingComponent, DebuggingComponent>();
            services.AddTransient<IUiEventHandlers, UiEventHandlers>();
            services.AddSingleton<IConfigCache, ConfigCache>();
        }
    }
}