using Reface.AppStarter.Attributes;
using SqlSugar;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.Entities
{
    [Entity]
    [SugarTable("Role")]
    public class Role
    {
        [SugarColumn(IsPrimaryKey = true)]
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
