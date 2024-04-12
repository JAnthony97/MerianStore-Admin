using CJ.MerianPartyStore.DL.DA;
using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.BL.BC
{
    public class InvitacionBC
    {
        public IEnumerable<Invitacion> ListarInvitaciones(String Filtro, String Busqueda)
        {
            try
            {
                return new InvitacionDA().ListarInvitaciones(Filtro, Busqueda);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Invitacion ObtenerInvitacion(int IdInvitacion)
        {
            try
            {
                return new InvitacionDA().ObtenerInvitacion(IdInvitacion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Invitacion ObtenerInvitacionCliente(int IdInvitacion,string Cliente)
        {
            try
            {
                return new InvitacionDA().ObtenerInvitacionCliente(IdInvitacion,Cliente);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int GuardarInvitacion(Invitacion objInvitacion, List<FotoInvitacion> lstFotoNuevo, int[] IdFotosMantener)
        {
            try
            {
                InvitacionDA objInvitacionDA = new InvitacionDA();

                bool Nuevo = objInvitacion.IdInvitacion == 0;

                //Guardar producto
                objInvitacion.IdInvitacion = objInvitacionDA.GuardarInvitacion(objInvitacion);

                //Eliminar fotos borradas por el usuario
                if (IdFotosMantener != null)
                    new FotoInvitacionDA().EliminarFotosInvitacionExcepto(objInvitacion.IdInvitacion, IdFotosMantener);

                //Insertar fotos nuevas
                if (lstFotoNuevo != null)
                {
                    foreach (FotoInvitacion objFoto in lstFotoNuevo)
                        objFoto.IdInvitacion = objInvitacion.IdInvitacion;

                    new FotoInvitacionDA().InsertarFotos(lstFotoNuevo);
                }
   
                return objInvitacion.IdInvitacion;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EliminarInvitacion(int IdInvitacion)
        {
            try
            {


                new FotoInvitacionDA().EliminarFotosInvitacionExcepto(IdInvitacion, new int[] { });
                new InvitacionDA().EliminarInvitacion(IdInvitacion);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
