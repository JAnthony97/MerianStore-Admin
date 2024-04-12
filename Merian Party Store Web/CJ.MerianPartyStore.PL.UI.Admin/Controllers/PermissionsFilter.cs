using CJ.MerianPartyStore.PL.UI.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CJ.MerianPartyStore.PL.UI.Admin.Controllers
{
    public class PermissionsFilter : ActionFilterAttribute
    {
        private String[] Roles;

        public PermissionsFilter(String[] Perfiles)
        {
            this.Roles = Perfiles;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                UsuarioModel objUsuarioLogueado = UsuarioModel.FromString(filterContext.HttpContext.User.Identity.Name);
                if (!Roles.Contains(objUsuarioLogueado.Perfil))
                {
                    ViewResult objViewResult = new ViewResult
                    {
                        ViewName = "Error",
                        ViewData = filterContext.Controller.ViewData,
                        TempData = filterContext.Controller.TempData
                    };

                    objViewResult.ViewBag.Message = "No cuentas con permisos para acceder a esta sección.";
                    filterContext.Result = objViewResult;
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}