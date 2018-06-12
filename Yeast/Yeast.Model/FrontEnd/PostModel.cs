using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Yeast.Model.FrontEnd
{
    public class PostModel
    {
        public int Id { get; set; }
        public string CreatedDate { get; set; }
        public string TitleImg { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public List<int> TagIds { get; set; }
        public List<int> CategoryIds { get; set; }
        public MultiSelectList TagList { get; set; }
        public MultiSelectList CategoryList { get; set; }
    }
}
