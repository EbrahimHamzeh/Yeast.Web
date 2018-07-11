using System.Collections.Generic;
using Yeast.Model.Admin;

namespace Yeast.Model.FrontEnd
{
    public class HomeViewModel
    {
        public IEnumerable<Yeast.Model.FrontEnd.Product> ProductList { get; set; }
        public IEnumerable<PostModel> postlist { get; set; }
        public HomeModel homeModel { get; set; }

    }
}
