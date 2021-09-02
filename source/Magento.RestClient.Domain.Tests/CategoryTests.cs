using System.Linq;
using FluentAssertions;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Tests.Abstractions;
using Magento.RestClient.Models.Category;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	public class CategoryTests : AbstractDomainObjectTest
	{
		private long rootId;

		[SetUp]
		public void CategorySetup()
		{
			var root = this.Client.Categories.GetCategoryTree();
			this.rootId = root.Id;
		}

		[Test]
		public void AddChild_ValidCategory()
		{
			var model = new CategoryModel(Client, rootId);

			model.AddChild("TEST CATEGORY");

			model.Save();

			model.Children
				.SingleOrDefault(category => category.Name == "TEST CATEGORY")
				.Should()
				.NotBeNull();
		}


		[Test]
		public void AddProduct()
		{

			var root = new CategoryModel(Client, rootId);

			root.AddChild("TEST CATEGORY");

			root.Save();
			var cat = root.Children.SingleOrDefault(category => category.Name == "TEST CATEGORY").ToModel(Client);
			cat.AddProduct(this.SimpleProductSku);


			cat.Save();
			
			
		}
		[Test]
		public void AddChild_Nested()
		{
			var model = new CategoryModel(Client, rootId);

			model.AddChild("TEST CATEGORY");

			

			model.Save();

		}

		[TearDown]
		public void CategoryTeardown()
		{
			var model = new CategoryModel(Client, rootId);


			model.Children
				.Single(category => category.Name == "TEST CATEGORY")
				.ToModel(Client)
				.Delete();
		}
	}
}