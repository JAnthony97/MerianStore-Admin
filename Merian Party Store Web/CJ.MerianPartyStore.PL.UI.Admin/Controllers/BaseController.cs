using CJ.MerianPartyStore.BL.BC;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.PL.UI.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CJ.MerianPartyStore.PL.UI.Admin.Controllers
{
    public class BaseController : Controller
    {
        public String ApplicationRoot
        {
            get
            {
                return string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            }
        }

        // GET: Base
        public UsuarioModel UsuarioLogueado
        {
            get
            {
                if (User.Identity.IsAuthenticated)
                {
                    Usuario objUsuario = new UsuarioBC().ObtenerUsuario(UsuarioModel.FromString(User.Identity.Name).IdUsuario);

                    if (objUsuario != null)
                        return UsuarioModel.FromUsuario(objUsuario, true);
                }

                return null;
            }
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {

            base.Initialize(requestContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (UsuarioLogueado == null)
            {
                FormsAuthentication.SignOut();
                filterContext.Result = RedirectToAction("Index", "Login");
                base.OnActionExecuting(filterContext);
            }
        }
    }
}