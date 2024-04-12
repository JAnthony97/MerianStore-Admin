using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class VarianteDA
    {
        public void GuardarVariantes(List<Variante> lstVariante)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                foreach (Variante objVariante in lstVariante)
                {
                    Variante objVarianteBD = objModel.Variante.FirstOrDefault(m => m.IdTipoVariante == objVariante.IdTipoVariante && m.Nombre.ToUpper() == objVariante.Nombre.ToUpper());
                    if (objVarianteBD == null)
                        objModel.Variante.Add(objVariante);
                    else
                    {
                        objVarianteBD.IdTipoVariante = objVariante.IdTipoVariante;
                        objVarianteBD.Nombre = objVariante.Nombre;
                        objVarianteBD.Color = objVariante.Color;
                    }
                }

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarVariantesTipoVarianteExcepto(int IdTipoVariante, String[] NombreVariantes)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<Variante> lstVariante = objModel.Variante.Where(b => b.IdTipoVariante == IdTipoVariante && NombreVariantes.All(v => v.ToUpper() != b.Nombre.ToUpper()));

                foreach (Variante objVariante in lstVariante)
                    objVariante.VarianteProducto.Clear();

                objModel.Variante.RemoveRange(lstVariante);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
