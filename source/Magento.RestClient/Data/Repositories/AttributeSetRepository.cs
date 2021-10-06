using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Data.Models.EAV.Attributes;
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
		public AttributeSetRepository(IContext context) : base(context)
		{
		}

		public async Task<AttributeSet> Create(EntityType entityTypeCode, long skeletonId, AttributeSet attributeSet)
		{
			var request = new RestRequest("eav/attribute-sets", Method.POST);

			request.AddJsonBody(new {attributeSet, skeletonId, entityTypeCode = entityTypeCode.ToTypeCode()});

			return await ExecuteAsync<AttributeSet>(request).ConfigureAwait(false);
		}

		public Task Delete(long attributeSetId)
		{
			var request = new RestRequest("eav/attribute-sets/{id}", Method.DELETE);
			request.AddOrUpdateParameter("id", attributeSetId, ParameterType.UrlSegment);
			return this.Client.ExecuteAsync(request);
		}

		public async Task<long> CreateProductAttributeGroup(long attributeSetId, string attributeGroupName)
		{
			var request = new RestRequest("products/attribute-sets/{attributeSetId}/groups", Method.PUT);
			request.SetScope("all");
			request.AddOrUpdateParameter("attributeSetId", attributeSetId, ParameterType.UrlSegment);
			request.AddJsonBody(new {
				group = new {attribute_group_name = attributeGroupName, attribute_set_id = attributeSetId}
			});
			var response = await ExecuteAsync<AttributeGroup>(request).ConfigureAwait(false);

			return response.AttributeGroupId;
		}

		public Task<AttributeSet> Get(long id)
		{
			var request = new RestRequest("eav/attribute-sets/{id}", Method.GET);
			request.AddOrUpdateParameter("id", id, ParameterType.UrlSegment);

			return ExecuteAsync<AttributeSet>(request);
		}

		public Task AssignProductAttribute(long attributeSetId, long attributeGroupId, string attributeCode,
			int sortOrder = 1)
		{
			var request = new RestRequest("products/attribute-sets/attributes", Method.POST);

			request.AddJsonBody(new {attributeSetId, attributeCode, attributeGroupId, sortOrder});
			return ExecuteAsync(request);
		}

		public Task RemoveProductAttribute(long attributeSetId, string attributeCode)
		{
			var request = new RestRequest("products/attribute-sets/{attributeSetId}/attributes/{attributeCode}",
				Method.DELETE);
			request.AddOrUpdateParameter("attributeSetId", attributeSetId, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);

			return this.Client.ExecuteAsync(request);
		}

		public IQueryable<AttributeSet> AsQueryable()
		{
			return new MagentoQueryable<AttributeSet>(this.Client, "eav/attribute-sets/list", this.Cache,
				TimeSpan.FromMinutes(1));
		}
	}
}