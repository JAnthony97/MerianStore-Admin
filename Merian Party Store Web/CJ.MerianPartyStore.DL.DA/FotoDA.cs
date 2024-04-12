using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class FotoDA
    {
        public int[] InsertarFotos(List<Foto> lstFoto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();

                List<int> IdsFoto = new List<int>();
                foreach (Foto objFoto in lstFoto)
                    objModel.Foto.Add(objFoto);

                objModel.SaveChanges();
                return lstFoto.Select(b => b.IdFoto).ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarFotosProductoExcepto(int IdProducto, int[] IdFotos)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<Foto> lstFoto = objModel.Foto.Where(b => b.IdProducto == IdProducto && IdFotos.All(i => i != b.IdFoto));

                foreach (Foto objFoto in lstFoto)
                    objFoto.VarianteProducto.Clear();

                objModel.Foto.RemoveRange(lstFoto);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
