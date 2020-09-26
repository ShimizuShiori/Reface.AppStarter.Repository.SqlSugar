using Reface.AppStarter.Attributes;
using System;
using System.Linq.Expressions;

namespace Reface.AppStarter.Repository.SqlSugar
{
    [Component]
    public class SqlSugarRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, new()
    {
        private readonly ISqlSugarClientProvider sqlSugarClientProvider;

        public SqlSugarRepository(ISqlSugarClientProvider sqlSugarClientProvider)
        {
            this.sqlSugarClientProvider = sqlSugarClientProvider;
        }

        public void Clear()
        {
            this.sqlSugarClientProvider.Provide().Deleteable<TEntity>(x => true).ExecuteCommand();
        }

        public void Delete(TEntity entity)
        {
            this.sqlSugarClientProvider.Provide().Deleteable<TEntity>(entity).ExecuteCommand();
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            this.sqlSugarClientProvider.Provide().Deleteable<TEntity>()
                .Where(where)
                .ExecuteCommand();
        }

        public void Insert(TEntity entity)
        {
            this.sqlSugarClientProvider.Provide().Insertable<TEntity>(entity).ExecuteCommand();
        }

        public ISelector<TEntity> Select()
        {
            return new SqlSugarSelectResult<TEntity>(this.sqlSugarClientProvider.Provide().Queryable<TEntity>());
        }

        public void Update(TEntity entity)
        {
            this.sqlSugarClientProvider.Provide().Updateable<TEntity>(entity).ExecuteCommand();
        }

        public IUpdateCondition<TEntity> Update(Expression<Func<TEntity, TEntity>> set)
        {
            var updator = this.sqlSugarClientProvider.Provide().Updateable<TEntity>().SetColumns(set);
            return new SqlSugarUpdateCondition<TEntity>(updator);
        }
    }
}
