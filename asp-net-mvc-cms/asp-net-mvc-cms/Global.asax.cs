using asp_net_mvc_cms.App_Start;
using asp_net_mvc_cms.Models;
using asp_net_mvc_cms.Models.ModelBinders;
using System.Web.Mvc;
using System.Web.Routing;

namespace asp_net_mvc_cms
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AuthDbConfig.RegisterAdmin();
            ModelBinders.Binders.Add(typeof(Post), new PostModelBinder());
        }
    }
}
