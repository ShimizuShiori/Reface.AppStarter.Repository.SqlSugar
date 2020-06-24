using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Repository.SqlSugar.Tests.AppModules;
using Reface.AppStarter.Repository.SqlSugar.Tests.Entities;
using Reface.AppStarter.Repository.SqlSugar.Tests.Services;
using Reface.AppStarter.UnitTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reface.AppStarter.Repository.SqlSugar.Tests
{
    [TestClass]
    public class TranTests : TestClassBase<TestAppModule>
    {
        public IRepository<User> RepositoryOfUser { get; set; }

        public IUserService UserService { get; set; }

        public IRoleService RoleService { get; set; }

        protected override void OnAppStarted()
        {
            this.RepositoryOfUser.Clear();
        }

        [TestMethod]
        public void RollbackWhenThrowAnException()
        {
            User u1 = new User() { Id = Guid.NewGuid().ToString(), Name = "fc1" };
            User u2 = new User() { Id = Guid.NewGuid().ToString(), Name = "fc2" };
            try
            {
                this.UserService.CreateTwoUsers(u1, u2);
            }
            catch (ApplicationException)
            {
            }

            var u3 = this.RepositoryOfUser.Select()
                .Where(x => x.Id == u1.Id)
                .FirstOrNull();

            Assert.IsNull(u3);

            var u4 = this.RepositoryOfUser.Select()
                .Where(x => x.Id == u2.Id)
                .FirstOrNull();

            Assert.IsNull(u4);
        }

        [TestMethod]
        public void NestTran()
        {
            try
            {
                this.UserService.Create(new User()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = Constant.ROLE_DEFAULT
                });
                Assert.Fail("创建两个用户时应当抛出异常");
            }
            catch (ApplicationException)
            {
            }
        }
    }
}
