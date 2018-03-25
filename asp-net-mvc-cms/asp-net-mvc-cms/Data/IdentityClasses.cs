using asp_net_mvc_cms.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace asp_net_mvc_cms.Data
{
    public class CmsUserStore : UserStore<CmsUser>
    {
        public CmsUserStore() : this(new CmsContext()) { }

        public CmsUserStore(CmsContext context) : base(context) { }
    }

    public class CmsUserManager : UserManager<CmsUser>
    {
        public CmsUserManager() : this(new CmsUserStore()) { }

        public CmsUserManager(UserStore<CmsUser> userStore) : base(userStore) { }
    }
}