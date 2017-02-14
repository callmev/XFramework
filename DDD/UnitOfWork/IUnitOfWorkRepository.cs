using System.Collections;
using XFramework.DDD.Domain;

namespace XFramework.DDD.UnitOfWork
{
    public interface IUnitOfWorkRepository
    {
        Hashtable AccountList { get; }
        void PersistCreationOf(IAggregateRoot entity);
        void PersistUpdateOf(IAggregateRoot entity);
        void PersistDeletionOf(IAggregateRoot entity); 
    }
}