using Reface.AppStarter.Repository.SqlSugar.Tests.Entities;
using Reface.EventBus;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.Events
{
    public class UserCreatedEvent : Event
    {
        public User User { get; private set; }
        public UserCreatedEvent(object source,User user) : base(source)
        {
            this.User = user;
        }
    }
}
