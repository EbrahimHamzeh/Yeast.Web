using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Servicelayer.Interfaces;
using Yeast.Model.Admin;
using System.Reflection;
using Yeast.Model.FrontEnd;
using Yeast.Utilities.Helpers;

namespace Yeast.Servicelayer.EFServices
{
    public class OptionService : IOptionService
    {
        private readonly IDbSet<Option> _option;

        public OptionService(IUnitOfWork uow)
        {
            _option = uow.Set<Option>();
        }

        public void Update(object settings)
        {
            var propertis = settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance); ;

            foreach (var prop in propertis)
            {
                if (_option.Where(x => x.Value == prop.Name).Any())
                    _option.Where(x => x.Value == prop.Name).FirstOrDefault().Value = prop.GetValue(settings).ToString();
                else
                    _option.Add(new Option { Name = prop.Name, Value = prop.GetValue(settings) != null ? prop.GetValue(settings).ToString() : "" });
            }
        }

        public SettingViewModel GetAllSetting()
        {
            var option = _option.ToList();

            SettingViewModel settingViewModel = new SettingViewModel();
            if (option.Where(x => x.Name == "Address").Any())
                settingViewModel.Address = option.Where(x => x.Name == "Address").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "Email").Any())
                settingViewModel.Email = option.Where(x => x.Name == "Email").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "Facebooke").Any())
                settingViewModel.Facebooke = option.Where(x => x.Name == "Facebooke").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "Instagram").Any())
                settingViewModel.Instagram = option.Where(x => x.Name == "Instagram").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "MetaTag").Any())
                settingViewModel.MetaTag = option.Where(x => x.Name == "MetaTag").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "Mobile").Any())
                settingViewModel.Mobile = option.Where(x => x.Name == "Mobile").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "Phone").Any())
                settingViewModel.Phone = option.Where(x => x.Name == "Phone").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "Twitter").Any())
                settingViewModel.Twitter = option.Where(x => x.Name == "Twitter").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "WebSiteNameEn").Any())
                settingViewModel.WebSiteNameEn = option.Where(x => x.Name == "WebSiteNameEn").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "WebSiteNameFa").Any())
                settingViewModel.WebSiteNameFa = option.Where(x => x.Name == "WebSiteNameFa").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "WebSiteNameRu").Any())
                settingViewModel.WebSiteNameRu = option.Where(x => x.Name == "WebSiteNameRu").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "WebSiteNameSa").Any())
                settingViewModel.WebSiteNameSa = option.Where(x => x.Name == "WebSiteNameSa").FirstOrDefault().Value;

            return settingViewModel;
        }

        public AboutUs GetAllAboutUs()
        {
            var option = _option.ToList();

            AboutUs aboutusViewModel = new AboutUs();
            if (option.Where(x => x.Name == "TitleFa").Any())
                aboutusViewModel.TitleFa = option.Where(x => x.Name == "TitleFa").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "BodyFa").Any())
                aboutusViewModel.BodyFa = option.Where(x => x.Name == "BodyFa").FirstOrDefault().Value;

            if (option.Where(x => x.Name == "TitleEn").Any())
                aboutusViewModel.TitleEn = option.Where(x => x.Name == "TitleEn").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "BodyEn").Any())
                aboutusViewModel.BodyEn = option.Where(x => x.Name == "BodyEn").FirstOrDefault().Value;

            if (option.Where(x => x.Name == "TitleRu").Any())
                aboutusViewModel.TitleRu = option.Where(x => x.Name == "TitleRu").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "BodyRu").Any())
                aboutusViewModel.BodyRu = option.Where(x => x.Name == "BodyRu").FirstOrDefault().Value;

            if (option.Where(x => x.Name == "TitleSa").Any())
                aboutusViewModel.TitleSa = option.Where(x => x.Name == "TitleSa").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "BodySa").Any())
                aboutusViewModel.BodySa = option.Where(x => x.Name == "BodySa").FirstOrDefault().Value;

            return aboutusViewModel;
        }

        public AboutUsViewModel GetByCulter()
        {
            string cultur = CultureHelper.GetCurrentNeutralCulture();
            AboutUsViewModel aboutUsViewModel = new AboutUsViewModel();
            switch (cultur)
            {
                case "fa":
                    aboutUsViewModel.Title = _option.Where(x => x.Name == "TitleFa").FirstOrDefault().Value;
                    aboutUsViewModel.Body = _option.Where(x => x.Name == "BodyFa").FirstOrDefault().Value;
                    break;
                case "en":
                    aboutUsViewModel.Title = _option.Where(x => x.Name == "TitleEn").FirstOrDefault().Value;
                    aboutUsViewModel.Body = _option.Where(x => x.Name == "BodyEn").FirstOrDefault().Value;
                    break;
                case "ar":
                    aboutUsViewModel.Title = _option.Where(x => x.Name == "TitleSa").FirstOrDefault().Value;
                    aboutUsViewModel.Body = _option.Where(x => x.Name == "BodySa").FirstOrDefault().Value;
                    break;
                case "ru":
                    aboutUsViewModel.Title = _option.Where(x => x.Name == "TitleRu").FirstOrDefault().Value;
                    aboutUsViewModel.Body = _option.Where(x => x.Name == "BodyRu").FirstOrDefault().Value;
                    break;
                default:
                    break;
            }

            return aboutUsViewModel;
        }
    }
}
