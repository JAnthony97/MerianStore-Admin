using CJ.MerianPartyStore.BL.BC;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.PL.UI.Admin.Models;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CJ.MerianPartyStore.PL.UI.Admin.Controllers
{
    [Authorize]
    public class ConfiguracionController : BaseController
    {
        // GET: Configuracion
        public ActionResult Index()
        {
            try
            {
                ConfiguracionBC objConfiguracionBC = new ConfiguracionBC();
                ConfiguracionModel objConfiguracionModel = new ConfiguracionModel();

                //Banners
                IQueryable<Configuracion> lstConfiguracionBanner = objConfiguracionBC.ListarConfiguracion(Constants.Configuracion.HOME_BANNER);
                List<BannerModel> lstBannerModel = new List<BannerModel>();
                foreach (Configuracion objConfiguracion in lstConfiguracionBanner)
                    lstBannerModel.Add(BannerModel.FromConfiguracion(objConfiguracion));

                objConfiguracionModel.Banners = lstBannerModel;
                objConfiguracionModel.TipoCambio = Double.Parse(objConfiguracionBC.ObtenerConfiguracion(Constants.Configuracion.TIPO_CAMBIO).Valor);
                return View(objConfiguracionModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al cargar la configuración de contenidos";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }
        public ActionResult BannerHome(BannerModel objBannerModel)
        {
            return PartialView(objBannerModel);
        }


        public ActionResult GuardarConfiguracion(HttpPostedFileBase[] BannerFile, int[] IdConfiguraciones, String[] Link, double TipoCambio)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                ConfiguracionBC objConfiguracionBC = new ConfiguracionBC();

                //Elimina los banners anteriores
                Directory.CreateDirectory(Server.MapPath(BannerModel.IMAGE_HOME_PATH));
                if (IdConfiguraciones == null)
                    IdConfiguraciones = new int[] { };
                IQueryable<Configuracion> lstConfiguracionEliminar = objConfiguracionBC.ListarConfiguracion(Constants.Configuracion.HOME_BANNER).Where(c => IdConfiguraciones.All(i => c.IdConfiguracion != i));
                foreach (Configuracion objConfiguracion in lstConfiguracionEliminar)
                    if (!String.IsNullOrWhiteSpace(objConfiguracion.Valor))
                        new FileInfo(Path.Combine(Server.MapPath(BannerModel.IMAGE_HOME_PATH), objConfiguracion.Valor)).Delete();
                objConfiguracionBC.EliminarConfiguraciones(lstConfiguracionEliminar.Select(c => c.IdConfiguracion).ToArray());

                //Guarda los banners
                List<Configuracion> lstConfiguracion = new List<Configuracion>();
                for (int i = 0; i < IdConfiguraciones.Length; i++)
                {
                    Configuracion objConfiguracion = new Configuracion();
                    objConfiguracion.IdConfiguracion = IdConfiguraciones[i];
                    objConfiguracion.Nombre = Constants.Configuracion.HOME_BANNER;
                    objConfiguracion.Valor2 = Link[i];

                    if (objConfiguracion.IdConfiguracion == 0)
                    {
                        HttpPostedFileBase objBannerFile = BannerFile[lstConfiguracion.Count(c => c.IdConfiguracion == 0)];

                        objConfiguracion.Valor = Guid.NewGuid().ToString() + Path.GetExtension(objBannerFile.FileName);
                        objBannerFile.SaveAs(Path.Combine(Server.MapPath(BannerModel.IMAGE_HOME_PATH), objConfiguracion.Valor));
                    }

                    lstConfiguracion.Add(objConfiguracion);
                }

                //Guarda el tipo de cambio
                Configuracion objTipoCambio = objConfiguracionBC.ObtenerConfiguracion(Constants.Configuracion.TIPO_CAMBIO);
                objTipoCambio.Valor = TipoCambio.ToString("0.00");
                lstConfiguracion.Add(objTipoCambio);



                //Guarda la configuracion
                objConfiguracionBC.GuardarConfiguraciones(lstConfiguracion);

                objResultObject.Code = 0;
                objResultObject.Message = "¡La configuración se ha guardado con éxito!";
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al guardar la configuración de la tienda. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }
    }
}