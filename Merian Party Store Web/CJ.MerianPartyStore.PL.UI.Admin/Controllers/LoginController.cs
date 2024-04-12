using CJ.MerianPartyStore.BL.BC;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.PL.UI.Admin.Models;
using CJ.MerianPartyStore.Util;
using System;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Security;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CJ.MerianPartyStore.PL.UI.Admin.Controllers
{
    public class LoginController : Controller
    {

        public const int ERROR_PASSWORDS_NOT_MATCH = 1;
        public const int ERROR_INVALID_USER = 2;
        public const int ERROR_INCOMPLETE_DATA = 3;
        public const int ERROR_PASSWORD_VALIDATION = 4;
        public const int ERROR_EXISTENT_USER = 5;
        public const String TEMPLATE_PATH = "~/Templates";
        // GET: Login
        public ActionResult Index(String Username, String Password, String ReturnUrl, String Mensaje)
        {

            //string hostName = Dns.GetHostName(); // Obtener el nombre del host local
            //IPAddress[] addresses = Dns.GetHostAddresses(hostName); // Obtener todas las direcciones IP asociadas al nombre del host
            //string Ipv4 = "";
            //string macAddressString = "";
            //foreach (IPAddress address in addresses)
            //{
            //    // Filtrar solo las direcciones IPv4
            //    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            //    {
            //        Ipv4 = address.ToString();
            //        //Console.WriteLine("Dirección IP de tu PC: " + address.ToString());
            //    }
            //}

            //foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            //{
            //    // Filtrar solo las interfaces Ethernet o WiFi (para evitar obtener la dirección MAC de otras interfaces como Bluetooth).
            //    if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet || nic.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
            //    {
            //        PhysicalAddress macAddress = nic.GetPhysicalAddress();
            //        byte[] bytes = macAddress.GetAddressBytes();
            //         macAddressString = string.Join(":", bytes.Select(b => b.ToString("X2")));

            //        Console.WriteLine("Dirección MAC de tu PC: " + macAddressString);
            //        break; // Detener el bucle después de encontrar la primera interfaz Ethernet o WiFi.
            //    }
            //}

            //if (hostName != "DESKTOP-53R3ORS" || Ipv4 != "192.168.1.156" || macAddressString!= "1C:1B:0D:0F:E0:8E")
            //    return Redirect("http://www.merianinvitacionesdigitales.com/");

            if (User.Identity.IsAuthenticated)
                return RedirectToAction("", "");

            if (Username == null ||
                Password == null)
            {
                ViewBag.Message = Mensaje;
                return View();
            }
            if (!Request.HttpMethod.Equals("POST"))
                return View();

            Username = Username.Trim();
            ViewBag.Username = Username;

            if (String.IsNullOrEmpty(Username) ||
                String.IsNullOrEmpty(Password))
            {
                ViewBag.Message = "Debes completar todos los campos.";
                return View();
            }

            try
            {
                Usuario objUsuario = new UsuarioBC().Login(Username, Encryptor.SHA256Hash(Password));

                if (objUsuario != null)
                {
                    UsuarioModel objUsuarioModel = UsuarioModel.FromUsuario(objUsuario, true);
                    FormsAuthentication.SetAuthCookie(objUsuarioModel.ToString(), false);
                    if (String.IsNullOrEmpty(ReturnUrl))
                        return RedirectToRoute("Default");
                    else
                        return Redirect(ReturnUrl);
                }
                else
                {
                    ViewBag.Message = "Tus datos de acceso son incorrectos.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al iniciar sesión";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }


        public ActionResult RecuperarPassword(String Message)
        {
            if (!String.IsNullOrEmpty(Message))
                ViewBag.Message = Message;

            return View();

        }

        public ActionResult PasswordRecuperado(String Username)
        {
            if (String.IsNullOrEmpty(Username))
            {
                ViewBag.Message = "Debe ingresar su correo.";
                return RedirectToAction("RecuperarPassword", new { Message = ViewBag.Message });
            }
            try
            {
                UsuarioBC objUsuarioBC = new UsuarioBC();
                Guid guid = Guid.NewGuid();
                Usuario objUsuarioExistente = objUsuarioBC.ObtenerUsuario(Username);

                if (objUsuarioExistente != null)
                {
                    objUsuarioExistente.PasswordAntiguo = objUsuarioExistente.Password;
                    String NewPassword = guid.ToString().Substring(0, 8).Replace("-", "");
                    objUsuarioExistente.Password = Encryptor.SHA256Hash(NewPassword);
                    //Recuperar Password el usuario
                    objUsuarioExistente.IdUsuario = objUsuarioBC.RecuperarPasswordUsuario(objUsuarioExistente, objUsuarioExistente.Persona.FirstOrDefault(), Server.MapPath(TEMPLATE_PATH), string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~")), NewPassword);
                    return View();
                }
                else
                {
                    ViewBag.Message = "El correo ingresado no existe.";
                    return RedirectToAction("RecuperarPassword", new { Message = ViewBag.Message });

                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al iniciar sesión";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }

        public ActionResult PasswordAnteriorRecuperado(String Username)
        {
            try
            {
                UsuarioBC objUsuarioBC = new UsuarioBC();
                Guid guid = Guid.NewGuid();
                Usuario objUsuarioExistente = objUsuarioBC.ObtenerUsuario(Username);


                if (objUsuarioExistente != null)
                {

                    objUsuarioExistente.Password = objUsuarioExistente.PasswordAntiguo;
                    objUsuarioExistente.PasswordAntiguo = objUsuarioExistente.Password;
                    int IdUsuario = objUsuarioBC.ActualizarPasswordAntiguo(objUsuarioExistente);
                    //Recuperar Password el usuario
                    ViewBag.Message = "Se recupero el password anterior.";

                }
                return RedirectToAction("Index", new { Mensaje = ViewBag.Message });
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al iniciar sesión";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }
    }
}