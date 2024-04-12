using CJ.MerianPartyStore.BL.BE;
using CJ.MerianPartyStore.DL.DA;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CJ.MerianPartyStore.Util.Constants.Producto;

namespace CJ.MerianPartyStore.BL.BC
{
    public class CatalogoBC
    {
        #region PRODUCTOS
        //public IEnumerable<ItemCatalogo> ListarItemsCatalogo(String Busqueda, String[] Categorias, String UrlCategoria)
        //{
        //    try
        //    {
        //        return new ItemCatalogoDA().ListarItemsCatalogo(Busqueda, Categorias, UrlCategoria);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public IEnumerable<Producto> ListarProductos(String Filtro, String Busqueda)
        {
            try
            {
                return new ProductoDA().ListarProductos(Filtro, Busqueda);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<Producto> ListarProductoDestacado()
        {
            try
            {

                return new ProductoDA().ListarProductoDestacado();
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public Producto ObtenerProducto(int IdProducto)
        {
            try
            {
                return new ProductoDA().ObtenerProducto(IdProducto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Producto ObtenerProducto(String Url)
        {
            try
            {
                return new ProductoDA().ObtenerProducto(Url);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Producto ObtenerProductoDestacado()
        {
            try
            {
                return new ProductoDA().ObtenerProductoDestacado();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Producto ObtenerProductoNuevo()
        {
            try
            {
                return new ProductoDA().ObtenerProductoNuevo();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GuardarProducto(Producto objProducto, List<Foto> lstFotoNuevo, int[] IdFotosMantener, int[] IdCategorias, List<VarianteProducto> lstVarianteProducto, List<int[]> lstIndiceFoto, List<ServiciosAdicionales> lstServicioAdicional)
        {
            try
            {
                ProductoDA objProductoDA = new ProductoDA();

                bool Nuevo = objProducto.IdProducto == 0;

                //Guardar producto
                objProducto.IdProducto = objProductoDA.GuardarProducto(objProducto);

                //Eliminar fotos borradas por el usuario
                if (IdFotosMantener != null)
                    new FotoDA().EliminarFotosProductoExcepto(objProducto.IdProducto, IdFotosMantener);

                //Insertar fotos nuevas
                if (lstFotoNuevo != null)
                {
                    foreach (Foto objFoto in lstFotoNuevo)
                        objFoto.IdProducto = objProducto.IdProducto;

                    new FotoDA().InsertarFotos(lstFotoNuevo);
                }
                //Insertar servicios adicionales
                if (lstServicioAdicional.Count > 0)
                {
                    Producto objProductoServicio = objProductoDA.ObtenerProducto(objProducto.IdProducto);
                    if (objProductoServicio.ServiciosAdicionales.Count > 0 && lstServicioAdicional.All(sd => sd.IdServicioAdicionales != 0))
                    {
                        int[] lstservicios = lstServicioAdicional.Where(sd => sd.IdServicioAdicionales != 0).Select(sp => sp.IdServicioAdicionales).ToArray();
                        int[] lstserviciosbd = objProductoServicio.ServiciosAdicionales.Select(sp => sp.IdServicioAdicionales).ToArray();
                        if (lstserviciosbd.Length != lstservicios.Length)
                        {
                            List<int> lstservicioseliminados = lstserviciosbd.Except(lstservicios).ToList();
                            foreach (int idServicioAdicional in lstservicioseliminados)
                                new ServiciosAdicionalesDA().EliminaServicioAdicional(idServicioAdicional);
                        }
                    }
                    new ServiciosAdicionalesDA().GuardarServiciosAdicionales(lstServicioAdicional, objProducto.IdProducto);
                }

                //Asignar categorías
                if (IdCategorias != null)
                    new ProductoDA().GuardarCategoriasxProducto(objProducto.IdProducto, IdCategorias);

                //Generar primera variante si el producto es nuevo
                if (Nuevo)
                {

                    VarianteProducto objVarianteProducto = lstVarianteProducto[0];
                    objVarianteProducto.IdProducto = objProducto.IdProducto;
                    objVarianteProducto.Precio = objProducto.Precio;
                    objVarianteProducto.PrecioPromocional = objProducto.PrecioPromocional;

                    new VarianteProductoDA().GuardarVariantesProducto(objProducto.IdProducto, lstVarianteProducto, lstIndiceFoto);

                }
                else if (lstVarianteProducto != null)
                {
                    new VarianteProductoDA().GuardarVariantesProducto(objProducto.IdProducto, lstVarianteProducto, lstIndiceFoto);
                }

                return objProducto.IdProducto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarProducto(int IdProducto)
        {
            try
            {
                new ServiciosAdicionalesDA().EliminarServiciosAdicionalesxVarianteProducto(IdProducto);
                new VarianteProductoDA().EliminarVariantesProducto(IdProducto);
                EliminarTiposVarianteProductoExcepto(IdProducto, new int[] { });
                new FotoDA().EliminarFotosProductoExcepto(IdProducto, new int[] { });
                new ProductoDA().EliminarProducto(IdProducto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CambiarEstadoProducto(int IdProducto, bool Activo)
        {
            try
            {
                ProductoDA objProductoDA = new ProductoDA();
                objProductoDA.CambiarEstadoProducto(IdProducto, Activo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void MarcarProductoRecomendado(int IdProducto, bool Destacado)
        {
            try
            {
                ProductoDA objProductoDA = new ProductoDA();
                objProductoDA.MarcarProductoRecomendado(IdProducto, Destacado);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
        #region TIPO VARIANTE
        public int GuardarTipoVariante(TipoVariante objTipoVariante, List<DL.DM.Variante> lstVariante)
        {
            try
            {
                TipoVarianteDA objTipoVarianteDA = new TipoVarianteDA();

                //Guardar TipoVariante
                objTipoVariante.IdTipoVariante = objTipoVarianteDA.GuardarTipoVariante(objTipoVariante);

                //Eliminar variantes borradas por el usuario
                if (lstVariante != null)
                {
                    new VarianteDA().EliminarVariantesTipoVarianteExcepto(objTipoVariante.IdTipoVariante, lstVariante.Select(v => v.Nombre).ToArray());

                    foreach (DL.DM.Variante objVariante in lstVariante)
                        objVariante.IdTipoVariante = objTipoVariante.IdTipoVariante;

                    new VarianteDA().GuardarVariantes(lstVariante);
                }

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
                TipoVarianteDA objTipoVarianteDA = new TipoVarianteDA();
                IQueryable<TipoVariante> lstTipoVariante = objTipoVarianteDA.ListarTiposVarianteProductoExcepto(IdProducto, IdTiposVariante);

                foreach (int IdTipoVariante in lstTipoVariante.Select(t => t.IdTipoVariante))
                    new VarianteDA().EliminarVariantesTipoVarianteExcepto(IdTipoVariante, new String[] { });

                new TipoVarianteDA().EliminarTiposVarianteProductoExcepto(IdProducto, IdTiposVariante);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
        #region VARIANTE PRODUCTO
        public IQueryable<VarianteProducto> ListarVariantesProducto(int? IdMarca, String Busqueda, ValueContainer TotalPages, int? Page, int? PageSize)
        {
            try
            {
                return new VarianteProductoDA().ListarVariantesProducto(IdMarca, Busqueda, TotalPages, Page, PageSize);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<VarianteProducto> ObtenerVariantesProducto(int[] IdVariantesProducto)
        {
            try
            {
                return new VarianteProductoDA().ObtenerVariantesProducto(IdVariantesProducto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public VarianteProducto ObtenerVarianteProducto(int[] IdVariantes)
        {
            try
            {
                return new VarianteProductoDA().ObtenerVarianteProducto(IdVariantes);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public VarianteProducto ObtenerVarianteProducto(int IdVarianteProducto)
        {
            try
            {
                return new VarianteProductoDA().ObtenerVarianteProducto(IdVarianteProducto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarVariantesProducto(int IdProducto)
        {
            try
            {
                new VarianteProductoDA().EliminarVariantesProducto(IdProducto);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void GenerarVariantesProducto(int IdProducto)
        {
            try
            {
                new VarianteProductoDA().GenerarVariantesProducto(IdProducto);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CambiarEstadoVarianteProducto(int IdVarianteProducto, bool Activo)
        {
            try
            {
                VarianteProductoDA objVarianteProductoDA = new VarianteProductoDA();
                objVarianteProductoDA.CambiarEstadoVarianteProducto(IdVarianteProducto, Activo);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion

        #region CATEGORÍAS
        public IQueryable<Categoria> ListarCategoriasPadre()
        {
            try
            {
                return new CategoriaDA().ListarCategoriasPadre();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Categoria> ListarCategoriasPrincipales()
        {
            try
            {
                return new CategoriaDA().ListarCategoriasPrincipales();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IQueryable<Categoria> ListarCategoriasNoMostradas()
        {
            try
            {
                return new CategoriaDA().ListarCategoriasNoMostradas();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Categoria> ListarCategoriasDescendientes(int IdCategoria)
        {
            try
            {
                return new CategoriaDA().ListarCategoriasDescendientes(IdCategoria);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Categoria ObtenerCategoria(int IdCategoria)
        {
            try
            {
                return new CategoriaDA().ObtenerCategoria(IdCategoria);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Categoria ObtenerCategoria(String Url)
        {
            try
            {
                return new CategoriaDA().ObtenerCategoria(Url);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int GuardarCategoria(Categoria objCategoria)
        {
            try
            {
                CategoriaDA objCategoriaDA = new CategoriaDA();

                //Guardar categoria
                objCategoria.IdCategoria = objCategoriaDA.GuardarCategoria(objCategoria);

                return objCategoria.IdCategoria;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void EliminarCategoria(int IdCategoria)
        {
            try
            {
                CategoriaDA objCategoriaDA = new CategoriaDA();
                Categoria objCategoria = objCategoriaDA.ObtenerCategoria(IdCategoria);

                foreach (Categoria objSubcategoria in objCategoria.SubCategoria)
                    EliminarCategoria(objSubcategoria.IdCategoria);


                objCategoriaDA.EliminarCategoria(IdCategoria);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #endregion
    }
}
