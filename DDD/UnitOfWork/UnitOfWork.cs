using System.Collections.Generic;
using System.Transactions;
using XFramework.DDD.Domain;

namespace XFramework.DDD.UnitOfWork
{
    // 工作单元实现
    public class UnitOfWork : IUnitOfWork
    {
        // 引入了EF就不需要额外定义三个列表了，因为EF框架中包含的DbContext.DbSet<T>可以记录这3个列表
        // 然而在ByteartRetail案例中，也定义这3个列表，但其没有被真真使用到
        private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> _addedEntities;
        private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> _changedEntities;
        private readonly Dictionary<IAggregateRoot, IUnitOfWorkRepository> _deletedEntities;

        public UnitOfWork()
        {
            _addedEntities = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
            _changedEntities = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
            _deletedEntities = new Dictionary<IAggregateRoot, IUnitOfWorkRepository>();
        }

        // 将业务对象实体添加到内部列表中，真正完成实体持久化操作的还是由具体的仓储类去完成
        public void RegisterAmended(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository)
        {
            if (!_changedEntities.ContainsKey(entity))
            {
                _changedEntities.Add(entity, unitofWorkRepository);
            }
        }

        public void RegisterNew(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository)
        {
            if (!_addedEntities.ContainsKey(entity))
            {
                _addedEntities.Add(entity, unitofWorkRepository);
            };
        }

        public void RegisterRemoved(IAggregateRoot entity, IUnitOfWorkRepository unitofWorkRepository)
        {
            if (!_deletedEntities.ContainsKey(entity))
            {
                _deletedEntities.Add(entity, unitofWorkRepository);
            }
        }

        // 对内部列表进行统一提交
        // 引入EF后，提交的实现有点不同，它具体的持久化只需要调用DbContext.SaveChanges方法来完成
        // 则具体的仓储接口不需要实现IUnitOfWorkRepository接口，则自然不存在IUnitOfWorkRepository接口的定义
        public void Commit()
        {
            // 事务范围
            using (var scope = new TransactionScope())
            {
                // 分别调用具体的仓储对象的持久化逻辑来对业务对象进行持久化
                foreach (var entity in this._addedEntities.Keys)
                {
                    this._addedEntities[entity].PersistCreationOf(entity);
                }

                foreach (var entity in this._changedEntities.Keys)
                {
                    this._changedEntities[entity].PersistUpdateOf(entity);
                }

                foreach (var entity in this._deletedEntities.Keys)
                {
                    this._deletedEntities[entity].PersistDeletionOf(entity);
                }

                scope.Complete();
            }
        }
    }
}