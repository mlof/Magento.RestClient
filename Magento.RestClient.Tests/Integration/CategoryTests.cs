using FluentAssertions;
using Magento.RestClient.Models;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Integration
{
    public class CategoryTests : AbstractIntegrationTest
    {
        [SetUp]
        public void SetupCustomers()
        {

        }

        [Test]
        public void GetCategoryById_CategoryExists()
        {
            var c = this.Client.Categories.GetCategoryById(1);


            c.Should().NotBeNull();
        }
        [Test]
        public void GetCategoryById_CategoryDoesNotExist()
        {
            var c = this.Client.Categories.GetCategoryById(-1);


            c.Should().BeNull();
        }




    }
}