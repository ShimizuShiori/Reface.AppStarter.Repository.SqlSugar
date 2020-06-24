using Reface.AppStarter.AppModules;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.AppModules
{
    [EntityScanAppModule]
    [SqlSugarRepositoryAppModule]
    [ComponentScanAppModule]
    public class TestAppModule : AppModule
    {
    }
}
