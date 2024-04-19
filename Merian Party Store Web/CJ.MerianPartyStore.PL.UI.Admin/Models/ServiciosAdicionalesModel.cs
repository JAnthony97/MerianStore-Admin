using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class ServiciosAdicionalesModel
    {
        public int IdServiciosAdicionales { get; set; }
        public int IdProducto { get; set; }
        public int? IdVarianteProducto { get; set; }
        public String Nombre { get; set; }
        public String DescripcionEjemplo { get; set; }
        public double? Precio { get; set; }
        public String Link { get; set; }
        public double TipoCambio { get; set; }

        public static ServiciosAdicionalesModel FromServiciosAdicionales(ServiciosAdicionales objServiciosAdicionales)
        {
            try
            {
                ServiciosAdicionalesModel objServiciosAdicionalesModel = new ServiciosAdicionalesModel();

                objServiciosAdicionalesModel.IdServiciosAdicionales = objServiciosAdicionales.IdServicioAdicionales;
                objServiciosAdicionalesModel.IdProducto = objServiciosAdicionales.IdProducto.Value;
                objServiciosAdicionalesModel.IdVarianteProducto = objServiciosAdicionales.IdVarianteProducto;
                objServiciosAdicionalesModel.DescripcionEjemplo = objServiciosAdicionales.DescripcionEjemplo;
                objServiciosAdicionalesModel.Nombre = objServiciosAdicionales.Nombre;
                objServiciosAdicionalesModel.Precio = objServiciosAdicionales.Precio;
                objServiciosAdicionalesModel.Link = objServiciosAdicionales.Link;

                return objServiciosAdicionalesModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public ServiciosAdicionales ToServiciosAdicionales()
        {
            try
            {
                ServiciosAdicionales objServiciosAdicionales = new ServiciosAdicionales();
                objServiciosAdicionales.IdServicioAdicionales = IdServiciosAdicionales;
                objServiciosAdicionales.IdProducto = IdProducto;
                objServiciosAdicionales.IdVarianteProducto = IdVarianteProducto;
                objServiciosAdicionales.Nombre = Nombre;
                objServiciosAdicionales.DescripcionEjemplo = DescripcionEjemplo;
                objServiciosAdicionales.Precio = Precio;
                objServiciosAdicionales.Link = Link;

                return objServiciosAdicionales;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}