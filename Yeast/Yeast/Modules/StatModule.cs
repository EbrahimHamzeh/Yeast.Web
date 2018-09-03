using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;
using System.Web;
using System.Web.SessionState;
using UAParser;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Utilities;
using Yeast.Utilities.IPLocate;


namespace Yeast.Web.Modules
{
    public class StatModule : IHttpModule
    {
        public StatModule() { }

        public void Dispose() { }

        public void Init(HttpApplication context)
        {
            IHttpModule module = context.Modules["Session"];
            if (module.GetType() == typeof(SessionStateModule))
            {
                SessionStateModule stateModule = (SessionStateModule)module;
                stateModule.Start += (Session_Start);
            }
            context.BeginRequest += new EventHandler(Application_BeginRequest);
        }

        private void Application_BeginRequest(object sender, EventArgs e)
        {
            //var persianCulture = new PersianCulture();
            //Thread.CurrentThread.CurrentCulture = persianCulture;
            //Thread.CurrentThread.CurrentUICulture = persianCulture;
        }

        protected void Session_Start(object sender, EventArgs e)
        {
			//HostingEnvironment.QueueBackgroundWorkItem(canccellationToken =>
		 //  {
			//   System.Web.HttpContext context = System.Web.HttpContext.Current;

			//   var statistic = new Statistics();
			//   statistic.IpAddress = GetIPAddress();
			//   statistic.UserOs = GetUserOS(context.Request.UserAgent);
			//   statistic.PageViewed = context.Request.Url.AbsolutePath;
			//   statistic.Referer = context.Request.UrlReferrer?.ToString() ?? "Direct";
			//   statistic.UserAgent = context.Request.Browser.Browser;
			//   statistic.DateStamp = DateTime.Now;

			//   using (var db = new YeastDbContext())
			//   {
			//	   db.Statisticses.Add(statistic);
			//	   db.SaveChanges();
			//   }

			//   var countryData = IPLocateWrapper.HttpClient(GetIPAddress());

			//   using (var db = new YeastDbContext())
			//   {
			//	   if (db.Countries.Any(c => c.CountryCode.Equals(countryData.countryCode)))
			//	   {
			//			//then Update the ViewCount
			//			Country currentCountry =
			//			   db.Countries.First(cc => cc.CountryCode.Equals(countryData.countryCode));
			//		   currentCountry.ViewCount++;
			//		   db.SaveAllChanges();
			//	   }
			//	   else
			//	   {
			//			//then add this Country To Database
			//			var newCountry = new Country()
			//		   {
			//			   CountryCode = countryData.countryCode,
			//			   CountryName = countryData.countryName,
			//			   ViewCount = 1
			//		   };

			//		   db.Countries.Add(newCountry);
			//		   db.SaveAllChanges();
			//	   }
			//   }
		 //  });
        }

        private static readonly List<string> KnownCrawlers = new List<string>
        {
            "bot","crawler","baiduspider","80legs","ia_archiver","ahrefsBot","twitterbot",
            "yoozbot","yandexBot","bitlybot","other", "sogou web spider", "python requests",
            "voyager","curl","wget","yahoo! slurp","mediapartners-google", "mj12bot",
            "seznamBot", "Sogou web spider", "360Spider", "sogouwebspider"
        };

        public static bool IsBotOrCrawler(string agent)
        {
            agent = agent.ToLower();
            return KnownCrawlers.Any(crawler => agent.Contains(crawler) || agent.Equals(crawler));
        }

        public static string GetUserOS(string userAgent)
        {
            // get a parser with the embedded regex patterns
            var uaParser = UAParser.Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);
            return c.OS.Family;
        }

        public string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }
    }
}
