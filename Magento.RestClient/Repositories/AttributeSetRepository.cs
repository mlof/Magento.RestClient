using System;
using System.Collections.Generic;
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

       
    }
}