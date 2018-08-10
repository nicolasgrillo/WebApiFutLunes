using System.Web.Http;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common.WebHost;
using WebApiFutLunes.Helpers.AutoMapper;

namespace WebApiFutLunes
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AutoMapperConfiguration.Configure();
        }

        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        private void RegisterServices(IKernel kernel)
        {
            // e.g. kernel.Load(Assembly.GetExecutingAssembly());
        }
    }
}
