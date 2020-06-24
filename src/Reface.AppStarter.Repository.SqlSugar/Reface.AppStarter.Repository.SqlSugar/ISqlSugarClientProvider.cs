using SqlSugar;

namespace Reface.AppStarter.Repository.SqlSugar
{
    public interface ISqlSugarClientProvider
    {
        SqlSugarClient Provide();
    }
}
