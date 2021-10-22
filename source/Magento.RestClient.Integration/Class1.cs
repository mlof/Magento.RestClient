using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AgileObjects.AgileMapper;
using AgileObjects.AgileMapper.Extensions;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Domain;
using Magento.RestClient.Configuration;
using Magento.RestClient.Context;
using Magento.RestClient.Data.Models.Common;
using Magento.RestClient.Domain.Extensions;
using Magento.RestClient.Domain.Models.EAV;
using Magento.RestClient.Extensions;
using Magento.RestClient.Integration.Fixtures;
using Newtonsoft.Json;
using Attribute = Magento.RestClient.Integration.Fixtures.Attribute;

namespace Magento.RestClient.Integration
{
	public class FixtureLoader
	{
		private readonly IMapper mapper;

		public static void Main(string[] args)
		{
			var loader = new FixtureLoader();


			loader.Execute().GetAwaiter().GetResult();
		}

		private Task Execute()
		{
			var fixtureText = File.ReadAllText("Fixtures.json");

			var fixtures = JsonConvert.DeserializeObject<FixtureFile>(fixtureText);


			var domainModels = new List<IDomainModel>();

			var attributeModels = mapper.Map(fixtures.Attributes).ToANew<List<AttributeModel>>();
			domainModels.AddRange(attributeModels);


			return domainModels.SaveAllAsync();
		}

		public FixtureLoader()
		{
			this.Context = new MagentoAdminContext(new MagentoClientOptions());

			this.mapper = Mapper.CreateNew();
			mapper.WhenMapping.From<Attribute>()
				.To<AttributeModel>()
				.CreateInstancesUsing(data =>
					new AttributeModel(Context, data.Source.AttributeCode, data.Source.DefaultFrontendLabel));
		}

		public IAdminContext Context { get; set; }
	}
}