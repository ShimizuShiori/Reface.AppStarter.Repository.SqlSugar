using Reface.AppStarter.Attributes;
using Reface.AppStarter.Repository.Proxies;
using Reface.AppStarter.Repository.SqlSugar.Tests.Entities;
using Reface.AppStarter.Repository.SqlSugar.Tests.Events;
using System;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.Services
{
    public interface IUserService
    {
        void CreateTwoUsers(User user1, User user2);

        void Create(User user);
    }

    [Component]
    public class UserService : IUserService
    {
        private readonly IRepository<User> repositoryOfUser;
        private readonly IWork work;

        public UserService(IRepository<User> repositoryOfUser, IWork work)
        {
            this.repositoryOfUser = repositoryOfUser;
            this.work = work;
        }

        [Transcation]
        public void Create(User user)
        {
            if (string.IsNullOrEmpty(user.Id))
                user.Id = Guid.NewGuid().ToString();
            this.repositoryOfUser.Insert(user);

            this.work.PublishEvent(new UserCreatedEvent(this, user));
        }

        [Transcation]
        public void CreateTwoUsers(User user1, User user2)
        {
            this.repositoryOfUser.Insert(user1);
            throw new ApplicationException();
        }
    }
}
