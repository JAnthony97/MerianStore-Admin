using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class InvitacionDA
    {

        public IEnumerable<Invitacion> ListarInvitaciones(String Filtro, String Busqueda)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();

                //Filtro de búsqueda
                String[] Palabras = { };
                if (!String.IsNullOrWhiteSpace(Busqueda))
                    Palabras = Busqueda.Split(' ');
                else
                    Busqueda = null;

                IEnumerable<Invitacion> lstInvitacion = objModel.Invitacion.Where(p =>
                    (Filtro == null) &&
                    (Busqueda == null || p.Cliente.StartsWith(Busqueda))).OrderBy(p => p.Cliente);

                if (Busqueda != null)
                    lstInvitacion = lstInvitacion.Union(objModel.Invitacion.Where(p =>
                        (Filtro == null) &&
                        (Palabras.All(w => p.Cliente.Contains(w))))).OrderBy(p => p.Cliente);

                return lstInvitacion.Distinct();
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Invitacion.SingleOrDefault(m => m.IdInvitacion == IdInvitacion);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Invitacion ObtenerInvitacionCliente(int IdInvitacion, string Cliente)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Invitacion.SingleOrDefault(m => m.IdInvitacion == IdInvitacion && m.Cliente== Cliente);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int GuardarInvitacion(Invitacion objInvitacion)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                if (objInvitacion.IdInvitacion == 0)
                {
                    objInvitacion.Eliminado = false;
                    objInvitacion.FechaRegistro = DateTimeHelper.PeruDateTime;
                    objModel.Invitacion.Add(objInvitacion);
                }
                else
                {
                    Invitacion objInvitacionBD = objModel.Invitacion.SingleOrDefault(m => m.IdInvitacion == objInvitacion.IdInvitacion);

                    objInvitacionBD.Cliente = objInvitacion.Cliente;
                    objInvitacionBD.FechaInvitacion = objInvitacion.FechaInvitacion;
                    objInvitacionBD.HoraInvitacion = objInvitacion.HoraInvitacion;
                    objInvitacionBD.Color = objInvitacion.Color;
                    objInvitacionBD.CountdownColor = objInvitacion.CountdownColor;   
                    objInvitacionBD.FechaActualizacion = DateTimeHelper.PeruDateTime;
                    objInvitacionBD.TiempoCarga = objInvitacion.TiempoCarga;
                    if (objInvitacion.Fondo != null)
                        objInvitacionBD.Fondo = objInvitacion.Fondo;
                    if (objInvitacion.TipoInvitacion == "PDF")
                    {
                        if (objInvitacion.UrlPDFInvitacion != null)
                            objInvitacionBD.UrlPDFInvitacion = objInvitacion.UrlPDFInvitacion;

                        if (objInvitacion.Audio != null)
                            objInvitacionBD.Audio = objInvitacion.Audio;
                    }
                    else if (objInvitacion.TipoInvitacion == "ANIMADO")
                    {
                        objInvitacionBD.Titulo = objInvitacion.Titulo;
                        objInvitacionBD.MostrarIcono = objInvitacion.MostrarIcono;
                        objInvitacionBD.LinkCalendario = objInvitacion.LinkCalendario;
                        objInvitacionBD.UbicacionGoogleMaps = objInvitacion.UbicacionGoogleMaps;
                        objInvitacionBD.UbicacionGoogleMaps2 = objInvitacion.UbicacionGoogleMaps2;
                        objInvitacionBD.TelefonoContactoInvitacion = objInvitacionBD.TelefonoContactoInvitacion;
                        objInvitacionBD.TelefonoContactoInvitacion2 = objInvitacionBD.TelefonoContactoInvitacion2;
                        objInvitacionBD.LinkMesaRegalos = objInvitacionBD.LinkMesaRegalos;
                        objInvitacionBD.TipoEntidadBancaria = objInvitacionBD.TipoEntidadBancaria;
                        objInvitacionBD.NumeroCuenta = objInvitacionBD.NumeroCuenta;
                        objInvitacionBD.CCI = objInvitacionBD.CCI;

                        if (objInvitacion.Audio != null)
                            objInvitacionBD.Audio = objInvitacion.Audio;
                    }
                    else if (objInvitacion.TipoInvitacion == "VIDEO")
                    {
                        if (objInvitacion.UrlPDFInvitacion != null)
                            objInvitacionBD.UrlPDFInvitacion = objInvitacion.UrlPDFInvitacion;

                        if (objInvitacion.VideoInvitacion != null)
                            objInvitacionBD.VideoInvitacion = objInvitacion.VideoInvitacion;
                    }
                }

                objModel.SaveChanges();
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Invitacion objInvitacion = objModel.Invitacion.SingleOrDefault(m => m.IdInvitacion == IdInvitacion);

                if (objInvitacion.FotoInvitacion.Count > 0)
                    objInvitacion.FotoInvitacion.Clear();

                objModel.Invitacion.Remove(objInvitacion);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
