using Reface.EventBus;
using SqlSugar;
using System.Collections.Generic;

namespace Reface.AppStarter.Repository.SqlSugar.Events
{
    /// <summary>
    /// 当数据库中的结构被同步完成后的事件
    /// </summary>
    public class TableSchemaSyncedEvent : Event
    {
        /// <summary>
        /// 所有参与了表结构同步的实体类型
        /// </summary>
        public IEnumerable<EntityInfo> EntityInfos { get; private set; }

        public TableSchemaSyncedEvent(object source, IEnumerable<EntityInfo> entityInfos) : base(source)
        {
            this.EntityInfos = entityInfos;
        }
    }
}
