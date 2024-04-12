using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using CJ.MerianPartyStore.Util;
using System.IO;
using CJ.MerianPartyStore.DL.DM;

namespace CJ.MerianPartyStore.BL.BC
{

    public class ComunicacionBC
    {
        private const String MAIL_REGISTRO = "Registro.html";
        private const String MAIL_RECUPERAR_PASSWORD = "RecuperarPassword.html";
        public void EnviarEmailRegistro(Persona objPersona, Usuario objUsuario, String TemplatePath, String RootPath)
        {
            try
            {
                //Plantillas
                string RegistroTemplate = File.ReadAllText(Path.Combine(TemplatePath, MAIL_REGISTRO), Encoding.UTF8);

                //Datos generales
                String Email = RegistroTemplate
                    .Replace("[Nombre]", objPersona.Nombre.ToUpper())
                    .Replace("[Username]", objUsuario.Username)
                    .Replace("[UrlRoot]", RootPath);

                EnviarEmail("¡Bienvenido(a) a Doctor Gadget! Confía en el doc. 🤓", Email, new String[] { objPersona.Email }, null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EnviarEmailRecuperarPassword(Persona objPersona, Usuario objUsuario, String TemplatePath, String RootPath,String Password)
        {
            try
            {
                //Plantillas
                string RegistroTemplate = File.ReadAllText(Path.Combine(TemplatePath, MAIL_RECUPERAR_PASSWORD), Encoding.UTF8);

                //Datos generales
                String Email = RegistroTemplate
                    .Replace("[Nombre]", objPersona.Nombre.ToUpper())
                    .Replace("[Password]", Password)
                    .Replace("[UrlRoot]", RootPath)
                    .Replace("[UrlRestablecerPasswordAnterior]", ConfigurationManager.AppSettings["UrlClient"] + "Login/PasswordAnteriorRecuperado?Username=" + objUsuario.Username);

                EnviarEmail("¡Password Recuperado por Merian Party Store! 💖", Email, new String[] { (objUsuario.Username==Constants.Usuario.MASTER ? ConfigurationManager.AppSettings["EmailMaster"] : objUsuario.Username) }, null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EnviarEmail(String Asunto, String Contenido, String[] Destinatarios, Attachment[] Adjuntos)
        {
            try
            {
                MailMessage objMailMessage = new MailMessage();
                objMailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SMTPUser"], "Merian Party Store");

                foreach (String Destinatario in Destinatarios)
                    objMailMessage.To.Add(Destinatario);

                objMailMessage.Subject = Asunto;
                objMailMessage.IsBodyHtml = true;
                objMailMessage.Body = Contenido;

                if (Adjuntos != null)
                    foreach (Attachment objAdjunto in Adjuntos)
                        objMailMessage.Attachments.Add(objAdjunto);

                Mailer.Send(objMailMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
