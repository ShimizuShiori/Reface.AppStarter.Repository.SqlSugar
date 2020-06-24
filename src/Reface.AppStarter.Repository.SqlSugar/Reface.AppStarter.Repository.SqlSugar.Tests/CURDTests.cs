using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Repository.SqlSugar.Tests.AppModules;
using Reface.AppStarter.Repository.SqlSugar.Tests.Entities;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.Repository.SqlSugar.Tests
{
    [TestClass]
    public class CURDTests
    {
        [TestInitialize]
        public void Init()
        {
            var app = new AppSetup().Start(new TestAppModule());
            using (var work = app.BeginWork("Test"))
            {
                work.InjectProperties(this);
                this.Users.Delete(x => true);
            }
        }

        public IRepository<User> Users { get; set; }

        [TestMethod]
        public void CheckUserCountIsZero()
        {
            var count = Users.Select().Where(e => true).Count();
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void CreateUser()
        {
            User user = new User() { Id = Guid.NewGuid().ToString(), Name = "fc" };
            this.Users.Insert(user);

            User user2 = this.Users.Select()
                .Where(x => x.Id == user.Id)
                .FirstOrNull();

            Assert.AreEqual(user.Id, user2.Id);
            Assert.AreEqual("fc", user2.Name);
        }

        [TestMethod]
        public void CreateAndUpdate()
        {
            User user = new User() { Id = Guid.NewGuid().ToString(), Name = "fc" };
            this.Users.Insert(user);

            User user3 = this.Users.Select()
                .Where(x => x.Id == user.Id)
                .FirstOrNull();

            Assert.AreEqual("fc", user3.Name);

            this.Users.Update(it => new User() { Name = SqlFunc.MergeString(it.Name, "c") }).Where(x => x.Id == user.Id);

            User user2 = this.Users.Select()
                .Where(x => x.Id == user.Id)
                .FirstOrNull();

            Assert.AreEqual(user.Id, user2.Id);
            Assert.AreEqual("fcc", user2.Name);
        }

        [TestMethod]
        public void TestDeleteByEntity()
        {
            User u1 = new User(Guid.NewGuid().ToString(), "fc");
            Users.Insert(u1);

            Users.Delete(u1);

            User u2 = Users.Select()
                .Where(x => x.Id == u1.Id)
                .FirstOrNull();

            Assert.IsNull(u2);
        }

        [TestMethod]
        public void TestDeleteByCondition()
        {
            List<User> users = new List<User>();
            users.Add(new User("fc"));
            users.ForEach(x => Users.Insert(x));

            Users.Delete(x => x.Name == "fc");

            users.ForEach(x =>
            {
                var u = Users.Select()
                    .Where(y => y.Id == x.Id)
                    .FirstOrNull();
                Assert.IsNull(u, $"用户 {x.Id} 应当不存在");
            });
        }

        [TestMethod]
        public void TestSelectWhere()
        {
            MakeDatas(i =>
            {
                if (i >= 10) return null;
                return new User($"User_{i}");
            });
            var us = Users.Select()
                .Where(x => true)
                .ToList();

            Assert.AreEqual(10, us.Count());
        }

        [TestMethod]
        public void TestSelectWhereLike()
        {
            MakeDatas(i =>
            {
                if (i >= 10) return null;
                return new User($"User_{i}");
            });
            var us = Users.Select()
                .Where(x => x.Name.Contains("User_"))
                .ToList();

            Assert.AreEqual(10, us.Count());
        }

        [TestMethod]
        public void TestPageSelect()
        {
            MakeDatas(i => i >= 100 ? null : new User($"User_{i}"));
            int count;
            var us = Users.Select()
                .Where(x => true)
                .ToPageList(0, 10, out count);

            Assert.AreEqual(100, count);
            Assert.AreEqual(10, us.Count());
            Assert.AreEqual("User_0", us.First().Name);
        }

        [TestMethod]
        public void TestPageSelectOrderBy()
        {
            MakeDatas(i => i >= 100 ? null : new User($"User_{i}"));
            int count;
            var us = Users.Select()
                .Where(x => true)
                .OrderByDesc(x => x.Name)
                .ToPageList(0, 10, out count);

            Assert.AreEqual(100, count);
            Assert.AreEqual(10, us.Count());
            Assert.AreEqual("User_99", us.First().Name);
        }

        private void MakeDatas(Func<int, User> creator)
        {
            int i = 0;
            while (true)
            {
                User u = creator(i++);
                if (u == null) break;
                Users.Insert(u);
            }
        }
    }
}
