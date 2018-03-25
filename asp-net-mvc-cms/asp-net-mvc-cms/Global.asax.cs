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
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            // Previous this is first but because we used Attribute Routing it change
            AreaRegistration.RegisterAllAreas();

            AuthDbConfig.RegisterAdmin();
            ModelBinders.Binders.Add(typeof(Post), new PostModelBinder());
        }
    }
}
