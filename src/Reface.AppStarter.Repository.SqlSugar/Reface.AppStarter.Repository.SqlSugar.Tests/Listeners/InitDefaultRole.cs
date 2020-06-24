using Reface.AppStarter.Attributes;
using Reface.AppStarter.Repository.SqlSugar.Events;
using Reface.AppStarter.Repository.SqlSugar.Tests.Entities;
using Reface.EventBus;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.Listeners
{
    [Listener]
    public class InitDefaultRole : IEventListener<TableSchemaSyncedEvent>
    {
        private readonly IRepository<Role> roles;

        public InitDefaultRole(IRepository<Role> roles)
        {
            this.roles = roles;
        }

        public void Handle(TableSchemaSyncedEvent @event)
        {
            if (this.roles.Select().Where(x => x.Code == Constant.ROLE_DEFAULT).Count() == 1)
                return;

            roles.Insert(new Role() { Code = Constant.ROLE_DEFAULT, Name = "默认角色" });
        }
    }
}
