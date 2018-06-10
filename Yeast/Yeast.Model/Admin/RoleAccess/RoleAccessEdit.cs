using System.Collections.Generic;
using Yeast.Utilities.DynamicRoleBase;

namespace Yeast.Model.Admin
{
	public class RoleAccessEdit
    {
		public string Title { get; set; }
        public IEnumerable<ControllerDescription> SelectedControllers { get; set; }
        public IEnumerable<ControllerDescription> Controllers { get; set; }
		public string Description { get; set; }
	}
}
