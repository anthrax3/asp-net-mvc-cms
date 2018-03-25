using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace asp_net_mvc_cms.Data
{
    public interface IRoleRepository : IDisposable
    {
        IdentityRole GetRoleByName(string name);

        IEnumerable<IdentityRole> GetAllRoles();

        void Create(IdentityRole role);
    }
}
