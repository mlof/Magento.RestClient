using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Modules.Inventory.Abstractions;
using Magento.RestClient.Modules.Inventory.Models;
using RestSharp;

namespace Magento.RestClient.Modules.Inventory
{
	public class InventorySourceItemRepository : AbstractRepository, IInventorySourceItemRepository
	{
		public InventorySourceItemRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}


		public IQueryable<InventorySourceItem> AsQueryable()
		{
			return new MagentoQueryable<InventorySourceItem>(this.Client, "inventory/source-items");
		}

		/// <summary>
		///     Create
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException">Ignore.</exception>
		public Task Create(params InventorySourceItem[] items)
		{
			var request = new RestRequest("inventory/source-items") {Method = Method.Post};
			request.AddJsonBody(new {sourceItems = items});

			return ExecuteAsync(request);
		}

		/// <summary>
		///     Delete
		/// </summary>
		/// <param name="items"></param>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException">Ignore.</exception>
		public Task Delete(params InventorySourceItem[] items)
		{
			var request = new RestRequest("inventory/source-items") {Method = Method.Delete};
			request.AddJsonBody(new {sourceItems = items});

			return ExecuteAsync(request);
		}
	}
}