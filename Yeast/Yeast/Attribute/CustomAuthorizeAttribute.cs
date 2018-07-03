using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Yeast.Datalayer.Context;
using Yeast.Servicelayer.Interfaces;

namespace Yeast.Attribute
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        readonly IApplicationRoleManager _roleService;
        readonly IApplicationUserManager _userService;
        readonly IUnitOfWork _uow;

        private string _requestControllerName;
        private string _requestedActionName;

        public CustomAuthorizeAttribute(IUnitOfWork uow, IApplicationUserManager userService, IApplicationRoleManager roleService)
        {
            _uow = uow;
            _userService = userService;
            _roleService = roleService;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            _requestControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            _requestedActionName = filterContext.ActionDescriptor.ActionName;

            base.OnAuthorization(filterContext);
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException("httpContext");

            var user = httpContext.User;
            if (!user.Identity.IsAuthenticated)
                return false;

            //var findUser = _userService.FindByNameAsync(user.Identity.Name).Result;
            //var rol = _roleService.GetAllCustomRolesAsync().Result;
            //var dbContext = findUser.Roles.ToList();
            //var roleAccess = from ra in rol
            //                 let userId = findUser.Id
            //                 let roleIds = findUser.Roles.Where(r => r.RoleAccesses.Any(u => u.UserId == userId)).Select(r => r.Id)
            //                 where roleIds.Contains(ra.RoleId)
            //                 select ra;

            //if (roleAccess.Any(ra =>
            //        ra.Controller.Equals(_requestControllerName, StringComparison.InvariantCultureIgnoreCase) &&
            //        ra.Action.Equals(_requestedActionName, StringComparison.InvariantCultureIgnoreCase)))
            //    return true;

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAuthenticated)
            {
                filterContext.Result = new HttpStatusCodeResult(403);
                return;
            }

            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}