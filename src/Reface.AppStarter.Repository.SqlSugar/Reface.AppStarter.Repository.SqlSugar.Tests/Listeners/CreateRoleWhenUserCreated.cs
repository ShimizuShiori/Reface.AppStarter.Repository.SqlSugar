using Reface.AppStarter.Attributes;
using Reface.AppStarter.Repository.SqlSugar.Tests.Entities;
using Reface.AppStarter.Repository.SqlSugar.Tests.Events;
using Reface.AppStarter.Repository.SqlSugar.Tests.Services;
using Reface.EventBus;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.Listeners
{
    [Listener]
    public class CreateRoleWhenUserCreated : IEventListener<UserCreatedEvent>
    {
        private IRoleService roleService;

        public CreateRoleWhenUserCreated(IRoleService roleService)
        {
            this.roleService = roleService;
        }

        public void Handle(UserCreatedEvent @event)
        {
            this.roleService.Create(new Role()
            {
                Code = @event.User.Name,
                Name = "角色_" + @event.User.Name
            });
        }
    }
}
