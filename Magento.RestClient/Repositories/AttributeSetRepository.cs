using System;
using System.Collections.Generic;
using Magento.RestClient.Domain;
using Magento.RestClient.Exceptions;
using Magento.RestClient.Models;
using Magento.RestClient.Repositories.Abstractions;
using Magento.RestClient.Search;
using Magento.RestClient.Search.Extensions;
using RestSharp;

namespace Magento.RestClient.Repositories
{
    public class AttributeSetRepository : IAttributeSetRepository
    {
        private readonly IRestClient _client;

        public AttributeSetRepository(IRestClient client)
        {
            this._client = client;
        }


        public void Create(EntityType entityTypeCode, int skeletonId, AttributeSet attributeSet)
        {
            var request = new RestRequest("eav/attribute-sets");
            request.Method = Method.POST;
            request.AddJsonBody(new {attributeSet, skeletonId, entityTypeCode = entityTypeCode.ToTypeCode()});

            _client.Execute(request);
        }

        public void Delete(long attributeSetId)
        {
            var request = new RestRequest("eav/attribute-sets/{id}");
            request.Method = Method.DELETE;
            request.AddOrUpdateParameter("id", attributeSetId, ParameterType.UrlSegment);
            _client.Execute(request);
        }


        public long CreateProductAttributeGroup(long attributeSetId, string attributeGroupName)
        {
            var request = new RestRequest("products/attribute-sets/{attributeSetId}/groups");
            request.Method = Method.PUT;
            request.AddOrUpdateParameter("attributeSetId", attributeSetId, ParameterType.UrlSegment);
            request.AddJsonBody(new {
                group = new {attribute_group_name = attributeGroupName, attribute_set_id = attributeSetId}
            });
            var response = _client.Execute<AttributeGroup>(request);

            if (response.IsSuccessful)
            {
                return response.Data.AttributeGroupId;
            }
            else
            {
                throw MagentoException.Parse(response.Content);
            }
        }

        public void AssignProductAttribute(long attributeSetId, long attributeGroupId, string attributeCode,
            int sortOrder = 1)
        {
            var request = new RestRequest("products/attribute-sets/attributes");
            request.Method = Method.POST;

            request.AddJsonBody(new {attributeSetId, attributeCode, attributeGroupId, sortOrder});
            _client.Execute(request);
        }

        public void RemoveProductAttribute(long attributeSetId, string attributeCode)
        {
            var request = new RestRequest("products/attribute-sets/{attributeSetId}/attributes/{attributeCode}");
            request.Method = Method.DELETE;
            request.AddOrUpdateParameter("attributeSetId", attributeSetId, ParameterType.UrlSegment);
            request.AddOrUpdateParameter("attributeCode", attributeCode, ParameterType.UrlSegment);

            _client.Execute(request);
        }
    }
}