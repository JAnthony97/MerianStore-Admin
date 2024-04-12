using CJ.MerianPartyStore.BL.BC;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.PL.UI.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CJ.MerianPartyStore.PL.UI.Admin.Controllers
{
    public class TipoController : Controller
    {
        // GET: TipoInvitacion
        public ActionResult InvitacionVideo(int IdInvitacion, string Cliente)
        {
            try
            {
                InvitacionBC objInvitacionBC = new InvitacionBC();
                Invitacion objInvitacion = objInvitacionBC.ObtenerInvitacionCliente(IdInvitacion, Cliente);
                return View(InvitacionModel.FromInvitacionVideo(objInvitacion));
            }
            catch (Exception ex)
            {

                ViewBag.Message = "Ocurrió un error al encontrar la invitación.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }

        }

        public ActionResult InvitacionPDF(int IdInvitacion, string Cliente)
        {
            try
            {
                InvitacionBC objInvitacionBC = new InvitacionBC();
                Invitacion objInvitacion = objInvitacionBC.ObtenerInvitacionCliente(IdInvitacion, Cliente);
                return View(InvitacionModel.FromInvitacionPDF(objInvitacion));
            }
            catch (Exception ex)
            {

                ViewBag.Message = "Ocurrió un error al encontrar la invitación.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }

        public ActionResult InvitacionAnimada(/*int IdInvitacion, string Cliente*/)
        {
            try
            {
                //InvitacionBC objInvitacionBC = new InvitacionBC();
                //Invitacion objInvitacion = objInvitacionBC.ObtenerInvitacionCliente(IdInvitacion, Cliente);
                return View("InvitacionAnimada");
            }
            catch (Exception ex)
            {

                ViewBag.Message = "Ocurrió un error al encontrar la invitación.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }
    }
}