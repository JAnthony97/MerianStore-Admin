using CJ.MerianPartyStore.DL.DA;
using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.BL.BC
{
    public class ConfiguracionBC
    {
        public IQueryable<Configuracion> ListarConfiguracion(String Nombre)
        {
            try
            {
                return new ConfiguracionDA().ListarConfiguracion(Nombre);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Configuracion ObtenerConfiguracion(String Nombre)
        {
            try
            {
                return new ConfiguracionDA().ObtenerConfiguracion(Nombre, ConfigurationManager.AppSettings[Nombre + "_DEFAULT"], ConfigurationManager.AppSettings[Nombre + "_DEFAULT2"]);
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
                new ConfiguracionDA().EliminarConfiguraciones(IdConfiguraciones);
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
                new ConfiguracionDA().GuardarConfiguraciones(lstConfiguracion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
