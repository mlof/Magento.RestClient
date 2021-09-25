using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using Magento.RestClient.Extensions;
using Magento.RestClient.Search;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Remotion.Linq.Parsing;

namespace Magento.RestClient.Expressions.QueryGeneration
{
	public class MagentoExpressionTreeVisitor : ThrowingExpressionVisitor
	{
		private Filter _currentFilter;
		private readonly QueryPartAggregator _aggregator;

		public MagentoExpressionTreeVisitor(QueryPartAggregator queryPartAggregator)
		{
			this._aggregator = queryPartAggregator;

			this._currentFilter = new Filter();
		}


		protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
		{
			throw new NotImplementedException();
		}

		protected override Expression VisitUnary(UnaryExpression expression)
		{
			//_currentFilter.PropertyName = expression.;

			if (expression.NodeType == ExpressionType.Convert)
			{
				Visit(expression.Operand);
				var x = 9;
			}

			return expression;
		}

		protected override Expression VisitBinary(BinaryExpression expression)
		{
			Visit(expression.Left);


			_currentFilter.Condition = expression.NodeType switch {
				ExpressionType.Equal => SearchCondition.Equals,
				ExpressionType.GreaterThan => SearchCondition.GreaterThan,
				ExpressionType.GreaterThanOrEqual => SearchCondition.GreaterThanOrEqual,
				ExpressionType.LessThan => SearchCondition.LessThan,
				ExpressionType.LessThanOrEqual => SearchCondition.LessThanOrEqual,
				ExpressionType.NotEqual => SearchCondition.NotEqual,
				_ => SearchCondition.Equals
			};

			_aggregator.CreateNewFilterGroup();

			Visit(expression.Right);


			return expression;
		}


		protected override Expression VisitMethodCall(MethodCallExpression expression)
		{
			if (expression.Object is MemberExpression memberExpression)
			{
				Visit(memberExpression);
			}

			if (expression.Method.DeclaringType == typeof(string) && expression.Method.Name == "Contains")
			{
				_currentFilter.Condition = SearchCondition.Like;
				Visit(expression.Arguments.Single());
			}
		


			return expression;
		}


		protected override Expression VisitMember(MemberExpression expression)
		{
			_currentFilter.PropertyName = expression.Member.GetPropertyName();

			if (expression.Member is PropertyInfo member)
			{
				_currentFilter.PropertyType = member.PropertyType;
			}


			return expression;
		}

		public static string ToEnumString<T>(T type)
		{
			var enumType = typeof(T);
			var name = Enum.GetName(enumType, type);
			var enumMemberAttribute =
				((EnumMemberAttribute[]) enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true))
				.Single();
			return enumMemberAttribute.Value;
		}

		protected override Expression VisitConstant(ConstantExpression expression)
		{
			_currentFilter.Value = expression.Value;


			if (_currentFilter.Condition == SearchCondition.Like && _currentFilter.Value is string s)
			{
				_currentFilter.Value = $"%{s}%";
			}
			else
			{
				if (_currentFilter.PropertyType.IsEnum)
				{
					var value = Enum.ToObject(_currentFilter.PropertyType, (int) expression.Value);
					var name = value.ToString();
					if (name != null)
					{



						var enumMemberAttribute = _currentFilter.PropertyType.GetField(name)
							.GetCustomAttributes(typeof(EnumMemberAttribute)).OfType<EnumMemberAttribute>()
							.SingleOrDefault();
						if (enumMemberAttribute != null)
						{
							_currentFilter.Value = enumMemberAttribute.Value;
						}
					}
				}


				var x = 9;
			}

			_aggregator.AddToLastFilterGroup(new Filter(_currentFilter));
			_currentFilter = new Filter();

			return expression;
		}


		public static void SetParameters(QueryPartAggregator aggregator, Expression whereClausePredicate)
		{
			var visitor = new MagentoExpressionTreeVisitor(aggregator);
			visitor.Visit(whereClausePredicate);
		}
	}
}