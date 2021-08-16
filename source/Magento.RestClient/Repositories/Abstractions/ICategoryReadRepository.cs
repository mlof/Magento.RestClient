using Magento.RestClient.Models;
using Magento.RestClient.Models.Category;
using Magento.RestClient.Models.Search;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface ICategoryReadRepository
    {
        public Category GetCategoryById(int categoryId);

        public Category GetCategoryTree( int rootCategoryId, int depth = 1);


    }
}