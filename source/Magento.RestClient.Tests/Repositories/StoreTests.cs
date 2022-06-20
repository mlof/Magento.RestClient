using System.Threading.Tasks;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class StoreTests : AbstractAdminTest
    {
        [Test]
        public async  Task CanGetWebsites()
        {
            var websites = await this.MagentoContext.Stores.GetWebsites();
            Assert.IsNotEmpty(websites);
        }

        [Test]
        public async  Task CanGetStoreViews()
        {
            var storeViews = await this.MagentoContext.Stores.GetStoreViews();
            Assert.IsNotEmpty(storeViews);
        }

        [Test]
        public async  Task CanGetStoreConfigs()
        {
            var storeConfigs = await this.MagentoContext.Stores.GetStoreConfigs();
            Assert.IsNotEmpty(storeConfigs);
        }

        [Test]
        public async  Task CanGetStoreGroups()
        {
            var storeGroups = await this.MagentoContext.Stores.GetStoreGroups();
            Assert.IsNotEmpty(storeGroups);
        }
    }
}