using System;
using System.Threading;
using Yeast.Servicelayer.Interfaces;
using StructureMap;
using Yeast.Servicelayer.EFServices;
using Yeast.Datalayer.Context;
using StructureMap.Web;
using System.Web;

namespace Yeast.IocConfig
{
	public static class SmObjectFactory
	{
		private static readonly Lazy<Container> _containerBuilder =
					 new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

		public static IContainer Container
		{
			get { return _containerBuilder.Value; }
		}

		private static Container defaultContainer()
		{
			return new Container(x =>
			{
				x.Scan(scan =>
				{
					scan.TheCallingAssembly();
					scan.WithDefaultConventions();
				});

				//var dynamicProxy = new ProxyGenerator();     TODO: Hamzeh-CacheInterceptor

				x.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
        x.For<IUnitOfWork>().HttpContextScoped().Use<YeastDbContext>();
				x.For<ICategoryService>().Use<CategoryService>();
				//dynamicProxy.CreateInterfaceProxyWithTarget(myTypeInterface, new CacheInterceptor())).Use<UserService>();    TODO: Hamzeh-CacheInterceptor
			});
		}
	}
}