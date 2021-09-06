using System.Linq;
using FluentAssertions;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Domain.Models;
using Magento.RestClient.Domain.Tests.Abstractions;
using NUnit.Framework;

namespace Magento.RestClient.Domain.Tests
{
	public class CategoryTests : AbstractDomainObjectTest
	{
		private long rootId;

		[SetUp]
		public void CategorySetup()
		{
			var root = this.Context.Categories.GetCategoryTree();
			this.rootId = root.Id;
		}

		[Test]
		public void AddChild_ValidCategory()
		{
			var model = new CategoryModel(Context, rootId);

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

			var root = new CategoryModel(Context, rootId);

			root.AddChild("TEST CATEGORY");

			root.Save();
			var cat = root.Children.SingleOrDefault(category => category.Name == "TEST CATEGORY").ToModel(Context);
			cat.AddProduct(this.SimpleProductSku);


			cat.Save();
			
			
		}
		[Test]
		public void AddChild_Nested()
		{
			var model = new CategoryModel(Context, rootId);

			model.AddChild("TEST CATEGORY");

			

			model.Save();

		}

		[TearDown]
		public void CategoryTeardown()
		{
			var model = new CategoryModel(Context, rootId);


			model.Children
				.Single(category => category.Name == "TEST CATEGORY")
				.ToModel(Context)
				.Delete();
		}
	}
}