using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Configuration;
using Magento.RestClient.Context;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;

namespace Magento.RestClient.Tests
{
	public abstract class AbstractAdminTest
	{
		protected IMagentoContext MagentoContext;

		[OneTimeSetUp]
		public async  Task Setup()
		{
			Log.Logger = new LoggerConfiguration().WriteTo.Debug().CreateLogger();

			var configurationRoot = new ConfigurationBuilder().AddUserSecrets<MagentoClientOptions>().Build();

			var configuration = new MagentoClientOptions();
			configurationRoot.Bind(configuration);

			this.MagentoContext = new MagentoContext(configuration);

		}



		[TearDown]
		public void Teardown()
		{
		}
	}
}