using Reface.AppStarter.Attributes;
using Reface.AppStarter.Repository.SqlSugar.Events;
using Reface.EventBus;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.Listeners
{
    [Listener]
    public class SetEntityInfoToAppContext : IEventListener<TableSchemaSyncedEvent>
    {
        private readonly App app;

        public SetEntityInfoToAppContext(App app)
        {
            this.app = app;
        }

        public void Handle(TableSchemaSyncedEvent @event)
        {
            app.Context[Constant.APP_CONTEXT_KEY_ENTITY_INFOS] = @event.EntityInfos;
        }
    }
}
