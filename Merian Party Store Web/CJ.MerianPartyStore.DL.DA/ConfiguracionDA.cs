using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class ConfiguracionDA
    {
        public IQueryable<Configuracion> ListarConfiguracion(String Nombre)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Configuracion.Where(c => c.Nombre == Nombre);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Configuracion ObtenerConfiguracion(String Nombre, String ValorDefecto, String Valor2Defecto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Configuracion objConfiguracion = objModel.Configuracion.FirstOrDefault(c => c.Nombre == Nombre);

                if (objConfiguracion == null)
                {
                    objConfiguracion = new Configuracion();
                    objConfiguracion.Nombre = Nombre;
                    objConfiguracion.Valor = ValorDefecto;
                    objConfiguracion.Valor2 = Valor2Defecto;

                    objModel.Configuracion.Add(objConfiguracion);
                    objModel.SaveChanges();
                }

                return objConfiguracion;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void GuardarConfiguraciones(List<Configuracion> lstConfiguracion)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();

                objModel.Configuracion.AddRange(lstConfiguracion.Where(c => c.IdConfiguracion == 0));

                foreach (Configuracion objConfiguracion in lstConfiguracion.Where(c => c.IdConfiguracion != 0))
                {
                    Configuracion objConfiguracionBD = objModel.Configuracion.SingleOrDefault(c => c.IdConfiguracion == objConfiguracion.IdConfiguracion);
                    objConfiguracionBD.Nombre = objConfiguracion.Nombre;

                    if (objConfiguracion.Valor != null)
                        objConfiguracionBD.Valor = objConfiguracion.Valor;

                    if (objConfiguracion.Valor2 != null)
                        objConfiguracionBD.Valor2 = objConfiguracion.Valor2;
                }

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarConfiguraciones(int[] IdConfiguraciones)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<Configuracion> lstConfiguracion = objModel.Configuracion.Where(c => IdConfiguraciones.Any(i => i == c.IdConfiguracion));
                objModel.Configuracion.RemoveRange(lstConfiguracion);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
