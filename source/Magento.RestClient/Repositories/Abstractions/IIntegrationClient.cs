using System.Collections.Generic;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Search.Abstractions;

namespace Magento.RestClient.Repositories.Abstractions
{

    public interface IIntegrationClient : ICanGetModules
    {
        ISearchService Search { get; }
        IStoreRepository Stores { get; }
        IProductRepository Products { get; }
        IProductMediaRepository ProductMedia { get; }
        IConfigurableProductRepository ConfigurableProducts { get; }
        IOrderRepository Orders { get; }
        ICustomerRepository Customers { get; }
        ICustomerGroupRepository CustomerGroups { get; }
        IDirectoryRepository Directory { get; }
        IAttributeSetRepository AttributeSets { get; }
        IInvoiceRepository Invoices { get; }
        ICategoryRepository Categories { get; }
        ICartRepository Carts { get; }
        IAttributeRepository Attributes { get; set; }


    }

    /// <summary>
    /// Gets installed modules.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="MagentoException"></exception>

	public interface ICanGetModules
    {
	    List<string> GetModules();

    }
}