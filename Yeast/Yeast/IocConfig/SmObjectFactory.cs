﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using StructureMap;
using System.Data.Entity;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.EFServices;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.IocConfig
{
	public static class SmObjectFactory
	{
		private static readonly Lazy<Container> _containerBuilder = new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

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

				x.For<IIdentity>().Use(() => getIdentity());
				x.For<HttpContextBase>().Use(() => new HttpContextWrapper(HttpContext.Current));
                x.For<IUnitOfWork>().Use<YeastDbContext>();
				x.For<YeastDbContext>().Use(context => (YeastDbContext)context.GetInstance<IUnitOfWork>());
				x.For<DbContext>().Use(context => (YeastDbContext)context.GetInstance<IUnitOfWork>());
				x.For<IUserStore<User, int>>().Use<CustomUserStore>();
				x.For<IRoleStore<CustomRole, int>>().Use<CustomRoleStore>();
				x.For<IAuthenticationManager>().Use(() => HttpContext.Current.GetOwinContext().Authentication);
				x.For<IApplicationSignInManager>().Use<ApplicationSignInManager>();
				x.For<IApplicationRoleManager>().Use<ApplicationRoleManager>();
				// map same interface to different concrete classes
				x.For<IIdentityMessageService>().Use<SmsService>();
				x.For<IIdentityMessageService>().Use<EmailService>();
				x.For<IApplicationUserManager>()
					 .Use<ApplicationUserManager>()
					 .Ctor<IIdentityMessageService>("smsService").Is<SmsService>()
					 .Ctor<IIdentityMessageService>("emailService").Is<EmailService>()
					 .Setter(userManager => userManager.SmsService).Is<SmsService>()
					 .Setter(userManager => userManager.EmailService).Is<EmailService>();
				x.For<ApplicationUserManager>().Use(context => (ApplicationUserManager)context.GetInstance<IApplicationUserManager>());
				x.For<ICustomRoleStore>().Use<CustomRoleStore>();
				x.For<ICustomUserStore>().Use<CustomUserStore>();

				x.For<ICategoryService>().Use<CategoryService>();
				x.For<ITagService>().Use<TagService>();
				x.For<IProductService>().Use<ProductService>();
				x.For<IPostService>().Use<PostService>();
				x.For<ICategoryService>().Use<CategoryService>();
				x.For<IOptionService>().Use<OptionService>();
				x.For<IContactUsService>().Use<ContactUsService>();
				x.For<IServiceCategoryService>().Use<ServiceCategoryService>();
				x.For<IServiceService>().Use<ServiceService>();
				x.For<IEmailTemplateService>().Use<EmailTemplateService>();
				x.For<IEmailService>().Use<EmailsService>();
				x.For<IStatisticsService>().Use<StatisticsService>();
				x.For<IRepresentationService>().Use<RepresentationService>();
				x.For<ICustomerService>().Use<CustomerService>();

				x.Policies.SetAllProperties(y =>
				{
					y.OfType<IApplicationRoleManager>();
					y.OfType<IApplicationUserManager>();
					y.OfType<IUnitOfWork>();
				});
			});
		}
		
		private static IIdentity getIdentity()
		{
			if (HttpContext.Current != null && HttpContext.Current.User != null)
			{
				return HttpContext.Current.User.Identity;
			}

			return ClaimsPrincipal.Current != null ? ClaimsPrincipal.Current.Identity : null;
		}
	}
}