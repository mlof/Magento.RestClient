using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class StoreTests : AbstractIntegrationTest
    {
        [Test]
        public void CanGetWebsites()
        {
            var websites = this.Client.Stores.GetWebsites();
            Assert.IsNotEmpty(websites);
        }

        [Test]
        public void CanGetStoreViews()
        {
            var storeViews = this.Client.Stores.GetStoreViews();
            Assert.IsNotEmpty(storeViews);
        }

        [Test]
        public void CanGetStoreConfigs()
        {
            var storeConfigs = this.Client.Stores.GetStoreConfigs();
            Assert.IsNotEmpty(storeConfigs);
        }

        [Test]
        public void CanGetStoreGroups()
        {
            var storeGroups = this.Client.Stores.GetStoreGroups();
            Assert.IsNotEmpty(storeGroups);
        }
    }
}