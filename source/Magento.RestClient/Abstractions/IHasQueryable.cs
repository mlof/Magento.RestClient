using System.Linq;

namespace Magento.RestClient.Abstractions
{
	public interface IHasQueryable<out T>
	{
		IQueryable<T> AsQueryable();
	}
}