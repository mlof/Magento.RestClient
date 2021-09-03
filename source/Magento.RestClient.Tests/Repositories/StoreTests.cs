using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
    public class StoreTests : AbstractIntegrationTest
    {
        [Test]
        public void CanGetWebsites()
        {
            var websites = this.Context.Stores.GetWebsites();
            Assert.IsNotEmpty(websites);
        }

        [Test]
        public void CanGetStoreViews()
        {
            var storeViews = this.Context.Stores.GetStoreViews();
            Assert.IsNotEmpty(storeViews);
        }

        [Test]
        public void CanGetStoreConfigs()
        {
            var storeConfigs = this.Context.Stores.GetStoreConfigs();
            Assert.IsNotEmpty(storeConfigs);
        }

        [Test]
        public void CanGetStoreGroups()
        {
            var storeGroups = this.Context.Stores.GetStoreGroups();
            Assert.IsNotEmpty(storeGroups);
        }
    }
}