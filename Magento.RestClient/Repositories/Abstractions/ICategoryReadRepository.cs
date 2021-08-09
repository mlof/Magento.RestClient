using Magento.RestClient.Models;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface ICategoryReadRepository
    {
        public Category GetCategoryById(int categoryId);

        public Category GetCategoryTree( int rootCategoryId, int depth = 1);
        public SearchResponse<Category> Search();


    }
}