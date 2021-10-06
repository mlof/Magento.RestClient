using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Domain.Abstractions;

namespace Magento.RestClient.Extensions
{
	public static class DomainModelExtensions
	{
		public static Task SaveAllAsync(this IEnumerable<IDomainModel> domainModels)
		{
			return Task.WhenAll(domainModels.Select(model => model.SaveAsync()).ToList());
		}
	}
}