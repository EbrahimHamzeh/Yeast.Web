using System.Threading.Tasks;
using Yeast.Model.Admin;
using Yeast.Model.FrontEnd;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IOptionService
    {
		void Update(object settings);
        SettingViewModel GetAllSetting();
        AboutUs GetAllAboutUs();
        AboutUsViewModel GetByCulter();

    }
}
