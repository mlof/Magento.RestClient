using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Configuration;
using Magento.RestClient.Data.Models.Bulk;
using Magento.RestClient.Data.Models.Catalog.Products;
using Magento.RestClient.Data.Repositories;
using Magento.RestClient.Data.Requests;
using Magento.RestClient.Exceptions.Authentication;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Magento.RestClient.Context
{
    public class MagentoAdminContext : BaseContext, IAdminContext
    {
        private readonly RestSharp.RestClient _restClient;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="cache"></param>
        /// <param name="memoryCacheOptions"></param>
        /// <exception cref="InvalidAuthenticationMethodException"></exception>
        public MagentoAdminContext(MagentoClientOptions options, IMemoryCache cache = null,
            MemoryCacheOptions memoryCacheOptions = null) : base(cache, memoryCacheOptions)
        {
            if (options.AuthenticationMethod == AuthenticationMethod.Integration)
            {
                this._restClient = MagentoRestClientFactory.CreateIntegrationClient(options.Host, options.ConsumerKey,
                    options.ConsumerSecret, options.AccessToken, options.AccessTokenSecret, options.DefaultScope);
            }
            else if (options.AuthenticationMethod == AuthenticationMethod.Admin)

            {
                this._restClient = MagentoRestClientFactory.CreateAdminClient(options.Host, options.Username,
                    options.Password, options.DefaultScope);


            }
            else
            {
                throw new InvalidAuthenticationMethodException();
            }


        }

        public MagentoAdminContext(string host,
            string consumerKey,
            string consumerSecret,
            string accessToken,
            string accessTokenSecret,
            IMemoryCache cache = null, MemoryCacheOptions memoryCacheOptions = null,
            string defaultScope = "default") : this(
            new MagentoClientOptions(host, consumerKey, accessTokenSecret, accessToken, consumerSecret, defaultScope),
            cache,
            memoryCacheOptions)
        {
        }

        public MagentoAdminContext(string host,
            string username,
            string password,
            IMemoryCache cache = null, MemoryCacheOptions memoryCacheOptions = null,
            string defaultScope = "default") : this(
            new MagentoClientOptions(host, username, password, AuthenticationMethod.Admin, defaultScope), cache,
            memoryCacheOptions)
        {
        }

        public IAsyncRepository Async => new AsyncRepository(this);
        public ISpecialPriceRepository SpecialPrices => new SpecialPriceRepository(this);
        public IModuleRepository Modules => new ModuleRepository(this);
        public IProductAttributeGroupRepository ProductAttributeGroups => new ProductAttributeGroupRepository(this);
        public IStoreRepository Stores => new StoreRepository(this);
        public IProductRepository Products => new ProductRepository(this);

        /// <inheritdoc />
        public IProductAsyncRepository ProductsAsync => new ProductAsyncRepository(this);
        public IProductMediaRepository ProductMedia => new ProductMediaRepository(this);
        public IConfigurableProductRepository ConfigurableProducts => new ConfigurableProductRepository(this);
        public IOrderRepository Orders => new OrderRepository(this);
        public ICustomerRepository Customers => new CustomerRepository(this);
        public ICustomerGroupRepository CustomerGroups => new CustomerGroupRepository(this);
        public IDirectoryRepository Directory => new DirectoryRepository(this);
        public IAttributeSetRepository AttributeSets => new AttributeSetRepository(this);
        public IInvoiceRepository Invoices => new InvoiceRepository(this);
        public ICategoryRepository Categories => new CategoryRepository(this);
        public IInventoryStockRepository InventoryStocks => new InventoryStockRepository(this);
        public IInventorySourceItemRepository InventorySourceItems => new InventorySourceItemRepository(this);
        public IInventorySourceRepository InventorySources => new InventorySourceRepository(this);
        public ICartRepository Carts => new CartRepository(this);
        public IAttributeRepository Attributes => new AttributeRepository(this);
        public IShipmentRepository Shipments => new ShipmentRepository(this);


        public IProductAttributeRepository ProductAttributes => new ProductAttributeRepository(this);
        public IProductAttributeAsyncRepository ProductAttributeAsync => new ProductAttributeAsyncRepository(this);

        public RestSharp.RestClient RestClient => this._restClient;
    };



    public interface IProductAttributeAsyncRepository
    {
        Task<BulkActionResponse> PutAttributesByAttributeCode(params ProductAttribute[] attributes);
    }

    public class ProductAttributeAsyncRepository : AbstractRepository, IProductAttributeAsyncRepository
    {
        public ProductAttributeAsyncRepository(MagentoAdminContext context) : base(context)
        {
        }
        public Task<BulkActionResponse> PutAttributesByAttributeCode(params ProductAttribute[] attributes)
        {
            var maxOptionsPerRequest = 15;

            var request = new RestRequest("products/attributes/byAttributeCode", Method.Put);
            request.SetScope("all/async/bulk");

            var requests = new List<CreateOrUpdateAttributeRequest>();

            foreach (var attribute in attributes)
            {
                if (attribute.Options == null || attribute.Options.Count <= maxOptionsPerRequest)
                {
                    requests.Add(
                        new CreateOrUpdateAttributeRequest
                        {
                            AttributeCode = attribute.AttributeCode,
                            Attribute = attribute
                        });
                }
                else
                {
                    foreach (var options in attribute.Options.Chunk(maxOptionsPerRequest))
                    {
                        requests.Add(
                            new CreateOrUpdateAttributeRequest
                            {
                                AttributeCode = attribute.AttributeCode,
                                Attribute = attribute with { Options = options.ToList() }
                            });
                    }
                }
            }

            request.AddJsonBody(requests);

            return ExecuteAsync<BulkActionResponse>(request);
        }

    }

    public class ProductAttributeRepository : AbstractRepository, IProductAttributeRepository
    {
        public ProductAttributeRepository(MagentoAdminContext magentoAdminContext) : base(magentoAdminContext)
        {
        }

        public IQueryable<ProductAttribute> AsQueryable()
        {

            return new MagentoQueryable<ProductAttribute>(this.Client, "products/attributes");
        }
    }

    public interface IProductAttributeRepository : IHasQueryable<ProductAttribute>
    {
    }
}