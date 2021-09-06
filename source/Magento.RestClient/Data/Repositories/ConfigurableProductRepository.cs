﻿using System.Collections.Generic;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Data.Repositories.Abstractions;
using Magento.RestClient.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	internal class ConfigurableProductRepository : AbstractRepository, IConfigurableProductRepository
	{
		private readonly IRestClient client;

		public ConfigurableProductRepository(IRestClient client)
		{
			this.client = client;
		}

		public void CreateChild(string parentSku, string childSku)
		{
			var request = new RestRequest("configurable-products/{sku}/child");
			request.Method = Method.POST;
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddJsonBody(new {childSku});

			var response = client.Execute(request);
			if (!response.IsSuccessful)
			{
				throw MagentoException.Parse(response.Content);
			}
		}

		public void DeleteChild(string parentSku, string childSku)
		{
			var request = new RestRequest("configurable-products/{sku}/child/{childSku}");
			request.Method = Method.DELETE;
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			request.AddOrUpdateParameter("childSku", childSku);

			var response = client.Execute(request);
			if (!response.IsSuccessful)
			{
				throw MagentoException.Parse(response.Content);
			}
		}

		public List<Product> GetConfigurableChildren(string parentSku)
		{
			var request = new RestRequest("configurable-products/{sku}/children");
			request.Method = Method.GET;

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);

			var response = client.Execute<List<Product>>(request);
			return HandleResponse(response);
		}

		

		
		public void CreateOption(string parentSku, ConfigurableProductOption option)
		{
			var request = new RestRequest("configurable-products/{sku}/options");
			request.Method = Method.POST;
			request.AddJsonBody(new {
				option = option 
			});
			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			client.Execute(request);
		}

		public List<ConfigurableProductOption> GetOptions(string parentSku)
		{
			var request = new RestRequest("configurable-products/{sku}/options/all");
			request.Method = Method.GET;

			request.AddOrUpdateParameter("sku", parentSku, ParameterType.UrlSegment);
			var response = client.Execute<List<ConfigurableProductOption>>(request);
			return HandleResponse(response);
		}
	}
}