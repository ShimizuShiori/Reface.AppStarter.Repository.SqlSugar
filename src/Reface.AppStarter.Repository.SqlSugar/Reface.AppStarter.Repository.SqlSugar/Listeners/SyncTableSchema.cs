using Reface.AppStarter.Attributes;
using Reface.AppStarter.Events;
using Reface.AppStarter.Repository.SqlSugar.Configs;
using Reface.AppStarter.Repository.SqlSugar.Events;
using Reface.EventBus;
using System.Diagnostics;
using System.Linq;

namespace Reface.AppStarter.Repository.SqlSugar.Listeners
{
    /// <summary>
    /// 同步数据库表结构
    /// </summary>
    [Listener]
    public class SyncTableSchema : IEventListener<EntitiesFoundEvent>
    {
        private readonly ISqlSugarClientProvider sqlSugarClientProvider;
        private readonly IEventBus eventBus;
        private readonly DatabaseConfig databaseConfig;

        public SyncTableSchema(ISqlSugarClientProvider sqlSugarClientProvider, IEventBus eventBus, DatabaseConfig databaseConfig)
        {
            this.sqlSugarClientProvider = sqlSugarClientProvider;
            this.eventBus = eventBus;
            this.databaseConfig = databaseConfig;
        }

        public void Handle(EntitiesFoundEvent @event)
        {
            if (!this.databaseConfig.SyncTableSchema) return;
            var client = this.sqlSugarClientProvider.Provide();
            client.CodeFirst.InitTables(@event.EntityTypes.ToArray());
            var entityInfos = @event.EntityTypes.Select(x => client.EntityMaintenance.GetEntityInfo(x));
            this.eventBus.Publish(new TableSchemaSyncedEvent(this, entityInfos));
            Debug.WriteLine("Tables Inited");
        }
    }
}
