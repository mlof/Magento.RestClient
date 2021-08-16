using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using RestSharp;

namespace Magento.RestClient.Search
{
    public class SearchBuilder<T>
    {
        public const int DefaultPageSize = 10;
        public const int DefaultPage = 1;
        private readonly IRestRequest _restRequest;
        private readonly List<SearchFilter> _filters;
        private int _currentPage;
        private int _pageSize;

        public SearchBuilder(IRestRequest restRequest)
        {
            this._restRequest = restRequest;
            this._filters = new List<SearchFilter>();
            this._pageSize = DefaultPageSize;
            this._currentPage = DefaultPage;
        }


        private SearchBuilder<T> Where(Expression<Func<T, object>> func, SearchCondition condition, object value = null)
        {
            var expression = GetMemberInfo(func);


            var p = expression.Member.GetCustomAttributes(false).OfType<JsonPropertyAttribute>()
                .Single();
            switch (condition)
            {
                case SearchCondition.NotNull or SearchCondition.IsNull:
                    throw new InvalidOperationException($"{condition} does not support being called with an argument");
                case SearchCondition.InSet or SearchCondition.NotIn:
                {
                    throw new NotImplementedException();
                    //throw new InvalidOperationException($"{condition} needs to be called with an IEnumerable");
                }
                default:
                    break;
            }


            _filters.Add(new SearchFilter(p.PropertyName, condition, value));

            return this;
        }

        public SearchBuilder<T> WhereEquals(Expression<Func<T, object>> func,  object value = null)
        {
            return Where(func, SearchCondition.Equals, value);
        }

        private string GetMagentoCondition(SearchCondition condition)
        {
            return condition switch {
                SearchCondition.Equals => "eq",
                SearchCondition.InSet => "finset",
                SearchCondition.From => "from",
                SearchCondition.GreaterThan => "gt",
                SearchCondition.GreaterThanOrEqual => "gteq",
                SearchCondition.Like => "like",
                SearchCondition.LessThan => "lt",
                SearchCondition.LessThanOrEqual => "lteq",
                SearchCondition.MoreOrEqual => "moreq",
                SearchCondition.NotEqual => "neq",
                SearchCondition.NotWithinSet => "nfinset",
                SearchCondition.NotIn => "nin",
                SearchCondition.NotNull => "notnull",
                SearchCondition.IsNull => "isnull",
                SearchCondition.To => "to",
                _ => ""
            };
        }

        private static MemberExpression GetMemberInfo(Expression method)
        {
            if (method is not LambdaExpression lambda)
            {
                throw new ArgumentNullException("method");
            }

            MemberExpression memberExpr = null;

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpr =
                        ((UnaryExpression) lambda.Body).Operand as MemberExpression;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpr = lambda.Body as MemberExpression;
                    break;
            }

            if (memberExpr == null)
            {
                throw new ArgumentException("method");
            }

            return memberExpr;
        }

        public IRestRequest Build()
        {
            _restRequest.AddParameter("searchCriteria[pageSize]", _pageSize, ParameterType.QueryStringWithoutEncode);
            _restRequest.AddParameter("searchCriteria[currentPage]", _currentPage, ParameterType.QueryStringWithoutEncode);


            if (_filters.Any())
            {
                foreach (var filter in _filters.Select((filter, i) => new {filter, i}))
                {
                    var i = filter.i;
                    var searchFilter = filter.filter;

                    _restRequest.AddParameter($"searchCriteria[filter_groups][{i}][filters][0][field]",
                        searchFilter.PropertyName, ParameterType.QueryStringWithoutEncode);

                    _restRequest.AddParameter($"searchCriteria[filter_groups][{i}][filters][0][condition_type]",
                        GetMagentoCondition(searchFilter.Condition), ParameterType.QueryStringWithoutEncode);

                    if (searchFilter.Value != null)
                    {
                        var value = "";
                        if (searchFilter.Value is string s)
                        {
                            value = s;
                            
                        }
                        else
                        {
                            value = JsonConvert.SerializeObject(searchFilter.Value);
                        }

                        _restRequest.AddParameter($"searchCriteria[filter_groups][{i}][filters][0][value]",
                            value, ParameterType.QueryStringWithoutEncode);
                    }
                }
            }

            return _restRequest;
        }

        public SearchBuilder<T> WithPageSize(int pageSize)
        {
            this._pageSize = pageSize;
            return this;
        }

        public SearchBuilder<T> WithPage(int currentPage)
        {
            this._currentPage = currentPage;
            return this;
        }
    }
}