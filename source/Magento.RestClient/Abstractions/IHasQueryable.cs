using System.Linq;

namespace Magento.RestClient.Abstractions
{
	public interface IHasQueryable<T>
	{
		IQueryable<T> AsQueryable();
	}
}