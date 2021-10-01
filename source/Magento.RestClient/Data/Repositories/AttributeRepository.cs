using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Extensions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class AttributeRepository : AbstractRepository, IAttributeRepository
	{

		public AttributeRepository(IContext context) : base(context)
		{
			RelativeExpiration = TimeSpan.FromSeconds(5);
		}


		public async Task<IEnumerable<EntityAttribute>> GetProductAttributes(long attributeSetId)
		{
			var request = new RestRequest("products/attribute-sets/{id}/attributes");
			request.Method = Method.GET;
			request.AddOrUpdateParameter("id", attributeSetId, ParameterType.UrlSegment);
			request.SetScope("all");


			var response = await Client.ExecuteAsync<List<EntityAttribute>>(request);
			return HandleResponse(response);
		}


		public async Task<ProductAttribute> Create(ProductAttribute attribute)
		{
			var request = new RestRequest("products/attributes");
			request.SetScope("all");

			request.Method = Method.POST;
			request.AddJsonBody(new {attribute});


			var response = await Client.ExecuteAsync<ProductAttribute>(request);
			return HandleResponse(response);
		}

		public async Task DeleteProductAttribute(string attributeCode)
		{
			var request = new RestRequest("products/attributes/{attributeCode}");

			request.Method = Method.DELETE;
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			await Client.ExecuteAsync(request);
		}

		public async Task<List<Option>> GetProductAttributeOptions(string attributeCode)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options");

			request.Method = Method.GET;
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			var response = await Client.ExecuteAsync<List<Option>>(request);
			return HandleResponse(response);
		}

		public async Task<int> CreateProductAttributeOption(string attributeCode, Option option)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options");

			request.Method = Method.POST;
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			request.AddJsonBody(new {option});
			var response = await Client.ExecuteAsync<int>(request);

			if (response.IsSuccessful)
			{
				return response.Data;
			}

			throw MagentoException.Parse(response.Content);
		}

		public async Task<ProductAttribute> GetByCode(string attributeCode)
		{
			return await Cache.GetOrCreateAsync<ProductAttribute>($"{this.GetType().Name}_{attributeCode}",
				async entry => {
					entry.AbsoluteExpirationRelativeToNow = this.RelativeExpiration;

					var request = new RestRequest("products/attributes/{attributeCode}");
					request.Method = Method.GET;
					request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
					request.SetScope("all");

					var response = await Client.ExecuteAsync<ProductAttribute>(request);
					return HandleResponse(response);
				});
		}

		public TimeSpan RelativeExpiration { get; set; }

		public async Task<ProductAttribute> Update(string attributeCode, ProductAttribute attribute)
		{
			/*var request = new RestRequest("products/attributes/{attributeCode}");
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.SetScope("all");

			attribute.AttributeCode = null;
			request.Method = Method.PUT;
			request.AddJsonBody(new {attribute});


			var response = await Client.ExecuteAsync<ProductAttribute>(request);
			return HandleResponse(response);*/
			return attribute;
		}

		public async Task DeleteProductAttributeOption(string attributeCode, string optionValue)
		{
			var request = new RestRequest("products/attributes/{attributeCode}/options/{optionValue}");
			request.SetScope("all");
			request.Method = Method.DELETE;
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("optionValue", optionValue, ParameterType.UrlSegment);
			await Client.ExecuteAsync(request);
		}

		public async Task<ProductAttribute> GetById(long id)
		{
			var request = new RestRequest("products/attributes/{id}");
			request.Method = Method.GET;
			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);
			request.SetScope("all");

			var response = await Client.ExecuteAsync<ProductAttribute>(request);
			return HandleResponse(response);
		}
	}
}