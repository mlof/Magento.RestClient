namespace Magento.RestClient.Repositories.Abstractions
{
    public interface IIntegrationClient 
    {
        IStoreRepository Stores { get;  }
        IProductRepository Products { get; }
        IConfigurableProductRepository ConfigurableProducts{ get; }
        IOrderRepository Orders{ get; }
        ICustomerRepository Customers { get;  }
        ICustomerGroupRepository CustomerGroups { get; }
        IDirectoryRepository Directory{ get; }
        IAttributeSetRepository AttributeSets { get;  }
        IInvoiceRepository Invoices { get;  }
        ICategoryRepository Categories { get;  }
        ICartRepository Carts { get; }
    }
}