using System.Linq;

namespace Magento.RestClient.Abstractions.Abstractions
{
	public interface IHasQueryable<out T>
	{
		IQueryable<T> AsQueryable();
	}
}