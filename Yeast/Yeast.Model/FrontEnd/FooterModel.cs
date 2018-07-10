using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yeast.Model.Admin.Setting;

namespace Yeast.Model.FrontEnd
{
    public class FooterModel
    {
        public SettingViewModelCultur settingViewModelCultur { get; set; }
        public AboutUsViewModel aboutUsViewModel { get; set; }
        public List<PostModel> postModeList { get; set; }
        public List<ServiceModel> serviceModelList { get; set; }
    }
}
