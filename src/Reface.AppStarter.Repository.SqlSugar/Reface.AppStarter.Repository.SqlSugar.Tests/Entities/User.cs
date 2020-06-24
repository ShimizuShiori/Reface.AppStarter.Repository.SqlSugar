using Reface.AppStarter.Attributes;
using SqlSugar;
using System;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.Entities
{
    [Entity]
    [SugarTable("User")]
    public class User
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Id { get; set; }

        public string Name { get; set; }

        public User()
        {

        }

        public User(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public User(string name) : this(Guid.NewGuid().ToString(), name)
        {

        }
    }
}
