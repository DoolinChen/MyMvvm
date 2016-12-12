using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMvvm.Model.Common
{
  public interface IUnitOfWork
  {
    void SaveChanges();
    EntityState GetState(object entity);
    void Update(object entity);
    void Detach(object entity);
    bool HasChanges();
  }
  public interface IUnitOfWorkFactory<TUnitOfWork> where TUnitOfWork : IUnitOfWork
  {

    /// <summary>
    /// Creates a new unit of work.
    /// </summary>
    TUnitOfWork CreateUnitOfWork();
  }
  #region Entity State & Entity Message
  public enum EntityState
  {

    /// <summary>
    /// The object exists but is not being tracked. 
    /// An entity is in this state immediately after it has been created and before it is added to the unit of work. 
    /// An entity is also in this state after it has been removed from the unit of work by calling the IUnitOfWork.Detach method.
    /// </summary>
    Detached = 1,

    /// <summary>
    /// The object has not been modified since it was attached to the unit of work or since the last time that the IUnitOfWork.SaveChanges method was called.
    /// </summary>
    Unchanged = 2,

    /// <summary>
    /// The object is new, has been added to the unit of work, and the IUnitOfWork.SaveChanges method has not been called. 
    /// After the changes are saved, the object state changes to Unchanged.
    /// </summary>
    Added = 4,

    /// <summary>
    /// The object has been deleted from the unit of work. After the changes are saved, the object state changes to Detached.
    /// </summary>
    Deleted = 8,

    /// <summary>
    /// One of the scalar properties on the object has been modified and the IUnitOfWork.SaveChanges method has not been called. 
    /// After the changes are saved, the object state changes to Unchanged.
    /// </summary>
    Modified = 16,
  }
  public enum EntityMessageType
  {

    /// <summary>
    /// The new entity has been added to the unit of work. 
    /// </summary>
    Added,

    /// <summary>
    /// The object has been deleted from the unit of work.
    /// </summary>
    Deleted,

    /// <summary>
    /// One of the properties on the object has been modified. 
    /// </summary>
    Changed
  }
  public class EntityMessage<TEntity>
  {

    /// <summary>
    /// The entity that has been added, deleted or modified.
    /// </summary>
    public TEntity Entity { get; private set; }

    /// <summary>
    /// The entity state change notification type.
    /// </summary>
    public EntityMessageType MessageType { get; private set; }

    /// <summary>
    /// Initializes a new instance of the EntityMessage class.
    /// </summary>
    /// <param name="entity">An entity that has been added, deleted or modified.</param>
    /// <param name="messageType">An entity state change notification type.</param>
    public EntityMessage(TEntity entity, EntityMessageType messageType)
    {
      this.Entity = entity;
      this.MessageType = messageType;
    }
  }

  #endregion
}
