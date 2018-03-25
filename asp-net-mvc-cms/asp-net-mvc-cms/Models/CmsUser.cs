using Microsoft.AspNet.Identity.EntityFramework;

namespace asp_net_mvc_cms.Models
{
    public class CmsUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}