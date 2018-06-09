using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yeast.Utilities.DynamicRoleBase;

namespace Yeast.Model.Admin
{
	public class RoleAccessAdd
    {
        [Required]
        [StringLength(256, ErrorMessage = "The {0} must be at least {2} characters long.")]
        public string Title { get; set; }
        public IEnumerable<ControllerDescription> SelectedControllers { get; set; }
        public IEnumerable<ControllerDescription> Controllers { get; set; }
        public string Description { get; set; }
    }
}
