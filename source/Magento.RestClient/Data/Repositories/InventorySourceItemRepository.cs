using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Abstractions.Abstractions;
using Magento.RestClient.Abstractions.Repositories;
using Magento.RestClient.Data.Models.Inventory;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class InventorySourceItemRepository : AbstractRepository, IInventorySourceItemRepository
	{
		public InventorySourceItemRepository(IContext context) : base(context)
		{
		}


		public IQueryable<InventorySourceItem> AsQueryable()
		{
			return new MagentoQueryable<InventorySourceItem>(Client, "inventory/source-items");
		}

		/// <summary>
		/// Create
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException">Ignore.</exception>
		public Task Create(params InventorySourceItem[] items)
		{
			var request = new RestRequest("inventory/source-items") {Method = Method.POST};
			request.AddJsonBody(new {sourceItems = items});

			return ExecuteAsync(request);
		}

		/// <summary>
		/// Delete
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException">Ignore.</exception>
		public Task Delete(params InventorySourceItem[] items)
		{
			var request = new RestRequest("inventory/source-items") {Method = Method.DELETE};
			request.AddJsonBody(new { sourceItems = items });

			return ExecuteAsync(request);
		}
	}
}