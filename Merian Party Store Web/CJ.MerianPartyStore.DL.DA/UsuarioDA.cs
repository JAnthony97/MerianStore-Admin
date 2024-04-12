using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class UsuarioDA
    {
        public Usuario ObtenerUsuario(int IdUsuario)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Usuario objUsuario = objModel.Usuario.SingleOrDefault(u => u.IdUsuario == IdUsuario);
                return objUsuario;
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();              
                Usuario objUsuario = objModel.Usuario.FirstOrDefault(u => u.Username == Username);
               // Usuario objUsuario = objModel.Usuario.FirstOrDefault(p => p.Persona.Select(t=>t.Email).FirstOrDefault()== Username);
                return objUsuario;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int GuardarUsuario(Usuario objUsuario)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();

                if (objUsuario.IdUsuario == 0)
                {
                    objUsuario.FechaRegistro = DateTimeHelper.PeruDateTime;
                    objUsuario.Activo = true;
                    objModel.Usuario.Add(objUsuario);
                }
                else
                {
                    Usuario objUsuarioBd = objModel.Usuario.SingleOrDefault(u => u.IdUsuario == objUsuario.IdUsuario);
                    objUsuarioBd.Username = objUsuario.Username;

                    if (objUsuario.Password != null)
                        objUsuarioBd.Password = objUsuario.Password;

                    if (objUsuario.Perfil != null)
                        objUsuarioBd.Perfil = objUsuario.Perfil;

                    if (objUsuario.PasswordAntiguo != null)
                        objUsuarioBd.PasswordAntiguo = objUsuario.PasswordAntiguo;

                    //if (objUsuario.CulqiId != null)
                    //    objUsuarioBd.CulqiId = objUsuario.CulqiId;

                    objUsuario = objUsuarioBd;
                }

                objModel.SaveChanges();

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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Usuario objUsuarioBd = objModel.Usuario.SingleOrDefault(u => u.IdUsuario == objUsuario.IdUsuario);


                if (objUsuario.Password != null)
                    objUsuarioBd.Password = objUsuario.Password;


                if (objUsuario.PasswordAntiguo != null)
                    objUsuarioBd.PasswordAntiguo = objUsuario.PasswordAntiguo;

                objModel.SaveChanges();

                return objUsuario.IdUsuario;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public void EliminarUsuario(int IdUsuario)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Usuario objUsuario = objModel.Usuario.SingleOrDefault(u => u.IdUsuario == IdUsuario);

                //objUsuario.Marca.Clear();
                objUsuario.Persona.Clear();
                objModel.Usuario.Remove(objUsuario);

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Usuario Login(String Username, String Password)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Usuario objUsuario = objModel.Usuario.FirstOrDefault(u => u.Username == Username && u.Password == Password && u.Activo==true);
                return objUsuario;
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Usuario objUsuario = objModel.Usuario.SingleOrDefault(u => u.IdUsuario == IdUsuario);
                objUsuario.Activo = Activo;

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
