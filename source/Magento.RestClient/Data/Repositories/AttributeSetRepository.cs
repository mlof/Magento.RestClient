using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magento.RestClient.Data.Models.Attributes;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Extensions;
using Microsoft.Extensions.Caching.Memory;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class AttributeSetRepository : AbstractRepository, IAttributeSetRepository
	{
		private readonly IRestClient _client;
		private readonly MemoryCache cache;

		public AttributeSetRepository(IRestClient client, MemoryCache memoryCache)
		{
			_client = client;
			this.cache = memoryCache;
		}


		async public Task<AttributeSet> Create(EntityType entityTypeCode, long skeletonId, AttributeSet attributeSet)
		{
			var request = new RestRequest("eav/attribute-sets");

			request.Method = Method.POST;
			request.AddJsonBody(new {attributeSet, skeletonId, entityTypeCode = entityTypeCode.ToTypeCode()});


			var response = await _client.ExecuteAsync<AttributeSet>(request);
			return HandleResponse(response);
		}

		async public Task Delete(long attributeSetId)
		{
			var request = new RestRequest("eav/attribute-sets/{id}");
			request.Method = Method.DELETE;
			request.AddOrUpdateParameter("id", attributeSetId, ParameterType.UrlSegment);
			await _client.ExecuteAsync(request);
		}


		async public Task<long> CreateProductAttributeGroup(long attributeSetId, string attributeGroupName)
		{
			var request = new RestRequest("products/attribute-sets/{attributeSetId}/groups");
			request.Method = Method.PUT;
			request.SetScope("all");
			request.AddOrUpdateParameter("attributeSetId", attributeSetId, ParameterType.UrlSegment);
			request.AddJsonBody(new {
				group = new {attribute_group_name = attributeGroupName, attribute_set_id = attributeSetId}
			});
			var response = await _client.ExecuteAsync<AttributeGroup>(request);

			if (response.IsSuccessful)
			{
				return response.Data.AttributeGroupId;
			}

			throw MagentoException.Parse(response.Content);
		}

		async public Task<AttributeSet> Get(long id)
		{
			var request = new RestRequest("eav/attribute-sets/{id}");
			request.Method = Method.GET;
			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);
			var response = await _client.ExecuteAsync<AttributeSet>(request);
			return HandleResponse(response);
		}

		async public Task AssignProductAttribute(long attributeSetId, long attributeGroupId, string attributeCode,
			int sortOrder = 1)
		{
			var request = new RestRequest("products/attribute-sets/attributes");
			request.Method = Method.POST;

			request.AddJsonBody(new {attributeSetId, attributeCode, attributeGroupId, sortOrder});
			await _client.ExecuteAsync(request);
		}

		async public Task RemoveProductAttribute(long attributeSetId, string attributeCode)
		{
			var request = new RestRequest("products/attribute-sets/{attributeSetId}/attributes/{attributeCode}");
			request.Method = Method.DELETE;
			request.AddOrUpdateParameter("attributeSetId", attributeSetId, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);

			await _client.ExecuteAsync(request);
		}


		public IQueryable<AttributeSet> AsQueryable()
		{
			return new MagentoQueryable<AttributeSet>(_client, "eav/attribute-sets/list", cache,
				TimeSpan.FromMinutes(1));
		}
	}
}