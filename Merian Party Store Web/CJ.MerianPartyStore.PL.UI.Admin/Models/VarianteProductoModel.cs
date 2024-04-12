using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using static System.Windows.Forms.LinkLabel;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class VarianteProductoModel
    {
        public int IdVarianteProducto { get; set; }
        public int IdProducto { get; set; }
        public int IdMarca { get; set; }
        public String Producto { get; set; }
        public String Variante { get; set; }
        public String Link { get; set; }
        public double Precio { get; set; }
        public double? PrecioPromocional { get; set; }
        public List<FotoModel> Fotos { get; set; }
        public String FotoPrincipal { get; set; }
        public bool Activo { get; set; }

        public static VarianteProductoModel FromVarianteProducto(VarianteProducto objVarianteProducto)
        {
            try
            {
                VarianteProductoModel objVarianteProductoModel = new VarianteProductoModel();

                objVarianteProductoModel.IdVarianteProducto = objVarianteProducto.IdVarianteProducto;
                objVarianteProductoModel.IdProducto = objVarianteProducto.IdProducto;
 
                objVarianteProductoModel.Producto = objVarianteProducto.Producto.Nombre;
                objVarianteProductoModel.Link= objVarianteProducto.Link;
                objVarianteProductoModel.Activo = objVarianteProducto.Activo;

                if (objVarianteProducto.Variante != null && objVarianteProducto.Variante.Count > 0)
                    objVarianteProductoModel.Variante = String.Join(", ", objVarianteProducto.Variante.Select(v => v.Nombre));

                if (objVarianteProducto.Foto != null && objVarianteProducto.Foto.Count > 0)
                {
                    objVarianteProductoModel.Fotos = new List<FotoModel>();
                    foreach (Foto objFoto in objVarianteProducto.Foto)
                        objVarianteProductoModel.Fotos.Add(FotoModel.FromFoto(objFoto));
                }

                if (objVarianteProductoModel.Fotos != null && objVarianteProductoModel.Fotos.Count > 0)
                    objVarianteProductoModel.FotoPrincipal = objVarianteProductoModel.Fotos[0].Imagen;
                else if (objVarianteProducto.Producto.Foto != null && objVarianteProducto.Producto.Foto.Count > 0)
                    objVarianteProductoModel.FotoPrincipal = Path.Combine(FotoModel.IMAGE_PATH, objVarianteProducto.Producto.Foto.FirstOrDefault().Imagen);

                return objVarianteProductoModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public VarianteProducto ToVarianteProducto()
        {
            try
            {
                VarianteProducto objVarianteProducto = new VarianteProducto();
                objVarianteProducto.IdVarianteProducto = IdVarianteProducto;
                objVarianteProducto.IdProducto = IdProducto;
                objVarianteProducto.Link = Link;
                objVarianteProducto.Activo = Activo;

                return objVarianteProducto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}