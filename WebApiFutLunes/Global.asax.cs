using System.Web.Http;
using System.Web.Mvc;
using Ninject;
using Ninject.Web.Common.WebHost;
using WebApiFutLunes.Data.Repositories;
using WebApiFutLunes.Data.Repositories.Interface;
using WebApiFutLunes.Helpers.AutoMapper;

namespace WebApiFutLunes
{
    public class WebApiApplication : NinjectHttpApplication
    {
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            return kernel;
        }

        private void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMatchesRepository>().To<MatchesRepository>();
            kernel.Bind<IPlayersRepository>().To<PlayersRepository>();
        }

        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AutoMapperConfiguration.Configure();
            base.OnApplicationStarted();
        }
    }
}
