using System.Runtime.InteropServices;
using NUnit.Framework;

namespace MagentoApi.Tests.Integration
{
    public class StoreTests : AbstractIntegrationTest
    {
        [Test]
        public void CanGetWebsites()
        {
            var websites = this.client.Stores.GetWebsites();
            Assert.IsNotEmpty(websites);
        }

        [Test]
        public void CanGetStoreViews()
        {
            var storeViews = this.client.Stores.GetStoreViews();
            Assert.IsNotEmpty(storeViews);
        }

        [Test]
        public void CanGetStoreConfigs()
        {
            var storeConfigs = this.client.Stores.GetStoreConfigs();
            Assert.IsNotEmpty(storeConfigs);
        }

        [Test]
        public void CanGetStoreGroups()
        {
            var storeGroups = this.client.Stores.GetStoreGroups();
            Assert.IsNotEmpty(storeGroups);
        }
    }
}