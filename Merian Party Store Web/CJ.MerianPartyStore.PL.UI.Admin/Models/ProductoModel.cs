using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class ProductoModel
    {
        public int IdProducto { get; set; }
        public int IdCategoria { get; set; }
        public String Marca { get; set; }
        public String Nombre { get; set; }
        public double? Precio { get; set; }
        public double? PrecioPromocional { get; set; }
        public double PrecioFinal
        {
            get
            {
                if (PrecioPromocional == null)
                    return Precio.Value;

                return PrecioPromocional.Value;
            }
        }
        public String Keywords { get; set; }
        public List<FotoModel> Fotos { get; set; }
        public String FotoPrincipal { get; set; }
        public String Url { get; set; }
        public String VideoUrl { get; set; }
        public String Descripcion { get; set; }
        public String DescripcionServiciosAdicionales { get; set; }
        public String DescripcionComoComprar { get; set; }
        public double? Rating { get; set; }
        public int[] CantidadReviews { get; set; }
        public int CantidadReviewsAprobados { get; set; }
        public int CantidadReviewsPendientes { get; set; }
        public List<CategoriaModel> Categorias { get; set; }
        public List<TipoVarianteModel> TiposVariante { get; set; }
        public List<VarianteProductoModel> VariantesProducto { get; set; }
        public List<ServiciosAdicionalesModel> ServiciosAdicionales { get; set; }
        public String Variante { get; set; }
        public String SKU { get; set; }
        public int IdVarianteProducto { get; set; }
        public bool? Recomendado { get; set; }
        public bool Liquidacion { get; set; }
        public bool? Activo { get; set; }
        public double TipoCambio { get; set; }
        public static ProductoModel FromProducto(Producto objProducto, bool Fotos, bool Categorias, bool Descripcion, bool TipoVariante, bool VariantesProducto, bool Reviews, String EstadoReviews)
        {
            try
            {
                ProductoModel objProductoModel = new ProductoModel();

                objProductoModel.IdProducto = objProducto.IdProducto;
                objProductoModel.Nombre = objProducto.Nombre;
                objProductoModel.Precio = objProducto.Precio;
                objProductoModel.PrecioPromocional = objProducto.PrecioPromocional;

                if (objProducto.Foto != null && Fotos)
                {
                    objProductoModel.Fotos = new List<FotoModel>();
                    foreach (Foto objFoto in objProducto.Foto)
                        objProductoModel.Fotos.Add(FotoModel.FromFoto(objFoto));

                    if (objProductoModel.Fotos.Count > 0)
                        objProductoModel.FotoPrincipal = objProductoModel.Fotos[0].Imagen;
                }
                objProductoModel.Url = objProducto.Url;
                objProductoModel.VideoUrl = objProducto.VideoUrl;

                if (Descripcion)
                    objProductoModel.Descripcion = objProducto.Descripcion;

                objProductoModel.DescripcionServiciosAdicionales = objProducto.DescripcionServiciosAdicionales;
                objProductoModel.DescripcionComoComprar = objProducto.ComoComprar;
                if (Categorias)
                {
                    objProductoModel.Categorias = new List<CategoriaModel>();
                    foreach (Categoria objCategoria in objProducto.Categoria)
                        objProductoModel.Categorias.Add(CategoriaModel.FromCategoria(objCategoria, false));
                }

                objProductoModel.ServiciosAdicionales = new List<ServiciosAdicionalesModel>();
                foreach (ServiciosAdicionales objServiciosAdicionales in objProducto.ServiciosAdicionales)
                    objProductoModel.ServiciosAdicionales.Add(ServiciosAdicionalesModel.FromServiciosAdicionales(objServiciosAdicionales));

                if (TipoVariante && objProducto.TipoVariante != null)
                {
                    objProductoModel.TiposVariante = new List<TipoVarianteModel>();
                    foreach (TipoVariante objTipoVariante in objProducto.TipoVariante)
                        objProductoModel.TiposVariante.Add(TipoVarianteModel.FromTipoVariante(objTipoVariante));
                }

                if (VariantesProducto && objProducto.VarianteProducto != null)
                {
                    objProductoModel.VariantesProducto = new List<VarianteProductoModel>();
                    foreach (VarianteProducto objVarianteProducto in objProducto.VarianteProducto)
                        objProductoModel.VariantesProducto.Add(VarianteProductoModel.FromVarianteProducto(objVarianteProducto));
                }

                objProductoModel.Recomendado = objProducto.Recomendado;
                objProductoModel.Activo = objProducto.Activo;

                return objProductoModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Producto ToProducto()
        {
            try
            {
                Producto objProducto = new Producto();
                objProducto.IdProducto = IdProducto;
                objProducto.Nombre = Nombre;
                objProducto.Url = Url;
                objProducto.Precio = Precio;
                objProducto.PrecioPromocional = PrecioPromocional;

                objProducto.VideoUrl = VideoUrl;
                objProducto.Descripcion = Descripcion;
                objProducto.DescripcionServiciosAdicionales = DescripcionServiciosAdicionales;
                objProducto.ComoComprar = DescripcionComoComprar;
                objProducto.Recomendado = Recomendado;


                return objProducto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}