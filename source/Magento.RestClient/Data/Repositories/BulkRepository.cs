using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Magento.RestClient.Data.Models.Products;
using Magento.RestClient.Expressions;
using RestSharp;

namespace Magento.RestClient.Data.Repositories
{
	public class BulkRepository : AbstractRepository, IBulkRepository
	{
		private readonly IRestClient client;

		private IQueryable<BulkOperation> BulkRepositoryImplemention =>
			new MagentoQueryable<BulkOperation>(client, "bulk");

		public BulkRepository(IRestClient client)
		{
			this.client = client;
		}

		public BulkActionStatus GetStatus(string uuid)
		{
			return new();
		}

		public IEnumerator<BulkOperation> GetEnumerator()
		{
			return BulkRepositoryImplemention.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)BulkRepositoryImplemention).GetEnumerator();
		}

		public Type ElementType => BulkRepositoryImplemention.ElementType;

		public Expression Expression => BulkRepositoryImplemention.Expression;

		public IQueryProvider Provider => BulkRepositoryImplemention.Provider;
	}
}