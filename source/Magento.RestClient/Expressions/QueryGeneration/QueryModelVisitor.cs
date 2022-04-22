using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Magento.RestClient.Extensions;
using Magento.RestClient.Search;
using Remotion.Linq;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using RestSharp;

namespace Magento.RestClient.Expressions.QueryGeneration
{
	public class QueryModelVisitor : QueryModelVisitorBase
	{
		public QueryModelVisitor()
		{
			_queryPartAggregator = new QueryPartAggregator();
		}

		private readonly QueryPartAggregator _queryPartAggregator;

		public IRestRequest GetRequest(IRestRequest restRequest)
		{
			restRequest.AddParameter("searchCriteria[currentPage]", 1,
				ParameterType.QueryStringWithoutEncode);
			restRequest.AddOrUpdateParameter("searchCriteria[pageSize]", _queryPartAggregator.PageSize,
				ParameterType.QueryStringWithoutEncode);

			foreach (var (item, index) in _queryPartAggregator.Orderings.WithIndex())
			{
				restRequest.AddParameter(
					$"searchCriteria[sortOrders][{index}][field]",
					item.PropertyName, ParameterType.QueryStringWithoutEncode);
				restRequest.AddParameter(
					$"searchCriteria[sortOrders][{index}][direction]",
					item.Direction == OrderingDirection.Asc ? "ASC" : "DESC", ParameterType.QueryStringWithoutEncode);
			}

			foreach (var (item, filterGroupIndex) in _queryPartAggregator.Filtergroups.WithIndex())
			{
				foreach (var (filter, filterIndex) in item.WithIndex())
				{
					restRequest.AddParameter(
						$"searchCriteria[filter_groups][{filterGroupIndex}][filters][{filterIndex}][field]",
						filter.PropertyName, ParameterType.QueryStringWithoutEncode);

					restRequest.AddParameter(
						$"searchCriteria[filter_groups][{filterGroupIndex}][filters][{filterIndex}][condition_type]",
						filter.Condition.GetMagentoCondition(), ParameterType.QueryStringWithoutEncode);

					restRequest.AddParameter(
						$"searchCriteria[filter_groups][{filterGroupIndex}][filters][{filterIndex}][value]",
						filter.Value.ToString(), ParameterType.QueryStringWithoutEncode);
				}
			}

			return restRequest;
		}

		public override void VisitResultOperator(ResultOperatorBase resultOperator, QueryModel queryModel, int index)
		{
			if (resultOperator is TakeResultOperator takeResultOperator)
			{
				var constant = (ConstantExpression) takeResultOperator.Count;
				Debug.Assert(constant.Value != null, "constant.Value != null");
				_queryPartAggregator.PageSize = (int) constant.Value;
			}

			base.VisitResultOperator(resultOperator, queryModel, index);
		}

		public override void VisitWhereClause(WhereClause whereClause, QueryModel queryModel, int index)
		{
			ApplyParameters(whereClause.Predicate);

			base.VisitWhereClause(whereClause, queryModel, index);
		}

		public override void VisitOrderByClause(OrderByClause orderByClause, QueryModel queryModel, int index)
		{
			foreach (var ordering in orderByClause.Orderings)
			{
				if (ordering.Expression is MemberExpression memberExpression)
				{
					_queryPartAggregator.Orderings.Add(new OrderClause() {
						PropertyName = memberExpression.Member.GetPropertyName(), Direction = ordering.OrderingDirection
					});
				}
				else { throw new Exception("Complex orderings are not supported."); }
			}

			base.VisitOrderByClause(orderByClause, queryModel, index);
		}

		private void ApplyParameters(Expression whereClausePredicate)
		{
			MagentoExpressionTreeVisitor.SetParameters(_queryPartAggregator, whereClausePredicate);
		}
	}
}