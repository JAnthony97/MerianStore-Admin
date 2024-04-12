using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class TipoVarianteDA
    {
        public IQueryable<TipoVariante> ListarTiposVarianteProductoExcepto(int IdProducto, int[] IdTiposVariante)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<TipoVariante> lstTipoVariante = objModel.TipoVariante.Where(b => b.IdProducto == IdProducto && IdTiposVariante.All(i => i != b.IdTipoVariante));
                return lstTipoVariante;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GuardarTipoVariante(TipoVariante objTipoVariante)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                if (objTipoVariante.IdTipoVariante == 0)
                    objModel.TipoVariante.Add(objTipoVariante);
                else
                {
                    TipoVariante objTipoVarianteBD = objModel.TipoVariante.SingleOrDefault(m => m.IdTipoVariante == objTipoVariante.IdTipoVariante);

                    objTipoVarianteBD.IdProducto = objTipoVariante.IdProducto;
                    objTipoVarianteBD.Nombre = objTipoVariante.Nombre;
                    objTipoVarianteBD.Tipo = objTipoVariante.Tipo;
                }

                objModel.SaveChanges();
                return objTipoVariante.IdTipoVariante;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarTiposVarianteProductoExcepto(int IdProducto, int[] IdTiposVariante)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<TipoVariante> lstTipoVariante = objModel.TipoVariante.Where(b => b.IdProducto == IdProducto && IdTiposVariante.All(i => i != b.IdTipoVariante));
                objModel.TipoVariante.RemoveRange(lstTipoVariante);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
