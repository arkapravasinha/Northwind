[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NorthwindSolution.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NorthwindSolution.App_Start.NinjectWebCommon), "Stop")]

namespace NorthwindSolution.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using AutoMapper;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using NorthwindSolution.Repository;
    using NorthwindSolution.Repository.Interfaces;
    using NorthwindSolution.Repository.MappingProfile;
    using WebApiContrib.IoC.Ninject;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

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
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                //add to register Ninject  as dependency resolver by arka
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ICustomerRepository>().To<CustomerRepository>();

            //automapper added by me
            kernel.Bind<IMapper>()
                    .ToMethod(context =>
                    {
                        var config = new MapperConfiguration(cfg =>
                        {
                            cfg.AddProfile<CustomerMappingProfile>();
                            // tell automapper to use ninject when creating value converters and resolvers
                            cfg.ConstructServicesUsing(t => kernel.Get(t));
                        });
                        return config.CreateMapper();
                    }).InSingletonScope();

        }        
    }
}
