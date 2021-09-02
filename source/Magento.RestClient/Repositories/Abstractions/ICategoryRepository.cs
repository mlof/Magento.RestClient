using Magento.RestClient.Models.Category;

namespace Magento.RestClient.Repositories.Abstractions
{
    public interface ICategoryRepository : ICategoryReadRepository, ICategoryWriteRepository
    {
    }
}