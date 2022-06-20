using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Extensions;
using Magento.RestClient.Modules.Catalog.Models.Products;
using Magento.RestClient.Modules.EAV.Abstractions;
using Magento.RestClient.Modules.EAV.Model;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;
using Serilog;

namespace Magento.RestClient.Modules.EAV
{
	internal class AttributeRepository : AbstractRepository, IAttributeRepository
	{
		public AttributeRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
			this.RelativeExpiration = TimeSpan.FromMinutes(1);
		}

		public TimeSpan RelativeExpiration { get; set; }

		public async  Task<IEnumerable<EntityAttribute>> GetProductAttributes(long attributeSetId)
		{
			var request = new RestRequest("products/attribute-sets/{id}/attributes");
			request.AddOrUpdateParameter("id", attributeSetId, ParameterType.UrlSegment);
			request.SetScope("all");

			return await ExecuteAsync<List<EntityAttribute>>(request).ConfigureAwait(false);
		}

		public Task<ProductAttribute> Create(ProductAttribute attribute)
		{
			var request = new RestRequest("products/attributes", Method.Post);
			request.SetScope("all");

			request.AddJsonBody(new {attribute});

			return ExecuteAsync<ProductAttribute>(request);
		}

		public Task DeleteProductAttribute(string attributeCode)
		{
			var request = new RestRequest("products/attributes/{attributeCode}", Method.Delete);

			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");
			var key = this.Client.BuildUri(request);

			this.Cache.Remove(key);

			return this.Client.ExecuteAsync(request);
		}

		public Task<List<Option>> GetProductAttributeOptions(string attributeCode)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options");
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");
			var key = this.Client.BuildUri(request);

			return this.Cache.GetOrCreateAsync(key, entry => {
				entry.AbsoluteExpirationRelativeToNow = this.RelativeExpiration;


				return ExecuteAsync<List<Option>>(request);
			});
		}

		public Task<int> CreateProductAttributeOption(string attributeCode, Option option)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options", Method.Post);

			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			request.AddJsonBody(new {option});

			var key = this.Client.BuildUri(request);

			this.Cache.Remove(key);
			return ExecuteAsync<int>(request);
		}

		public async  Task<ProductAttribute> GetByCode(string attributeCode)
		{
			Log.Information("Getting product attribute {AttributeCode}", attributeCode);

			var request = new RestRequest("products/attributes/{attributeCode}");
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");
			var key = this.Client.BuildUri(request);

			var cacheItem = this.Cache.Get<ProductAttribute>(key);

			if (cacheItem != null)
			{
				return cacheItem;
			}

			var result = await ExecuteAsync<ProductAttribute>(request).ConfigureAwait(false);

			if (result != null)
			{
				this.Cache.Set(key, result, this.RelativeExpiration);
			}

			return result;
		}

		public Task<ProductAttribute> Update(string attributeCode, ProductAttribute attribute)
		{
			var request = new RestRequest("products/attributes/{attributeCode}", Method.Put);

			var key = this.Client.BuildUri(request);
			this.Cache.Remove(key);
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			attribute.AttributeCode = null;
			request.AddJsonBody(new {attribute});

			return ExecuteAsync<ProductAttribute>(request);
		}

		public Task DeleteProductAttributeOption(string attributeCode, string optionValue)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options/{optionValue}", Method.Delete);
			request.SetScope("all");
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("optionValue", optionValue, ParameterType.UrlSegment);
			var key = this.Client.BuildUri(request);

			this.Cache.Remove(key);
			return this.Client.ExecuteAsync(request);
		}

		public Task<ProductAttribute> GetById(long id)
		{
			var request = new RestRequest("products/attributes/{id}");
			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);
			request.SetScope("all");

			return ExecuteAsync<ProductAttribute>(request);
		}
	}
}