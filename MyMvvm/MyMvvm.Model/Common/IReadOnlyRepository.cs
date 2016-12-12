using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace MyMvvm.Model.Common
{
  public interface IReadOnlyRepository<TEntity> where TEntity : class
  {
    IQueryable<TEntity> GetEntities();
    IUnitOfWork UnitOfWork { get; }
    ObservableCollection<TEntity> Local { get; }
  }
  public static class ReadOnlyRepositoryExtensions
  {
    public static IQueryable<TEntity> GetFilteredEntities<TEntity>(this IReadOnlyRepository<TEntity> repository, Expression<Func<TEntity, bool>> filterExpression) where TEntity : class
    {
      var queryable = repository.GetEntities();
      if (filterExpression != null)
        queryable = queryable.Where(filterExpression);
      return queryable;
    }
  }
}
