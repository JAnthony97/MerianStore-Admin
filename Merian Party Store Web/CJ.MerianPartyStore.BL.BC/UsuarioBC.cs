using CJ.MerianPartyStore.DL.DA;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CJ.MerianPartyStore.BL.BC
{
    public class UsuarioBC
    {
        private const String FACEBOOK_GRAPH_URL = "https://graph.facebook.com/{UID}?fields=id,first_name,last_name,email&access_token={AccessToken}";
        private const String FACEBOOK_PICTURE_URL = "https://graph.facebook.com/{UID}/picture?type=large";
        private const String GOOGLE_API_URL = "https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={AccessToken}";
        private const String USER_ID = "{UID}";
        private const String ACCESS_TOKEN = "{AccessToken}";

        public Usuario Login(String Username, String Password)
        {
            try
            {
                return new UsuarioDA().Login(Username, Password);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        //public Usuario LoginFacebook(String ID, String AccessToken, String TemplatePath, String RootPath)
        //{
        //    try
        //    {
        //        //Consulta los datos a Facebook
        //        JObject jResult = APIClient.Request(FACEBOOK_GRAPH_URL.Replace(USER_ID, ID).Replace(ACCESS_TOKEN, AccessToken), APIClient.ContentType.FORM, null, null);
        //        String Email = (String)jResult["email"];
        //        String Nombre = (String)jResult["first_name"];
        //        String Apellido = (String)jResult["last_name"];

        //        UsuarioDA objUsuarioDA = new UsuarioDA();
        //        PersonaDA objPersonaDA = new PersonaDA();

        //        //Verifica si el usuario ya existe
        //        Usuario objUsuario = objUsuarioDA.ObtenerUsuario(Email);

        //        //Si no existe, lo crea
        //        if (objUsuario == null)
        //        {
        //            //Culqi
        //            JObject jCulqiResult = new CulqiBC().CrearCliente(Constants.Ubicacion.Pais.PERU, Email, Nombre, Apellido, null);
        //            String CulqiId = (String)jCulqiResult["id"];

        //            //Usuario
        //            objUsuario = new Usuario();
        //            objUsuario.Username = Email;
        //            objUsuario.Perfil = Constants.Usuario.Perfil.CLIENTE;
        //            objUsuario.Activo = true;
        //            objUsuario.CulqiId = CulqiId;
        //            objUsuario.IdUsuario = objUsuarioDA.GuardarUsuario(objUsuario);

        //            //Persona
        //            Persona objPersona = new Persona();
        //            objPersona.IdUsuario = objUsuario.IdUsuario;
        //            objPersona.Nombre = Nombre;
        //            objPersona.PrimerApellido = Apellido;
        //            objPersona.Email = Email;
        //            objPersona.Foto = FACEBOOK_PICTURE_URL.Replace(USER_ID, ID);
        //            objPersonaDA.GuardarPersona(objPersona);

        //            new ComunicacionBC().EnviarEmailRegistro(objPersona, objUsuario, TemplatePath, RootPath);

        //            objUsuario = objUsuarioDA.ObtenerUsuario(Email);
        //        }

        //        return objUsuario;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //public Usuario LoginGoogle(String AccessToken, String TemplatePath, String RootPath)
        //{
        //    try
        //    {
        //        //Consulta los datos a Google
        //        JObject jResult = APIClient.Request(GOOGLE_API_URL.Replace(ACCESS_TOKEN, AccessToken), APIClient.ContentType.JSON, null, null);
        //        String Email = (String)jResult["email"];
        //        String Nombre = (String)jResult["given_name"];
        //        String Apellido = (String)jResult["family_name"];

        //        UsuarioDA objUsuarioDA = new UsuarioDA();
        //        PersonaDA objPersonaDA = new PersonaDA();

        //        //Verifica si el usuario ya existe
        //        Usuario objUsuario = objUsuarioDA.ObtenerUsuario(Email);

        //        //Si no existe, lo crea
        //        if (objUsuario == null)
        //        {
        //            //Culqi
        //            JObject jCulqiResult = new CulqiBC().CrearCliente(Constants.Ubicacion.Pais.PERU, Email, Nombre, Apellido, null);
        //            String CulqiId = (String)jCulqiResult["id"];

        //            //Usuario
        //            objUsuario = new Usuario();
        //            objUsuario.Username = Email;
        //            objUsuario.Perfil = Constants.Usuario.Perfil.CLIENTE;
        //            objUsuario.Activo = true;
        //            objUsuario.CulqiId = CulqiId;
        //            objUsuario.IdUsuario = objUsuarioDA.GuardarUsuario(objUsuario);

        //            //Persona
        //            Persona objPersona = new Persona();
        //            objPersona.IdUsuario = objUsuario.IdUsuario;
        //            objPersona.Nombre = Nombre;
        //            objPersona.PrimerApellido = Apellido;
        //            objPersona.Email = Email;
        //            objPersona.Foto = (String)jResult["picture"];
        //            objPersonaDA.GuardarPersona(objPersona);

        //            new ComunicacionBC().EnviarEmailRegistro(objPersona, objUsuario, TemplatePath, RootPath);

        //            objUsuario = objUsuarioDA.ObtenerUsuario(Email);
        //        }

        //        return objUsuario;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public Usuario ObtenerUsuario(int IdUsuario)
        {
            try
            {
                return new UsuarioDA().ObtenerUsuario(IdUsuario);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Usuario ObtenerUsuario(String Username)
        {
            try
            {
                return new UsuarioDA().ObtenerUsuario(Username);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GuardarUsuario(Usuario objUsuario, Persona objPersona, String TemplatePath, String RootPath)
        {
            try
            {
                UsuarioDA objUsuarioDA = new UsuarioDA();
                PersonaDA objPersonaDA = new PersonaDA();
                bool Nuevo = false;

                if (objUsuario.IdUsuario == 0)
                {
                    ////Culqi
                    //JObject jCulqiResult = new CulqiBC().CrearCliente(Constants.Ubicacion.Pais.PERU, objPersona.Email, objPersona.Nombre, objPersona.PrimerApellido + " " + objPersona.SegundoApellido, objPersona.Telefono);
                    //String CulqiId = (String)jCulqiResult["id"];
                    //objUsuario.CulqiId = CulqiId;
                    Nuevo = true;
                }

                objUsuario.IdUsuario = objUsuarioDA.GuardarUsuario(objUsuario);

                if (objPersona != null)
                {
                    objPersona.IdUsuario = objUsuario.IdUsuario;
                    objPersonaDA.GuardarPersona(objPersona);
                }

                //if (Nuevo)
                //    new ComunicacionBC().EnviarEmailRegistro(objPersona, objUsuario, TemplatePath, RootPath);

                return objUsuario.IdUsuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int ActualizarPasswordAntiguo(Usuario objUsuario) {
            try
            {
                UsuarioDA objUsuarioDA = new UsuarioDA();
                objUsuario.IdUsuario = objUsuarioDA.ActualizarPasswordAntiguo(objUsuario);

                return objUsuario.IdUsuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int RecuperarPasswordUsuario(Usuario objUsuario, Persona objPersona, String TemplatePath, String RootPath,string Password)
        {
            try
            {
                UsuarioDA objUsuarioDA = new UsuarioDA();
                objUsuario.IdUsuario = objUsuarioDA.GuardarUsuario(objUsuario);
                new ComunicacionBC().EnviarEmailRecuperarPassword(objPersona, objUsuario, TemplatePath, RootPath, Password);
                return objUsuario.IdUsuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
            public void CambiarEstadoUsuario(int IdUsuario, bool Activo)
        {
            try
            {
                UsuarioDA objUsuarioDA = new UsuarioDA();
                objUsuarioDA.CambiarEstadoUsuario(IdUsuario, Activo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Persona> ListarPersonas(String Perfil, String Busqueda, ValueContainer TotalPages, int? Page, int? PageSize)
        {
            try
            {
                return new PersonaDA().ListarPersonas(Perfil, Busqueda, TotalPages, Page, PageSize);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Persona ObtenerPersona(int IdPersona)
        {
            try
            {
                return new PersonaDA().ObtenerPersona(IdPersona);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Persona ObtenerPersona(String DocumentoIdentidad)
        {
            try
            {
                return new PersonaDA().ObtenerPersona(DocumentoIdentidad);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarPersona(int IdPersona)
        {
            try
            {
                UsuarioDA objUsuarioDA = new UsuarioDA();
                PersonaDA objPersonaDA = new PersonaDA();

                Persona objPersona = objPersonaDA.ObtenerPersona(IdPersona);

                objPersonaDA.EliminarPersona(objPersona.IdPersona);

                if (objPersona.IdUsuario != null)
                    objUsuarioDA.EliminarUsuario(objPersona.IdUsuario.Value);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
