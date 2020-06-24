using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Reface.AppStarter.Repository.SqlSugar
{
    public class SqlSugarSelectResult<TEntity> : ISelector<TEntity>
        where TEntity : class
    {

        private readonly ISugarQueryable<TEntity> sugarQueryable;

        public SqlSugarSelectResult(ISugarQueryable<TEntity> sugarQueryable)
        {
            this.sugarQueryable = sugarQueryable;
        }

        public int Count()
        {
            return this.sugarQueryable.Count();
        }

        public TEntity First()
        {
            return this.sugarQueryable.First();
        }

        public TEntity FirstOrNull()
        {
            return this.sugarQueryable.First();
        }

        public ISelector<TEntity> OrderByAsc(Expression<Func<TEntity, object>> expression)
        {
            this.sugarQueryable.OrderBy(expression, OrderByType.Asc);
            return this;
        }

        public ISelector<TEntity> OrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            return new SqlSugarSelectResult<TEntity>(this.sugarQueryable.OrderBy(expression, OrderByType.Desc));
        }

        public IEnumerable<TEntity> ToList()
        {
            return this.sugarQueryable.ToList();
        }

        public IEnumerable<TEntity> ToPageList(int pageIndex, int pageSize)
        {
            IEnumerable<TEntity> result;
            result = this.sugarQueryable.ToPageList(pageIndex, pageSize);
            return result;
        }

        public IEnumerable<TEntity> ToPageList(int pageIndex, int pageSize, out int count)
        {
            int resultCount = 0;
            IEnumerable<TEntity> result;
            result = this.sugarQueryable.ToPageList(pageIndex, pageSize, ref resultCount);
            count = resultCount;
            return result;
        }

        public ISelector<TEntity> Where(Expression<Func<TEntity, bool>> expression)
        {
            return new SqlSugarSelectResult<TEntity>(this.sugarQueryable.Where(expression)); ;
        }
    }
}
