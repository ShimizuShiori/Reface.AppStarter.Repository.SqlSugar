using SqlSugar;
using System;
using System.Linq.Expressions;

namespace Reface.AppStarter.Repository.SqlSugar
{
    public class SqlSugarUpdateCondition<TEntity> : IUpdateCondition<TEntity>
        where TEntity : class, new()
    {
        private readonly IUpdateable<TEntity> updateable;

        public SqlSugarUpdateCondition(IUpdateable<TEntity> updateable)
        {
            this.updateable = updateable;
        }

        public void Where(Expression<Func<TEntity, bool>> where)
        {
            this.updateable.Where(where).ExecuteCommand();
        }
    }
}
