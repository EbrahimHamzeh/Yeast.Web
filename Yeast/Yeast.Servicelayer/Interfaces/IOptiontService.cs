using System.Threading.Tasks;
using Yeast.Model.Admin;
using Yeast.Model.Admin.Setting;
using Yeast.Model.FrontEnd;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IOptionService
    {
		void Update(object settings);
        SettingViewModel GetAllSetting();
        AboutUs GetAllAboutUs();
        AboutUsViewModel GetByCulterAboutUsV();
        SliderViewModel GetAllSliderImage();
        SettingViewModelCultur GetAllSettingByCultur();
        HomeModel GetHome();
		MetaTagModel GetByCulterMetaTag();

	}
}
