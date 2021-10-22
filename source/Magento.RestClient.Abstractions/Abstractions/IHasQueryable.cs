using System.Linq;

namespace Magento.RestClient.Abstractions.Abstractions
{
	public interface IHasQueryable<T>
	{
		IQueryable<T> AsQueryable();
	}
}