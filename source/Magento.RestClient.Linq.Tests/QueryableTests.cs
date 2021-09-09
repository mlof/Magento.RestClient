using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JsonExts.JsonPath;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Expressions;
using Magento.RestClient.Tests.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NUnit.Framework;
using Remotion.Linq;
using Remotion.Linq.Parsing.Structure;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serializers.NewtonsoftJson;

namespace Magento.RestClient.Linq.Tests
{
	public class QueryableTests
	{
		private MagentoQueryable<Product> products;

		[SetUp]
		public void QueryableSetup()
		{
			var conf = TestConfiguration.GetInstance();
			var host = "http://localhost";
			var baseUrl = "";
			if (host.EndsWith("/"))
			{
				host = host.TrimEnd('/');
			}

			baseUrl = $"{host}/rest/{{scope}}/V1/";
			var adminTokenUrl = $"{host}/rest/V1/integration/admin/token";
			var customerTokenUrl = host + "/rest/V1/integration/customer/token";

			var restClient = new RestSharp.RestClient(baseUrl);

			restClient.Authenticator = OAuth1Authenticator.ForProtectedResource(conf.ConsumerKey, conf.ConsumerSecret,
				conf.AccessToken, conf.AccessTokenSecret);


			restClient.AddDefaultUrlSegment("scope", "default");
			restClient.UseNewtonsoftJson(new JsonSerializerSettings {
				NullValueHandling = NullValueHandling.Ignore,
				Culture = CultureInfo.InvariantCulture,
				Formatting = Formatting.Indented,
				DefaultValueHandling = DefaultValueHandling.Ignore,
				Converters = new List<JsonConverter> {
					new IsoDateTimeConverter {DateTimeStyles = DateTimeStyles.AssumeUniversal},
					new JsonPathObjectConverter()
				}
			});

			this.products = new MagentoQueryable<Product>(restClient, "products");
		}


		[Test]
		public void GetProducts_WhereSkuEquals()
		{
			var result = products.Where(product => product.Sku == "TESTSKU")
				.ToList();
		}

		[Test]
		public void GetProducts_WhereSkuContains_AndSkuContains()
		{
			var result = products.Where(product => product.Sku.Contains("TEST") && product.Sku.Contains("SKU"))
				.ToList();
		}

		[Test]
		public void GetProducts_WhereSkuContains_OrPriceBelow()
		{
			var result = products.Where(product => product.Sku.Contains("TEST") || product.Price < 10)
				.ToList();
		}

		[Test]
		public void GetProducts_WhereSkuContains()
		{
			var result = products.Where(product => product.Sku.Contains("TEST"))
				.ToList();
		}

		[Test]
		public void GetProducts_Simple()
		{
			var result = products
				.ToList();
		}

		[Test]
		public void GetProducts_OrderByPrice()
		{
			var result = products
				.OrderBy(product => product.Price)
				.ToList();
		}

		[Test]
		public void GetProducts_WherePriceLessThan()
		{
			var result = products.Where(product => product.Price < 5)
				.ToList();
		}

		[Test]
		public void GetProducts_WherePriceGreaterThanOrEqual()
		{
			var result = products.Where(product => product.Price >= 10)
				.ToList();
		}
	}
}