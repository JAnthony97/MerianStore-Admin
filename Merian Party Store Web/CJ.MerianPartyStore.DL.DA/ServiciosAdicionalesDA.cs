using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class ServiciosAdicionalesDA
    {
        public void GuardarServiciosAdicionales(List<ServiciosAdicionales> lstServiciosAdicionales,int IdProducto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
              
                foreach (ServiciosAdicionales objServiciosAdicionales in lstServiciosAdicionales)
                {
                    ServiciosAdicionales objServiciosAdicionalesBD = objModel.ServiciosAdicionales.FirstOrDefault(m => m.IdServicioAdicionales == objServiciosAdicionales.IdServicioAdicionales);
                    if (objServiciosAdicionalesBD == null)
                    {
                        objServiciosAdicionales.FechaRegistro = DateTimeHelper.PeruDateTime;
                        objServiciosAdicionales.IdProducto = IdProducto;
                        objServiciosAdicionales.Eliminar = false;
                        objModel.ServiciosAdicionales.Add(objServiciosAdicionales);
                    }
                    else
                    {
                        objServiciosAdicionalesBD.IdServicioAdicionales = objServiciosAdicionales.IdServicioAdicionales;
                        objServiciosAdicionalesBD.IdProducto = objServiciosAdicionales.IdProducto;
                        objServiciosAdicionalesBD.Nombre = objServiciosAdicionales.Nombre;
                        objServiciosAdicionalesBD.Link = objServiciosAdicionales.Link;
                        objServiciosAdicionalesBD.DescripcionEjemplo = objServiciosAdicionales.DescripcionEjemplo;
                        objServiciosAdicionalesBD.FechaActualizacion = DateTimeHelper.PeruDateTime;
                    }

                    objModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarServiciosAdicionales(int idProducto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<ServiciosAdicionales> lstServiciosAdicionales = objModel.ServiciosAdicionales.Where(v => v.IdProducto == idProducto);
                if (lstServiciosAdicionales != null)
                {
                    objModel.ServiciosAdicionales.RemoveRange(lstServiciosAdicionales);
                    objModel.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EliminarServiciosAdicionalesxVarianteProducto(int idProducto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<ServiciosAdicionales> lstServiciosAdicionales = objModel.ServiciosAdicionales.Where(v => v.IdProducto == idProducto);
                foreach (ServiciosAdicionales objServiciosAdicionales in lstServiciosAdicionales)
                {
                    objServiciosAdicionales.VarianteProducto.Clear();
                }
                objModel.ServiciosAdicionales.RemoveRange(lstServiciosAdicionales);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void EliminaServicioAdicional(int IdServicioAdicionales)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                ServiciosAdicionales objServiciosAdicionales = objModel.ServiciosAdicionales.SingleOrDefault(m => m.IdServicioAdicionales == IdServicioAdicionales);
                objServiciosAdicionales.VarianteProducto.Clear();
                objModel.ServiciosAdicionales.Remove(objServiciosAdicionales);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
