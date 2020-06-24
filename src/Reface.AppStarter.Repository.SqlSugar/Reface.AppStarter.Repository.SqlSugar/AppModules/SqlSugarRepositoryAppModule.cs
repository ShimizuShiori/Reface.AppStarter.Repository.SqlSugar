using Reface.AppStarter.Repository.SqlSugar.Configs;
using Reface.AppStarter.Repository.SqlSugar;
using Reface.AppStarter.Repository.SqlSugar.Listeners;

namespace Reface.AppStarter.AppModules
{
    /// <summary>
    /// 提供以 SqlSugar 实现的 Repository 给容器。主要包含以下组件：
    /// <see cref="DatabaseConfig"/> ,
    /// <see cref="SqlSugarRepository{TEntity}"/> ,
    /// <see cref="SyncTableSchema"/>。
    /// 使用 <see cref="SyncTableSchema"/> 时，你还需要为你的模块添加 <see cref="EntityScanAppModule"/>
    /// </summary>
    [ComponentScanAppModule]
    [RepositoryAppModule]
    [AutoConfigAppModule]
    public class SqlSugarRepositoryAppModule : AppModule
    {
    }
}
