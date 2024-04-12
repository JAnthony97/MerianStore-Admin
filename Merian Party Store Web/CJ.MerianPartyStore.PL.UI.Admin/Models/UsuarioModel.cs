using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class UsuarioModel
    {

        public int IdUsuario { get; set; }
        public int IdPersona { get; set; }
        public int IdMarca { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public String TipoDocumento { get; set; }
        public String DocumentoIdentidad { get; set; }
        public String Nombre { get; set; }
        public String RazonSocial { get; set; }
        public String PrimerApellido { get; set; }
        public String SegundoApellido { get; set; }
        public String FechaNacimiento { get; set; }
        public String FechaRegistro { get; set; }
        public String Sexo { get; set; }
        public String Email { get; set; }
        public String Telefono { get; set; }
        public String Direccion { get; set; }
        public String Foto { get; set; }
        public String Perfil { get; set; }
        public bool Activo { get; set; }

        public const String TEMPLATE_PATH = "~/Templates";
        public static UsuarioModel FromUsuario(Usuario objUsuario, bool DatosPersona)
        {
            try
            {
                UsuarioModel objUsuarioModel = null;

                if (DatosPersona)
                {
                    if (objUsuario.Persona.Count == 1)
                        objUsuarioModel = FromPersona(objUsuario.Persona.FirstOrDefault(), false);
                  
                    else
                    {
                        objUsuarioModel = new UsuarioModel();

                        objUsuarioModel.TipoDocumento = "-";
                        objUsuarioModel.DocumentoIdentidad = "-";
                        objUsuarioModel.Nombre = "Merian";
                        objUsuarioModel.PrimerApellido = "Party";
                        objUsuarioModel.SegundoApellido = "Store";
                        objUsuarioModel.FechaNacimiento = "-";
                        objUsuarioModel.Email = "-";
                        objUsuarioModel.Telefono = "-";
                    }
                }
                else
                    objUsuarioModel = new UsuarioModel();

                objUsuarioModel.IdUsuario = objUsuario.IdUsuario;
                objUsuarioModel.Username = objUsuario.Username;
                objUsuarioModel.Perfil = objUsuario.Perfil;
                objUsuarioModel.Activo = objUsuario.Activo.Value;

                if (objUsuario.FechaRegistro != null)
                    objUsuarioModel.FechaRegistro = DateTimeHelper.ToString(objUsuario.FechaRegistro.Value, true, false);

                return objUsuarioModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static UsuarioModel FromPersona(Persona objPersona, bool DatosUsuario)
        {
            try
            {
                UsuarioModel objUsuarioModel;

                if (DatosUsuario && objPersona.Usuario != null)
                    objUsuarioModel = FromUsuario(objPersona.Usuario, false);
                else
                    objUsuarioModel = new UsuarioModel();

                objUsuarioModel.IdPersona = objPersona.IdPersona;
                objUsuarioModel.TipoDocumento = objPersona.TipoDocumento;
                objUsuarioModel.DocumentoIdentidad = objPersona.DocumentoIdentidad;
                objUsuarioModel.Nombre = objPersona.Nombre;
                objUsuarioModel.PrimerApellido = objPersona.PrimerApellido;
                objUsuarioModel.SegundoApellido = objPersona.SegundoApellido;
                if (objPersona.FechaNacimiento != null)
                    objUsuarioModel.FechaNacimiento = DateTimeHelper.ToString(objPersona.FechaNacimiento.Value, true, false);
                objUsuarioModel.Sexo = objPersona.Sexo;
                objUsuarioModel.Email = objPersona.Email;
                objUsuarioModel.Telefono = objPersona.Telefono;

                return objUsuarioModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    
        public Usuario ToUsuario()
        {
            try
            {
                Usuario objUsuario = new Usuario();
                objUsuario.IdUsuario = IdUsuario;
                objUsuario.Username = Username;
                objUsuario.Password = Password;
                objUsuario.Perfil = Perfil;

                return objUsuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Persona ToPersona()
        {
            try
            {
                Persona objPersona = new Persona();
                objPersona.IdPersona = IdPersona;
                objPersona.IdUsuario = IdUsuario;
                objPersona.Nombre = Nombre;
                objPersona.PrimerApellido = PrimerApellido;
                objPersona.SegundoApellido = SegundoApellido;
                objPersona.TipoDocumento = TipoDocumento;
                objPersona.DocumentoIdentidad = DocumentoIdentidad;
                if (!String.IsNullOrWhiteSpace(FechaNacimiento))
                    objPersona.FechaNacimiento = DateTimeHelper.FromString(FechaNacimiento, true, false);
                objPersona.Sexo = Sexo;
                objPersona.Email = Email;
                objPersona.Telefono = Telefono;

                return objPersona;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override String ToString()
        {
            try
            {
                JObject jUsuario = new JObject();
                jUsuario["IdUsuario"] = IdUsuario;
                jUsuario["IdPersona"] = IdPersona;
                jUsuario["Username"] = Username;
                jUsuario["Nombre"] = Nombre;
                jUsuario["PrimerApellido"] = PrimerApellido;
                jUsuario["SegundoApellido"] = SegundoApellido;
                jUsuario["Perfil"] = Perfil;

                return jUsuario.ToString();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static UsuarioModel FromString(String sUsuario)
        {
            try
            {
                JObject jUsuario = JObject.Parse(sUsuario);
                UsuarioModel objUsuarioModel = new UsuarioModel();
                objUsuarioModel.IdUsuario = (int)jUsuario["IdUsuario"];
                objUsuarioModel.IdPersona = (int)jUsuario["IdPersona"];
                objUsuarioModel.Username = (string)jUsuario["Username"];
                objUsuarioModel.Nombre = (string)jUsuario["Nombre"];
                objUsuarioModel.PrimerApellido = (string)jUsuario["PrimerApellido"];
                objUsuarioModel.SegundoApellido = (string)jUsuario["SegundoApellido"];
                objUsuarioModel.Perfil = (string)jUsuario["Perfil"];

                return objUsuarioModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
