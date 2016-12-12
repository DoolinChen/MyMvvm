using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMvvm.Model.Common
{
  public interface IRepository<TEntity, TPrimaryKey> : IReadOnlyRepository<TEntity> where TEntity : class {
    TEntity Find(TPrimaryKey key);
    void Remove(TEntity entity);
    TEntity Create();
    TEntity Reload(TEntity entity);
    Expression<Func<TEntity, TPrimaryKey>> GetPrimaryKeyExpression { get; }
    TPrimaryKey GetPrimaryKey(TEntity entity);
    bool HasPrimaryKey(TEntity entity);
    void SetPrimaryKey(TEntity entity, TPrimaryKey key);
  }
}
