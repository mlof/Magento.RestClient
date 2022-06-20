using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using AgileObjects.NetStandardPolyfills;
using Magento.RestClient.Extensions;
using Magento.RestClient.Search;
using Remotion.Linq.Parsing;

namespace Magento.RestClient.Expressions.QueryGeneration
{
	public class MagentoExpressionTreeVisitor : ThrowingExpressionVisitor
	{
		private readonly QueryPartAggregator _aggregator;
		private Filter _currentFilter;

		public MagentoExpressionTreeVisitor(QueryPartAggregator queryPartAggregator)
		{
			_aggregator = queryPartAggregator;

			_currentFilter = new Filter();
		}

		protected override Exception CreateUnhandledItemException<T>(T unhandledItem, string visitMethod)
		{
			throw new NotImplementedException();
		}

		protected override Expression VisitUnary(UnaryExpression expression)
		{
			if (expression.NodeType == ExpressionType.Convert)
			{
				Visit(expression.Operand);
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
			Debug.Assert(enumType != null, nameof(enumType) + " != null");
			var enumMemberAttribute =
				((EnumMemberAttribute[]) enumType.GetField(name!)!
					.GetCustomAttributes(typeof(EnumMemberAttribute), true))
				.Single();
			return enumMemberAttribute.Value;
		}
		public static bool IsNullableEnum(Type t)
		{
			Type u = Nullable.GetUnderlyingType(t);
			return (u != null) && u.IsEnum;
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
				if (_currentFilter.PropertyType.IsEnum || IsNullableEnum(_currentFilter.PropertyType))
				{

					Type enumType = null;
					if (_currentFilter.PropertyType.IsNullableType())
					{
						
						enumType = Nullable.GetUnderlyingType(_currentFilter.PropertyType);
					}
					else
					{
						enumType = _currentFilter.PropertyType;
					}
					var value = Enum.ToObject(enumType!,
						(int) (expression.Value ?? throw new InvalidOperationException()));
					var name = value.ToString();
					if (name != null)
					{
						var enumMemberAttribute = (enumType!.GetField(name) ??
						                           throw new InvalidOperationException())
							.GetCustomAttributes(typeof(EnumMemberAttribute)).OfType<EnumMemberAttribute>()
							.SingleOrDefault();
						if (enumMemberAttribute != null)
						{
							_currentFilter.Value = enumMemberAttribute.Value;
						}
					}
				}
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