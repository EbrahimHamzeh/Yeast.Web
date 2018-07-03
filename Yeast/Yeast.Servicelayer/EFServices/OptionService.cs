using System.Data.Entity;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;
using Yeast.Datalayer.Context;
using Yeast.DomainClasses.Entities;
using Yeast.Model.Admin;
using Yeast.Model.Admin.Setting;
using Yeast.Model.FrontEnd;
using Yeast.Servicelayer.Interfaces;
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
            var propertis = settings.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in propertis)
            {
                if (_option.Where(x => x.Name == prop.Name).Any())
                    _option.Where(x => x.Name == prop.Name).FirstOrDefault().Value = prop.GetValue(settings) != null ? prop.GetValue(settings).ToString() : "";
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
        public SettingViewModelCultur GetAllSettingByCultur()
        {
            string cultur = CultureHelper.GetCurrentNeutralCulture();
            AboutUsViewModel aboutUsViewModel = new AboutUsViewModel();
            var option = _option.ToList();
            SettingViewModelCultur settingViewModel = new SettingViewModelCultur();
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

            switch (cultur)
            {
                case "fa":
                    settingViewModel.WebSiteName = option.Where(x => x.Name == "WebSiteNameFa").FirstOrDefault().Value;
                    break;
                case "en":
                    settingViewModel.WebSiteName = option.Where(x => x.Name == "WebSiteNameEn").FirstOrDefault().Value;
                    break;
                case "ar":
                    settingViewModel.WebSiteName = option.Where(x => x.Name == "WebSiteNameSa").FirstOrDefault().Value;
                    break;
                case "ru":
                    settingViewModel.WebSiteName = option.Where(x => x.Name == "WebSiteNameRu").FirstOrDefault().Value;
                    break;
            }
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

        public AboutUsViewModel GetByCulterAboutUsV()
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

        public SliderViewModel GetAllSliderImage()
        {
            var option = _option.ToList();

            SliderViewModel sliderViewModel = new SliderViewModel();

            if (option.Where(x => x.Name == "Image1").Any())
                sliderViewModel.Image1 = option.Where(x => x.Name == "Image1").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "Image2").Any())
                sliderViewModel.Image2 = option.Where(x => x.Name == "Image2").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "Image3").Any())
                sliderViewModel.Image3 = option.Where(x => x.Name == "Image3").FirstOrDefault().Value;

            if (option.Where(x => x.Name == "TextImag1").Any())
                sliderViewModel.TextImag1 = option.Where(x => x.Name == "TextImag1").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "TextImag2").Any())
                sliderViewModel.TextImag2 = option.Where(x => x.Name == "TextImag2").FirstOrDefault().Value;
            if (option.Where(x => x.Name == "TextImag3").Any())
                sliderViewModel.TextImag3 = option.Where(x => x.Name == "TextImag3").FirstOrDefault().Value;

            sliderViewModel.BodyEn = option.Where(x => x.Name == "BodyEn").FirstOrDefault().Value; ;
            sliderViewModel.BodyEn1 = option.Where(x => x.Name == "BodyEn1").FirstOrDefault().Value; 
            sliderViewModel.BodyFa = option.Where(x => x.Name == "BodyFa").FirstOrDefault().Value; ;
            sliderViewModel.BodyFa1 = option.Where(x => x.Name == "BodyFa1").FirstOrDefault().Value; 
            sliderViewModel.BodyRu = option.Where(x => x.Name == "BodyRu").FirstOrDefault().Value; ;
            sliderViewModel.BodyRu1 = option.Where(x => x.Name == "BodyRu1").FirstOrDefault().Value; 
            sliderViewModel.BodySa = option.Where(x => x.Name == "BodySa").FirstOrDefault().Value; ;
            sliderViewModel.BodySa1 = option.Where(x => x.Name == "BodySa1").FirstOrDefault().Value; 

            sliderViewModel.TitleEn = option.Where(x => x.Name == "TitleEn").FirstOrDefault().Value; ;
            sliderViewModel.TitleEn1 = option.Where(x => x.Name == "TitleEn1").FirstOrDefault().Value; 
            sliderViewModel.TitleFa = option.Where(x => x.Name == "TitleFa").FirstOrDefault().Value; ;
            sliderViewModel.TitleFa1 = option.Where(x => x.Name == "TitleFa1").FirstOrDefault().Value; 
            sliderViewModel.TitleRu = option.Where(x => x.Name == "TitleRu").FirstOrDefault().Value; ;
            sliderViewModel.TitleRu1 = option.Where(x => x.Name == "TitleRu1").FirstOrDefault().Value; 
            sliderViewModel.TitleSa = option.Where(x => x.Name == "TitleSa").FirstOrDefault().Value; ;
            sliderViewModel.TitleSa1 = option.Where(x => x.Name == "TitleSa1").FirstOrDefault().Value; 

            return sliderViewModel;
        }


        public HomeModel GetHome()
        {
            var option = _option.ToList();

            HomeModel home = new HomeModel();
            string cultur = CultureHelper.GetCurrentNeutralCulture();

            switch (cultur)
            {
                case "fa":
                    if (option.Where(x => x.Name == "Image1").Any())
                        home.Image1 = option.Where(x => x.Name == "Image1").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "Image2").Any())
                        home.Image2 = option.Where(x => x.Name == "Image2").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "Image3").Any())
                        home.Image3 = option.Where(x => x.Name == "Image3").FirstOrDefault().Value;

                    if (option.Where(x => x.Name == "TextImag1").Any())
                        home.TextImag1 = option.Where(x => x.Name == "TextImag1").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "TextImag2").Any())
                        home.TextImag2 = option.Where(x => x.Name == "TextImag2").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "TextImag3").Any())
                        home.TextImag3 = option.Where(x => x.Name == "TextImag3").FirstOrDefault().Value;

                    home.Bodypart1 = _option.Where(x => x.Name == "BodyFa").FirstOrDefault().Value;
                    home.Bodypart2 = _option.Where(x => x.Name == "BodyFa1").FirstOrDefault().Value;
                    home.Titlepart1 = _option.Where(x => x.Name == "TitleFa").FirstOrDefault().Value;
                    home.Titlepart2 = _option.Where(x => x.Name == "TitleFa1").FirstOrDefault().Value;
                    break;
                case "en":
                    if (option.Where(x => x.Name == "Image1").Any())
                        home.Image1 = option.Where(x => x.Name == "Image1").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "Image2").Any())
                        home.Image2 = option.Where(x => x.Name == "Image2").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "Image3").Any())
                        home.Image3 = option.Where(x => x.Name == "Image3").FirstOrDefault().Value;

                    if (option.Where(x => x.Name == "TextImag1").Any())
                        home.TextImag1 = option.Where(x => x.Name == "TextImag1").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "TextImag2").Any())
                        home.TextImag2 = option.Where(x => x.Name == "TextImag2").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "TextImag3").Any())
                        home.TextImag3 = option.Where(x => x.Name == "TextImag3").FirstOrDefault().Value;

                    home.Bodypart1 = _option.Where(x => x.Name == "BodyEn").FirstOrDefault().Value;
                    home.Bodypart2 = _option.Where(x => x.Name == "BodyEn1").FirstOrDefault().Value;
                    home.Titlepart1 = _option.Where(x => x.Name == "TitleEn").FirstOrDefault().Value;
                    home.Titlepart2 = _option.Where(x => x.Name == "TitleEn1").FirstOrDefault().Value;
                    break;
                case "ar":
                    if (option.Where(x => x.Name == "Image1").Any())
                        home.Image1 = option.Where(x => x.Name == "Image1").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "Image2").Any())
                        home.Image2 = option.Where(x => x.Name == "Image2").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "Image3").Any())
                        home.Image3 = option.Where(x => x.Name == "Image3").FirstOrDefault().Value;

                    if (option.Where(x => x.Name == "TextImag1").Any())
                        home.TextImag1 = option.Where(x => x.Name == "TextImag1").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "TextImag2").Any())
                        home.TextImag2 = option.Where(x => x.Name == "TextImag2").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "TextImag3").Any())
                        home.TextImag3 = option.Where(x => x.Name == "TextImag3").FirstOrDefault().Value;

                    home.Bodypart1 = _option.Where(x => x.Name == "BodySa").FirstOrDefault().Value;
                    home.Bodypart2 = _option.Where(x => x.Name == "BodySa1").FirstOrDefault().Value;
                    home.Titlepart1 = _option.Where(x => x.Name == "TitleSa").FirstOrDefault().Value;
                    home.Titlepart2 = _option.Where(x => x.Name == "TitleSa1").FirstOrDefault().Value;
                    break;
                case "ru":
                    if (option.Where(x => x.Name == "Image1").Any())
                        home.Image1 = option.Where(x => x.Name == "Image1").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "Image2").Any())
                        home.Image2 = option.Where(x => x.Name == "Image2").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "Image3").Any())
                        home.Image3 = option.Where(x => x.Name == "Image3").FirstOrDefault().Value;

                    if (option.Where(x => x.Name == "TextImag1").Any())
                        home.TextImag1 = option.Where(x => x.Name == "TextImag1").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "TextImag2").Any())
                        home.TextImag2 = option.Where(x => x.Name == "TextImag2").FirstOrDefault().Value;
                    if (option.Where(x => x.Name == "TextImag3").Any())
                        home.TextImag3 = option.Where(x => x.Name == "TextImag3").FirstOrDefault().Value;

                    home.Bodypart1 = _option.Where(x => x.Name == "BodyRu").FirstOrDefault().Value;
                    home.Bodypart2 = _option.Where(x => x.Name == "BodyRu1").FirstOrDefault().Value;
                    home.Titlepart1 = _option.Where(x => x.Name == "TitleRu").FirstOrDefault().Value;
                    home.Titlepart2 = _option.Where(x => x.Name == "TitleRu1").FirstOrDefault().Value;
                    break;
                default:
                    break;
            }

            return home;
        }
    }
}
