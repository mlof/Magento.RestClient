using System.Linq;
using FluentAssertions;
using Magento.RestClient.Modules.Catalog.Models.Category;
using NUnit.Framework;

namespace Magento.RestClient.Tests.Repositories
{
	public class CategoryTests : AbstractAdminTest
	{
		[SetUp]
		public void SetupCategories()
		{
		}

		[TearDown]
		public void TeardownCategories()
		{
			var category = MagentoContext.Categories.AsQueryable().SingleOrDefault(category1 => category1.Name == "Should Be Created");


			if (category != null)
			{
				MagentoContext.Categories.DeleteCategoryById(category.Id);
			}
		}

		[Test]
		public void GetCategoryById_CategoryExists()
		{
			var c = this.MagentoContext.Categories.GetCategoryById(1);


			c.Should().NotBeNull();
		}


		[Test]
		public void Create_ValidCategory_WithoutSubCategories()
		{
			var shouldBeCreated = new Category() {Name = "Should Be Created", IsActive = true};

			var created = this.MagentoContext.Categories.CreateCategory(shouldBeCreated);


			var category = MagentoContext.Categories.AsQueryable().SingleOrDefault(category1 => category1.Name == "Should Be Created");
			category.Name.Should().BeEquivalentTo(shouldBeCreated.Name);
			category.IsActive.Should().BeTrue();
		}

		[Test]
		public void Create_ValidCategory_WithSubCategories()
		{
			var shouldBeCreated = new Category() {Name = "Should Be Created", IsActive = true};

			this.MagentoContext.Categories.CreateCategory(shouldBeCreated);


			var category = MagentoContext.Categories.AsQueryable().SingleOrDefault(category1 => category1.Name == "Should Be Created");
			category.Name.Should().BeEquivalentTo(shouldBeCreated.Name);
			category.IsActive.Should().BeTrue();
		}

		[Test]
		public void GetCategoryById_CategoryDoesNotExist()
		{
			var c = this.MagentoContext.Categories.GetCategoryById(-1);


			c.Should().BeNull();
		}
	}
}