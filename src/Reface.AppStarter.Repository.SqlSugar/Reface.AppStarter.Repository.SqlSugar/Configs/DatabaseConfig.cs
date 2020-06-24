using Reface.AppStarter.Attributes;
using SqlSugar;
using System.ComponentModel;

namespace Reface.AppStarter.Repository.SqlSugar.Configs
{
    /// <summary>
    /// 数据库配置类
    /// </summary>
    [Config("Database")]
    [Description("数据库配置")]
    public class DatabaseConfig
    {
        [Description("数据库类型")]
        public DbType DbType { get; set; } = DbType.SqlServer;

        [Description("数据库连接字符串")]
        public string ConnectionString { get; set; } = "";

        [Description("是否自动同步数据库中的表结构")]
        public bool SyncTableSchema { get; set; } = false;

    }
}
