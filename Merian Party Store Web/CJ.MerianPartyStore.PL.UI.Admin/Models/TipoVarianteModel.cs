using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class TipoVarianteModel
    {
        public int IdTipoVariante { get; set; }
        public int IdProducto { get; set; }
        public String Nombre { get; set; }
        public String Tipo { get; set; }
        public String Variantes { get; set; }

        public static TipoVarianteModel FromTipoVariante(TipoVariante objTipoVariante)
        {
            try
            {
                TipoVarianteModel objTipoVarianteModel = new TipoVarianteModel();

                objTipoVarianteModel.IdTipoVariante = objTipoVariante.IdTipoVariante;
                objTipoVarianteModel.IdProducto = objTipoVariante.IdProducto;
                objTipoVarianteModel.Nombre = objTipoVariante.Nombre;
                objTipoVarianteModel.Tipo = objTipoVariante.Tipo;
                objTipoVarianteModel.Variantes = "";

                for (int i = 0; i < objTipoVariante.Variante.Count; i++)
                {
                    if (i > 0)
                        objTipoVarianteModel.Variantes += ",";

                    Variante objVariante = objTipoVariante.Variante.ElementAt(i);
                    if (objTipoVariante.Tipo == Constants.Producto.Variante.Tipo.COLOR)
                        objTipoVarianteModel.Variantes += "[" + objVariante.Color + "]";

                    objTipoVarianteModel.Variantes += objVariante.Nombre;
                }

                return objTipoVarianteModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TipoVariante ToTipoVariante()
        {
            try
            {
                TipoVariante objTipoVariante = new TipoVariante();
                objTipoVariante.IdTipoVariante = IdTipoVariante;
                objTipoVariante.IdProducto = IdProducto;
                objTipoVariante.Nombre = Nombre;

                return objTipoVariante;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}