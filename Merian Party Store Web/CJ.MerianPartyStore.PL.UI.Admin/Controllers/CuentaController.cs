using CJ.MerianPartyStore.BL.BC;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.PL.UI.Admin.Models;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CJ.MerianPartyStore.PL.UI.Admin.Controllers
{
    [Authorize]
    public class CuentaController : BaseController
    {
        public const int ERROR_INCOMPLETE_DATA = 1;
        // GET: Cuenta
        public ActionResult Index()
        {
            try
            {
                UsuarioBC objUsuarioBC = new UsuarioBC();
                UsuarioModel objUsuarioLogueado = UsuarioModel.FromString(User.Identity.Name);

                Usuario objUsuario = objUsuarioBC.ObtenerUsuario(objUsuarioLogueado.IdUsuario);
                objUsuarioLogueado = UsuarioModel.FromUsuario(objUsuario, true);

                return View(objUsuarioLogueado);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al obtener la información de cuenta";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }
        public const int ERROR_INVALID_PASSWORD = 1;
        public const int ERROR_DATA_VALIDATION = 2;
        public ActionResult Guardar(UsuarioModel objUsuarioModel, String PasswordActual)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                UsuarioModel objUsuarioLogueadoModel = UsuarioModel.FromString(User.Identity.Name);

                //Validaciones de campos completos
                bool Valido = true;
                if (objUsuarioLogueadoModel.Perfil != Constants.Usuario.Perfil.MERCHANT && objUsuarioLogueadoModel.Username != Constants.Usuario.MASTER)
                    Valido = !String.IsNullOrWhiteSpace(objUsuarioModel.Username) &&
                        !String.IsNullOrWhiteSpace(objUsuarioModel.DocumentoIdentidad) &&
                        !String.IsNullOrWhiteSpace(objUsuarioModel.TipoDocumento) &&
                        !String.IsNullOrWhiteSpace(objUsuarioModel.Nombre) &&
                        !String.IsNullOrWhiteSpace(objUsuarioModel.PrimerApellido) &&
                        !String.IsNullOrWhiteSpace(objUsuarioModel.Sexo);

                if (Valido)
                {
                    UsuarioBC objUsuarioBC = new UsuarioBC();

                    //Valida contraseña de usuario logueado
                    Usuario objUsuarioLogueado = objUsuarioBC.Login(objUsuarioLogueadoModel.Username, Encryptor.SHA256Hash(PasswordActual));

                    if (objUsuarioLogueado != null)
                    {
                        Usuario objUsuario = objUsuarioModel.ToUsuario();
                        Persona objPersona = null;

                        if (objUsuarioModel.IdPersona != 0)
                            objPersona = objUsuarioModel.ToPersona();

                        if (objUsuario.Password != null)
                            objUsuario.Password = Encryptor.SHA256Hash(objUsuario.Password);

                        if(objUsuario.Username==Constants.Usuario.MASTER)
                            objPersona.Email = objUsuarioLogueado.Persona.FirstOrDefault().Email;

                        //Guarda el usuario y captura errores, si es que los hay
                        objUsuarioBC.GuardarUsuario(objUsuario, objPersona, Server.MapPath(UsuarioModel.TEMPLATE_PATH), ConfigurationManager.AppSettings["UrlClient"]);

                        objResultObject.Code = 0;
                        objResultObject.Message = "¡Los datos de cuenta se han actualizado con éxito!";
                    }
                    else
                    {
                        objResultObject.Code = ERROR_INVALID_PASSWORD;
                        objResultObject.Message = "La contraseña no es válida, inténtelo nuevamente.";
                    }
                }
                else
                {
                    objResultObject.Code = ERROR_INCOMPLETE_DATA;
                    objResultObject.Message = "Debes completar todos los campos requeridos.";
                }
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al guardar los datos de cuenta. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }
    }
}