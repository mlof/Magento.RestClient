using MagentoApi.Models;

namespace MagentoApi.Repositories.Abstractions
{
    public interface ICategoryReadRepository
    {
        public Category GetCategoryById();

        public Category GetCategoryTree( int rootCategoryId, int depth = 1);
        public SearchResponse<Category> Search();


    }
}