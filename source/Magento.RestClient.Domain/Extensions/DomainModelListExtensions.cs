using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions.Domain;

namespace Magento.RestClient.Domain.Extensions
{
	public static class DomainModelListExtensions
	{
		public static Task SaveAllAsync(this IEnumerable<IDomainModel> domainModels)
		{
			return Task.WhenAll(domainModels.Select(model => model.SaveAsync()).ToList());
		}
	}
}