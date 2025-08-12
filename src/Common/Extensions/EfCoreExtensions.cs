using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace dotnet_qrshop.Infrastructure.Database.Extensions
{
  public static class EfCoreExtensions
  {
    public static IQueryable<T> IncludeIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, object>> navigationPropertyPath)
        where T : class
    {
      return condition ? source.Include(navigationPropertyPath) : source;
    }

    public static IIncludableQueryable<T, TProperty> IncludeIf<T, TProperty>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, TProperty>> navigationPropertyPath)
        where T : class
    {
      return condition ? source.Include(navigationPropertyPath) : (IIncludableQueryable<T, TProperty>)source;
    }
  }
}
