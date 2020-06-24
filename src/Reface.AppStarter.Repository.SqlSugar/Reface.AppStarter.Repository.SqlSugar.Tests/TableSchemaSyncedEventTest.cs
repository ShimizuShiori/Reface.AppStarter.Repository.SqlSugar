using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reface.AppStarter.Repository.SqlSugar.Tests.AppModules;
using Reface.AppStarter.UnitTests;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;

namespace Reface.AppStarter.Repository.SqlSugar.Tests
{
    [TestClass]
    public class TableSchemaSyncedEventTest : TestClassBase<TestAppModule>
    {


        [TestMethod]
        public void CheckEntityInfos()
        {
            IEnumerable<EntityInfo> infos = this.App.Context[Constant.APP_CONTEXT_KEY_ENTITY_INFOS] as IEnumerable<EntityInfo>;
            Assert.IsNotNull(infos);
            Assert.AreEqual(2, infos.Count());
        }
    }
}
