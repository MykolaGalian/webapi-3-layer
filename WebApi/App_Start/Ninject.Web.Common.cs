using System.Web.Http;
using WebApiContrib.IoC.Ninject;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace WebApi.App_Start
{
    using System;
    using System.Web;
   
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using BLL.Infostructure;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;  // add from NuGet
    using Ninject.Modules;
    using BLL.Contracts;
    using BLL.Services;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static IKernel Kernel { get; private set; }

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var modules = new INinjectModule[] { new DIResolver("ProdConection") };
            // передаем ядру модули конфигурации (modules), реализующие интерфейс INinjectModule

            Kernel = new StandardKernel(modules);
            try
            {
                Kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                Kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();


                //web api set
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(Kernel); // Resolver DI -  WebApiContrib.IoC.Ninject

                RegisterServices(Kernel);
                return Kernel;
            }
            catch
            {
                Kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDTOUnitOfWork>().To<DTOUnitOfWork>();
        }        
    }
}