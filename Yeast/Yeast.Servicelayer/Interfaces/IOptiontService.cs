using System.Threading.Tasks;
using Yeast.Model.Admin;

namespace Yeast.Servicelayer.Interfaces
{
	public interface IOptionService
    {
		void Update(object settings);
        SettingViewModel GetAllSetting();
        AboutusViewModel GetAllAboutUs();
	}
}
