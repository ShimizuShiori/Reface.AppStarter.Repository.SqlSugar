using Reface.AppStarter.Attributes;
using Reface.AppStarter.Repository.Proxies;
using Reface.AppStarter.Repository.SqlSugar.Tests.Entities;
using System;

namespace Reface.AppStarter.Repository.SqlSugar.Tests.Services
{
    public interface IRoleService
    {
        void Create(Role role);
    }

    [Component]
    public class RoleService : IRoleService
    {
        private readonly IRepository<Role> roles;

        public RoleService(IRepository<Role> roles)
        {
            this.roles = roles;
        }

        [Transcation]
        public void Create(Role role)
        {
            if (roles.Select().Where(x => x.Code == role.Code).Count() == 1)
                throw new ApplicationException();

            roles.Insert(role);
        }
    }
}
