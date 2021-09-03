using System.Linq;
using FluentAssertions;
using Magento.RestClient.Models.Category;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class CategoryTests : AbstractIntegrationTest
	{
		[SetUp]
		public void SetupCategories()
		{
		}

		[TearDown]
		public void TeardownCategories()
		{
			var category = Context.Search
				.Categories(builder => builder.WhereEquals(category => category.Name, "Should Be Created"))
				.Items
				.SingleOrDefault();


			if (category != null)
			{
				Context.Categories.DeleteCategoryById(category.Id);
			}
		}

		[Test]
		public void GetCategoryById_CategoryExists()
		{
			var c = this.Context.Categories.GetCategoryById(1);


			c.Should().NotBeNull();
		}


		[Test]
		public void Create_ValidCategory_WithoutSubCategories()
		{
			var shouldBeCreated = new Category() {Name = "Should Be Created", IsActive = true};

			var created = this.Context.Categories.CreateCategory(shouldBeCreated);


			var category = Context.Search
				.Categories(builder => builder.WhereEquals(category => category.Name, "Should Be Created"))
				.Items.Single();
			category.Name.Should().BeEquivalentTo(shouldBeCreated.Name);
			category.IsActive.Should().BeTrue();
		}

		[Test]
		public void Create_ValidCategory_WithSubCategories()
		{
			var shouldBeCreated = new Category() {Name = "Should Be Created", IsActive = true};

			this.Context.Categories.CreateCategory(shouldBeCreated);


			var category = Context.Search
				.Categories(builder => builder.WhereEquals(category => category.Name, "Should Be Created"))
				.Items.Single();
			category.Name.Should().BeEquivalentTo(shouldBeCreated.Name);
			category.IsActive.Should().BeTrue();
		}

		[Test]
		public void GetCategoryById_CategoryDoesNotExist()
		{
			var c = this.Context.Categories.GetCategoryById(-1);


			c.Should().BeNull();
		}
	}
}