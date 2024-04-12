using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class PersonaDA
    {
        public IQueryable<Persona> ListarPersonas(String Perfil, String Busqueda, ValueContainer TotalPages, int? Page, int? PageSize)
        {
            try
            {
                String[] Palabras = { };

                if (!String.IsNullOrWhiteSpace(Busqueda))
                    Palabras = Busqueda.Split(' ');
                else
                    Busqueda = null;

                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<Persona> lstPersona = objModel.Persona.Where(p =>
                    (Perfil == null || p.Usuario.Perfil == Perfil) &&
                    (Busqueda == null || Palabras.All(w => p.Nombre.Contains(w) || p.PrimerApellido.Contains(w) || p.SegundoApellido.Contains(w))) &&
                    p.Eliminado==false);

                if (TotalPages != null && Page != null && PageSize != null)
                {
                    TotalPages.Value = (int)Math.Ceiling(lstPersona.Count() / (double)PageSize.Value);
                    lstPersona = lstPersona.OrderBy(u => u.IdUsuario).Skip((Page.Value - 1) * PageSize.Value).Take(PageSize.Value);
                }
                else if (TotalPages != null)
                    TotalPages.Value = 1;

                return lstPersona;
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Persona.SingleOrDefault(p => p.IdPersona == IdPersona && p.Eliminado==false);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Persona.FirstOrDefault(p => p.DocumentoIdentidad == DocumentoIdentidad && p.Eliminado==false);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Persona objPersona = objModel.Persona.SingleOrDefault(p => p.IdPersona == IdPersona);
                objPersona.Eliminado = true;
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GuardarPersona(Persona objPersona)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                if (objPersona.IdPersona == 0)
                    objModel.Persona.Add(objPersona);
                else
                {
                    Persona objPersonaBD = objModel.Persona.SingleOrDefault(p => p.IdPersona == objPersona.IdPersona && p.Eliminado==false);
                    objPersonaBD.TipoDocumento = objPersona.TipoDocumento;
                    objPersonaBD.DocumentoIdentidad = objPersona.DocumentoIdentidad;
                    objPersonaBD.Nombre = objPersona.Nombre;
                    objPersonaBD.PrimerApellido = objPersona.PrimerApellido;
                    objPersonaBD.SegundoApellido = objPersona.SegundoApellido;
                    objPersonaBD.FechaNacimiento = objPersona.FechaNacimiento;
                    objPersonaBD.Sexo = objPersona.Sexo;
                    objPersonaBD.Email = objPersona.Email;
                    objPersonaBD.Telefono = objPersona.Telefono;
                    objPersonaBD.Foto = objPersona.Foto;
                }

                objModel.SaveChanges();
                return objPersona.IdPersona;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
