using asp_net_mvc_cms.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace asp_net_mvc_cms.Data
{
    public interface IUserRepository : IDisposable
    {
        Task<CmsUser> GetUserByNameAsync(string username);

        Task<IEnumerable<CmsUser>> GetAllUsersAsync();

        Task CreateAsync(CmsUser user, string password);

        Task DeleteAsync(CmsUser user);

        Task UpdateAsync(CmsUser user);
    }
}
