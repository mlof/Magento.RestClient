using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Magento.RestClient.Domain.Models.Catalog;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Domain
{
	public class CategoryTests : AbstractAdminTest
	{
		private long rootId;

		[SetUp]
		public void CategorySetup()
		{
			var root = this.Context.Categories.GetCategoryTree();
			this.rootId = root.Id;
		}

		[Test]
		async public Task AddChild_ValidCategory()
		{
			var model = new CategoryModel(Context, rootId);

			
			model.GetOrCreateChild("TEST CATEGORY");

			await model.SaveAsync();

			model.Children
				.SingleOrDefault(category => category.Name == "TEST CATEGORY")
				.Should()
				.NotBeNull();
		}


		[Test]
		async public Task AddProduct()
		{

			var root = new CategoryModel(Context, rootId);

			root.GetOrCreateChild("TEST CATEGORY");

			await root.SaveAsync();
			var cat = root.Children.SingleOrDefault(category => category.Name == "TEST CATEGORY");


			await cat.SaveAsync();
			
			
		}
		[Test]
		async public Task AddChild_Nested()
		{
			var model = new CategoryModel(Context, rootId);

			model.GetOrCreateChild("TEST CATEGORY");

			

			await model.SaveAsync();

		}

		[TearDown]
		async public Task CategoryTeardown()
		{
			var model = new CategoryModel(Context, rootId);


			await model.Children
				.Single(category => category.Name == "TEST CATEGORY")
				.Delete();
		}
	}
}