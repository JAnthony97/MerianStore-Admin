using CJ.MerianPartyStore.BL.BC;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.PL.UI.Admin.Models;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CJ.MerianPartyStore.PL.UI.Admin.Controllers
{
    [Authorize]
    public class InvitacionController : BaseController
    {
        // GET: Invitacion
        public const int ERROR_INCOMPLETE_DATA = 1;
        private const int PAGE_SIZE = 20;
        public ActionResult Index(String Filtro, String Busqueda, int? Page)
        {
            try
            {
                InvitacionBC objInvitacionBC = new InvitacionBC();

                //Filtro "Todos" por default
                if (String.IsNullOrWhiteSpace(Filtro))
                    Filtro = Constants.Producto.Filtro.TODOS;

                //Página 1 por default
                if (Page == null)
                    Page = 1;
                int TotalPages = 1;

                //Lista las invitaciones
                IEnumerable<Invitacion> lstInvitacion = objInvitacionBC.ListarInvitaciones(Filtro, Busqueda);

                //Paginación
                TotalPages = (int)Math.Ceiling(lstInvitacion.Count() / (double)PAGE_SIZE);
                //lstInvitacion = lstInvitacion.OrderBy(u => u.Cliente).ThenBy(u => u.Cliente).Skip((Page.Value - 1) * PAGE_SIZE).Take(PAGE_SIZE);
                lstInvitacion = lstInvitacion.OrderByDescending(u => u.IdInvitacion).ThenByDescending(u => u.IdInvitacion).Skip((Page.Value - 1) * PAGE_SIZE).Take(PAGE_SIZE);
                List<InvitacionModel> lstInvitacionModel = new List<InvitacionModel>();
                foreach (Invitacion objInvitacion in lstInvitacion)
                    lstInvitacionModel.Add(InvitacionModel.FromInvitacion(objInvitacion, true));


                //Envía el filtro para que muestre dicho tab activo
                ViewBag.Filtro = Filtro;

                //Envía la paginación
                ViewBag.Page = Page;
                ViewBag.TotalPages = TotalPages;

                return View(lstInvitacionModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al obtener la lista de invitaciones.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }

        [PermissionsFilter(new String[] { Constants.Usuario.Perfil.ADMINISTRADOR })]
        public ActionResult EliminarInvitacion(int IdInvitacion)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                InvitacionBC objInvitacionBC = new InvitacionBC();
                Invitacion objInvitacion = objInvitacionBC.ObtenerInvitacion(IdInvitacion);

                string DirectorioDestino = Server.MapPath("~/event/Inv-" + objInvitacion.IdInvitacion + "/" +
                       objInvitacion.Cliente.Replace(" ", "").Replace("&", "y").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u") + "/");
                if (System.IO.File.Exists(Path.Combine(DirectorioDestino, "Index.html")))
                    System.IO.File.Delete(Path.Combine(DirectorioDestino, "Index.html"));

                if (Directory.Exists(DirectorioDestino))
                    Directory.Delete(DirectorioDestino);

                if (Directory.Exists(Server.MapPath("~/event/Inv-" + objInvitacion.IdInvitacion + "/")))
                    Directory.Delete(Server.MapPath("~/event/Inv-" + objInvitacion.IdInvitacion + "/"));

                if (objInvitacion != null)
                {

                    if (!String.IsNullOrWhiteSpace(objInvitacion.Audio))
                        new FileInfo(Path.Combine(Server.MapPath(InvitacionModel.AUDIO_INVITACION_PATH), objInvitacion.Audio)).Delete();

                    if (!String.IsNullOrWhiteSpace(objInvitacion.Fondo))
                        new FileInfo(Path.Combine(Server.MapPath(InvitacionModel.FONDO_INVITACION_PATH), objInvitacion.Fondo)).Delete();

                    if (!String.IsNullOrWhiteSpace(objInvitacion.VideoInvitacion))
                        new FileInfo(Path.Combine(Server.MapPath(InvitacionModel.VIDEO_INVITACION_PATH), objInvitacion.VideoInvitacion)).Delete();

                    if (!String.IsNullOrWhiteSpace(objInvitacion.UrlPDFInvitacion))
                        new FileInfo(Path.Combine(Server.MapPath(InvitacionModel.PDF_INVITACION_PATH), objInvitacion.UrlPDFInvitacion)).Delete();
                }

                List<FotoInvitacion> lstFoto = objInvitacion.FotoInvitacion.ToList();

                //Elimina el producto
                objInvitacionBC.EliminarInvitacion(IdInvitacion);

                //Elimina todas sus fotos
                foreach (FotoInvitacion objFoto in lstFoto)
                    if (!String.IsNullOrWhiteSpace(objFoto.Foto))
                        new FileInfo(Path.Combine(Server.MapPath(FotoModel.IMAGE_INVITACION_PATH), objFoto.Foto)).Delete();

                objResultObject.Code = 0;
                objResultObject.Message = "¡La invitación se ha eliminado con éxito!";
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al eliminar la invitación. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }
        [PermissionsFilter(new String[] { Constants.Usuario.Perfil.ADMINISTRADOR })]
        [HttpPost]
        public ActionResult GuardarInvitacion(InvitacionModel objInvitacionModel, HttpPostedFileBase[] FotoFile, int[] IdFotos, HttpPostedFileBase FondoFile, HttpPostedFileBase PDFFile, HttpPostedFileBase AudioFile, HttpPostedFileBase VideoFile)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                //Validación de datos generales de producto
                if (!String.IsNullOrWhiteSpace(objInvitacionModel.ClienteInvitacion) && !String.IsNullOrWhiteSpace(objInvitacionModel.FechaEvento.ToString()) && !String.IsNullOrWhiteSpace(objInvitacionModel.HoraEvento.ToString())
                    && !String.IsNullOrWhiteSpace(objInvitacionModel.ColorAudio) && !String.IsNullOrWhiteSpace(objInvitacionModel.ColorContador) && !String.IsNullOrWhiteSpace(objInvitacionModel.TiempoCarga.ToString()))
                {

                    InvitacionBC objInvitacionBC = new InvitacionBC();
                    Invitacion objInvitacion = objInvitacionBC.ObtenerInvitacion(objInvitacionModel.IdInvitacion);

                    //Elimina las imagenes anteriores

                    if (IdFotos == null)
                        IdFotos = new int[] { };

                    if (objInvitacion != null)
                    {
                        objInvitacionModel.TipoInvitacion = objInvitacion.TipoInvitacion.Trim();
                        IEnumerable<FotoInvitacion> lstFotoEliminar = objInvitacion.FotoInvitacion.Where(b => IdFotos.All(i => b.IdFotoInvitacion != i));
                        foreach (FotoInvitacion objFoto in lstFotoEliminar)
                            if (!String.IsNullOrWhiteSpace(objFoto.Foto))
                                new FileInfo(Path.Combine(Server.MapPath(FotoModel.IMAGE_INVITACION_PATH), objFoto.Foto)).Delete();

                    }
                    if (FondoFile != null)
                    {
                        if (objInvitacion != null && !String.IsNullOrWhiteSpace(objInvitacion.Fondo))
                            new FileInfo(Path.Combine(Server.MapPath(InvitacionModel.FONDO_INVITACION_PATH), objInvitacion.Fondo)).Delete();

                        //Guarda la nueva imagen fondo
                        objInvitacionModel.FondoFile = Guid.NewGuid().ToString() + Path.GetExtension(FondoFile.FileName);
                        Directory.CreateDirectory(Server.MapPath(InvitacionModel.INVITACION_PATH));
                        Directory.CreateDirectory(Server.MapPath(InvitacionModel.FONDO_INVITACION_PATH));
                        FondoFile.SaveAs(Path.Combine(Server.MapPath(InvitacionModel.FONDO_INVITACION_PATH), objInvitacionModel.FondoFile));
                    }


                    if (PDFFile != null)
                    {
                        if (objInvitacion != null)
                        {
                            if (!String.IsNullOrWhiteSpace(objInvitacion.UrlPDFInvitacion) && objInvitacion.TipoInvitacion == Constants.Invitacion.TipoInvitacion.PDF || objInvitacion.TipoInvitacion == Constants.Invitacion.TipoInvitacion.VIDEO)
                                new FileInfo(Path.Combine(Server.MapPath(InvitacionModel.PDF_INVITACION_PATH), objInvitacion.UrlPDFInvitacion)).Delete();
                        }
                        //Guarda la nuevo PDF 
                        objInvitacionModel.PDFFile = Guid.NewGuid().ToString() + Path.GetExtension(PDFFile.FileName);
                        Directory.CreateDirectory(Server.MapPath(InvitacionModel.INVITACION_PATH));
                        Directory.CreateDirectory(Server.MapPath(InvitacionModel.PDF_INVITACION_PATH));
                        PDFFile.SaveAs(Path.Combine(Server.MapPath(InvitacionModel.PDF_INVITACION_PATH), objInvitacionModel.PDFFile));
                    }


                    if (AudioFile != null)
                    {
                        if (objInvitacion != null)
                        {
                            if (!String.IsNullOrWhiteSpace(objInvitacion.Audio) && objInvitacion.TipoInvitacion == Constants.Invitacion.TipoInvitacion.PDF || objInvitacion.TipoInvitacion == Constants.Invitacion.TipoInvitacion.ANIMADO)
                                new FileInfo(Path.Combine(Server.MapPath(InvitacionModel.AUDIO_INVITACION_PATH), objInvitacion.Audio)).Delete();
                        }
                        objInvitacionModel.AudioFile = Guid.NewGuid().ToString() + Path.GetExtension(AudioFile.FileName);
                        Directory.CreateDirectory(Server.MapPath(InvitacionModel.INVITACION_PATH));
                        Directory.CreateDirectory(Server.MapPath(InvitacionModel.AUDIO_INVITACION_PATH));
                        AudioFile.SaveAs(Path.Combine(Server.MapPath(InvitacionModel.AUDIO_INVITACION_PATH), objInvitacionModel.AudioFile));
                    }


                    if (VideoFile != null)
                    {
                        if (objInvitacion != null)
                        {
                            if (!String.IsNullOrWhiteSpace(objInvitacion.VideoInvitacion) && objInvitacion.TipoInvitacion == Constants.Invitacion.TipoInvitacion.VIDEO)
                                new FileInfo(Path.Combine(Server.MapPath(InvitacionModel.VIDEO_INVITACION_PATH), objInvitacion.VideoInvitacion)).Delete();
                        }
                        objInvitacionModel.VideoFile = Guid.NewGuid().ToString() + Path.GetExtension(VideoFile.FileName);
                        Directory.CreateDirectory(Server.MapPath(InvitacionModel.INVITACION_PATH));
                        Directory.CreateDirectory(Server.MapPath(InvitacionModel.VIDEO_INVITACION_PATH));
                        VideoFile.SaveAs(Path.Combine(Server.MapPath(InvitacionModel.VIDEO_INVITACION_PATH), objInvitacionModel.VideoFile));
                    }



                    //Guarda las nuevas fotos
                    List<FotoInvitacion> lstFotoNueva = null;
                    if (FotoFile != null)
                    {
                        Directory.CreateDirectory(Server.MapPath(FotoModel.IMAGE_INVITACION_PATH));

                        lstFotoNueva = new List<FotoInvitacion>();
                        foreach (HttpPostedFileBase objFotoFile in FotoFile)
                        {
                            if (objFotoFile != null)
                            {
                                FotoInvitacion objFoto = new FotoInvitacion();
                                objFoto.Foto = Guid.NewGuid().ToString() + Path.GetExtension(objFotoFile.FileName);
                                objFotoFile.SaveAs(Path.Combine(Server.MapPath(FotoModel.IMAGE_INVITACION_PATH), objFoto.Foto));

                                lstFotoNueva.Add(objFoto);
                            }
                        }
                    }

                    objInvitacion = objInvitacionModel.ToInvitacion();

                    //Guarda el producto
                    objInvitacion.IdInvitacion = objInvitacionBC.GuardarInvitacion(objInvitacion, lstFotoNueva, IdFotos);
                    //Cambio de datos de archivo




                    objInvitacion = objInvitacionBC.ObtenerInvitacion(objInvitacion.IdInvitacion);
                    //Directorio de Invitacion             

                    string ArchivoOrigen = Server.MapPath("~/TemplatesInvitaciones/Invitacion" + objInvitacionModel.TipoInvitacion + "/Index.html");
                    string DirectorioDestino = Server.MapPath("~/event/Inv-" + objInvitacion.IdInvitacion + "/" +
                        objInvitacion.Cliente.Replace(" ", "").Replace("&", "y").Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u") + "/");

                    string NombreArchivo = Path.GetFileName(ArchivoOrigen);
                    //Plantillas
                    string RegistroTemplate = System.IO.File.ReadAllText(ArchivoOrigen, Encoding.UTF8);

                    //Datos generales
                    String Invitacion = RegistroTemplate
                        .Replace("[FondoFile]", objInvitacion.Fondo)
                        .Replace("[ColorAudio]", objInvitacion.Color)
                        .Replace("[ColorContador]", objInvitacion.CountdownColor)
                        .Replace("[FechaEvento]", objInvitacion.FechaInvitacion.Value.ToString("yyyy-MM-dd"))
                        .Replace("[HoraEvento]", objInvitacion.HoraInvitacion.Value.ToString())
                        .Replace("[AudioFile]", objInvitacion.Audio)
                        .Replace("[VideoFile]", objInvitacion.VideoInvitacion)
                        .Replace("[PDFFile]", objInvitacion.UrlPDFInvitacion)
                        .Replace("[Cliente]", objInvitacion.Cliente)
                         .Replace("[TiempoCarga]", objInvitacion.TiempoCarga.Value.ToString())
                        .Replace("[UrlClient]", ConfigurationManager.AppSettings["UrlClient"]);



                    //Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Invitaciones"));
                    Directory.CreateDirectory(Server.MapPath("~/event/Inv-" + objInvitacion.IdInvitacion + "/"));
                    Directory.CreateDirectory(DirectorioDestino);

                    string nuevaRuta = Path.Combine(DirectorioDestino, NombreArchivo);
                    if (!System.IO.File.Exists(nuevaRuta))
                    {
                        System.IO.File.Copy(ArchivoOrigen, nuevaRuta);
                    }
                    System.IO.File.WriteAllText(Path.Combine(DirectorioDestino, "Index.html"), Invitacion);

                    objResultObject.Code = 0;
                    objResultObject.Data = objInvitacion.IdInvitacion;
                    objResultObject.Message = "¡La invitación se ha guardado con éxito!";

                }
                else
                {
                    objResultObject.Code = ERROR_INCOMPLETE_DATA;
                    //objResultObject.Message = "Debes completar todos los campos requeridos.";
                    objResultObject.Message = "Debes completar todos los campos requeridos.";
                }
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al guardar los datos de la invitación. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }
        public ActionResult NuevaInvitacion()
        {
            return View("Detalle");
        }
        public ActionResult Detalle(int id)
        {
            InvitacionBC objInvitacionBC = new InvitacionBC();
            Invitacion objInvitacion = objInvitacionBC.ObtenerInvitacion(id);


            return View(InvitacionModel.FromInvitacion(objInvitacion, true));

        }
        public ActionResult Foto(FotoModel objFotoModel)
        {
            return PartialView(objFotoModel);
        }
        public ActionResult InvitacionTipo(string TipoInvitacion, int IdInvitacion, string Cliente)
        {
            try
            {

                if (TipoInvitacion == Constants.Invitacion.TipoInvitacion.VIDEO)
                {
                    return RedirectToAction("InvitacionVideo", "Tipo", new { IdInvitacion = IdInvitacion, Cliente = Cliente });
                }
                else if (TipoInvitacion == Constants.Invitacion.TipoInvitacion.PDF)
                {
                    return RedirectToAction("InvitacionPDF", "Tipo", new { IdInvitacion = IdInvitacion, Cliente = Cliente });
                }
                else
                {
                    return RedirectToAction("InvitacionAnimada", "Tipo", new { IdInvitacion = IdInvitacion, Cliente = Cliente });
                }

            }
            catch (Exception ex)
            {

                ViewBag.Message = "Ocurrió un error al encontrar la invitación.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }


        }

        public ActionResult InvitacionAnimada()
        {
            return View();
        }

    }
}