using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace ReleaseTrackers
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        private static IWindsorContainer _Container;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }


        protected void Application_BeginRequest()
        {
            BootstrapContainer();
        }

        private static void BootstrapContainer()
        {
            _Container = new WindsorContainer()
               .Install(new ReleaseTrackerInstaller());

            var ControllerFactory = new WindsorControllerFactory(_Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(ControllerFactory);

            GlobalConfiguration.Configuration.Services.Replace(
                typeof(IHttpControllerActivator), new WindsorCompositionRoot(_Container));
            
        }
    }
}
