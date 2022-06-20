using System.Linq;
using System.Threading.Tasks;
using Magento.RestClient.Abstractions;
using Magento.RestClient.Expressions;
using Magento.RestClient.Modules.Inventory.Abstractions;
using Magento.RestClient.Modules.Inventory.Models;
using RestSharp;

namespace Magento.RestClient.Modules.Inventory
{
	public class InventorySourceRepository : AbstractRepository, IInventorySourceRepository
	{
		public InventorySourceRepository(IMagentoContext magentoContext) : base(magentoContext)
		{
		}


		/// <summary>
		///     Create
		/// </summary>
		/// <param name="source"></param>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException">Ignore.</exception>
		public Task Create(InventorySource source)
		{
			var request = new RestRequest("inventory/sources");

			request.AddJsonBody(new {source});
			request.Method = Method.Post;
			return ExecuteAsync(request);
		}

		public Task<InventorySource> GetByCode(string sourceCode)
		{
			var request = new RestRequest("inventory/sources/{sourceCode}");

			request.AddUrlSegment("sourceCode", sourceCode);

			request.Method = Method.Get;
			return ExecuteAsync<InventorySource>(request);
		}


		/// <summary>
		///     Update
		/// </summary>
		/// <param name="sourceCode"></param>
		/// <param name="source"></param>
		/// <returns></returns>
		/// <exception cref="Magento.RestClient.Exceptions.Generic.MagentoException">Ignore.</exception>
		public Task Update(string sourceCode, InventorySource source)
		{
			var request = new RestRequest("inventory/sources/{sourceCode}");

			request.AddUrlSegment("sourceCode", sourceCode);
			request.Method = Method.Put;
			return ExecuteAsync(request);
		}

		public IQueryable<InventorySource> AsQueryable()
		{
			return new MagentoQueryable<InventorySource>(this.Client, "inventory/sources");
		}
	}
}