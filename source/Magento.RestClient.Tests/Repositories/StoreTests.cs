using System.Threading.Tasks;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class StoreTests : AbstractIntegrationTest
    {
        [Test]
        async public Task CanGetWebsites()
        {
            var websites = await this.Context.Stores.GetWebsites();
            Assert.IsNotEmpty(websites);
        }

        [Test]
        async public Task CanGetStoreViews()
        {
            var storeViews = await this.Context.Stores.GetStoreViews();
            Assert.IsNotEmpty(storeViews);
        }

        [Test]
        async public Task CanGetStoreConfigs()
        {
            var storeConfigs = await this.Context.Stores.GetStoreConfigs();
            Assert.IsNotEmpty(storeConfigs);
        }

        [Test]
        async public Task CanGetStoreGroups()
        {
            var storeGroups = await this.Context.Stores.GetStoreGroups();
            Assert.IsNotEmpty(storeGroups);
        }
    }
}