using Reface.AppStarter.Attributes;
using SqlSugar;

namespace Reface.AppStarter.Repository.SqlSugar
{
    [Component]
    public class SqlSugarDatabaseTransactionController : IDatabaseTransactionController
    {
        private readonly SqlSugarClient client;

        public SqlSugarDatabaseTransactionController(ISqlSugarClientProvider provider)
        {
            this.client = provider.Provide();
        }

        public void Begin()
        {
            this.client.BeginTran();
        }

        public void Commit()
        {
            this.client.CommitTran();
        }

        public void Rollback()
        {
            this.client.RollbackTran();
        }
    }
}
