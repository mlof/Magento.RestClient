using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Magento.RestClient;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Configuration;
using Magento.RestClient.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Magento.Manager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
	    public App()
	    {

		    this.Configuration = new ConfigurationBuilder()
			    .AddJsonFile("appsettings.json")
			    .Build();
		    Services = ConfigureServices();

			this.InitializeComponent();
	    }

	    public IConfigurationRoot Configuration { get; set; }

	    /// <summary>
	    /// Gets the current <see cref="App"/> instance in use
	    /// </summary>
	    public new static App Current => (App)Application.Current;

	    /// <summary>
	    /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
	    /// </summary>
	    public IServiceProvider Services { get; }

	    /// <summary>
	    /// Configures the services for the application.
	    /// </summary>
	    private  IServiceProvider ConfigureServices()
	    {
		    var services = new ServiceCollection();

		    var magentoContext =
			    new MagentoAdminContext(Configuration["Magento:Host"],
				    Configuration["Username"], Configuration["Password"]);
			    
		    services.AddSingleton<IAdminContext>(provider => magentoContext);



			return services.BuildServiceProvider();
	    }
		
	}
}
