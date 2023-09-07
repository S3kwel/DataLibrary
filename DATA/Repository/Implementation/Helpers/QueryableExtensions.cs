using DATA.Repository.Abstraction;
using DATA.Repository.Implementation.Filtering;
using DATA.Repository.Implementation.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DATA.Repository.Implementation.Helpers
{
    public static class QueryableExtensions<T> where T : BaseEntity
    {
        internal static IQueryable<T> ApplyEagerLoading(IQueryable<T> query, BaseFilter<T> filter)
        {
            if (filter.Includes.Count == 0 || filter == null) return query;

            foreach (var e in filter.Includes)
            {
                query = query.Include(e);
            }
            return query;
        }
        internal static IQueryable<T> DisableQueryFilters(IQueryable<T> query, BaseFilter<T> filter)
        {
            if (filter.IncludeDeletedRecords)
                return query.IgnoreQueryFilters();
            return query;
        }
        internal static IQueryable<T> ApplyOrdering(IQueryable<T> query, List<OrderingCriteria> criteria)
        {
            if (criteria == null || !criteria.Any()) return query;
            bool isFirstCriteria = true;
            foreach (var c in criteria)
            {
                if (isFirstCriteria)
                {
                    query = ApplySingleOrderingExpression(query, c.PropertyName, c.Descending);
                    isFirstCriteria = false;
                }

                var orderingDelegate = ApplyOrderingExpression(c.PropertyName);
                if (c.Descending)
                {
                    query = ((IOrderedQueryable<T>)query).OrderByDescending(orderingDelegate);
                }
                else
                {
                    query = ((IOrderedQueryable<T>)query).OrderBy(orderingDelegate);
                }
            }
            return query;
        }

        internal static IQueryable<T> ApplyFilteringLogic(IQueryable<T> query, BaseFilter<T> filter)
        {
            if (!(filter is CompositeSpecification<T> specification)) return query;

            var predicate = PredicateBuilder.New(specification.Criteria);

            foreach (var andCriteria in specification.AndCriteria)
            {
                predicate = predicate.And(andCriteria);
            }

            foreach (var orCriteria in specification.OrCriteria)
            {
                predicate = predicate.Or(orCriteria);
            }

            return query.Where(predicate);
        }

        internal static IQueryable<T> ApplySingleOrderingExpression(IQueryable<T> query, string propertyName, bool descending = false)
        {
            var orderingDelegate = ApplyOrderingExpression(propertyName);
            if (descending)
            {
                return query.OrderByDescending(orderingDelegate);
            }

            return query.OrderBy(orderingDelegate);
        }
        internal static Expression<Func<T, object>> ApplyOrderingExpression(string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, propertyName);
            var conversion = Expression.Convert(property, typeof(object));

            return Expression.Lambda<Func<T, object>>(conversion, parameter);
        }
        internal static IQueryable<T> ApplyOrderingLogic(IQueryable<T> query, BaseFilter<T> filter)
        {
            if (filter.OrderingCriteria != null)
            {
                query = ApplyOrdering(query, filter.OrderingCriteria);
            }
            return query;
        }



    }



}
