using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class FotoInvitacionDA
    {
        public int[] InsertarFotos(List<FotoInvitacion> lstFoto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();

                List<int> IdsFoto = new List<int>();
                foreach (FotoInvitacion objFoto in lstFoto)
                    objModel.FotoInvitacion.Add(objFoto);

                objModel.SaveChanges();
                return lstFoto.Select(b => b.IdFotoInvitacion).ToArray();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarFotosInvitacionExcepto(int IdInvitacion, int[] IdFotos)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<FotoInvitacion> lstFoto = objModel.FotoInvitacion.Where(b => b.IdInvitacion == IdInvitacion && IdFotos.All(i => i != b.IdFotoInvitacion));


                objModel.FotoInvitacion.RemoveRange(lstFoto);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
