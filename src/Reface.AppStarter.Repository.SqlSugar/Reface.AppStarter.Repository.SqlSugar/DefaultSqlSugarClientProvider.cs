using Reface.AppStarter.Attributes;
using Reface.AppStarter.Repository.SqlSugar.Configs;
using SqlSugar;

namespace Reface.AppStarter.Repository.SqlSugar
{
    [Component]
    public class DefaultSqlSugarClientProvider : ISqlSugarClientProvider
    {
        private readonly SqlSugarClient sqlSugarClient;

        public DefaultSqlSugarClientProvider(DatabaseConfig databaseConfig)
        {
            ConnectionConfig config = new ConnectionConfig();
            config.DbType = databaseConfig.DbType;
            config.ConnectionString = databaseConfig.ConnectionString;
            config.InitKeyType = InitKeyType.Attribute;
            this.sqlSugarClient = new SqlSugarClient(config);
        }

        public SqlSugarClient Provide()
        {
            return this.sqlSugarClient;
        }
    }
}
