using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class ProductoDA
    {
        public IEnumerable<Producto> ListarProductos(String Filtro, String Busqueda)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();

                //Filtro de búsqueda
                String[] Palabras = { };
                if (!String.IsNullOrWhiteSpace(Busqueda))
                    Palabras = Busqueda.Split(' ');
                else
                    Busqueda = null;

                IEnumerable<Producto> lstProducto = objModel.Producto.Where(p =>
                    (Filtro == null) &&
                    (Busqueda == null || p.Nombre.StartsWith(Busqueda))).OrderBy(p => p.Nombre);

                if (Busqueda != null)
                    lstProducto = lstProducto.Union(objModel.Producto.Where(p =>
                        (Filtro == null) &&
                        //(Palabras.All(w => p.Nombre.Contains(w)) || p.ServiciosAdicionales.Any(sp => sp.Nombre.StartsWith(Busqueda)) || p.Categoria.Any(c => c.Nombre.Contains(Busqueda))))).OrderBy(p => p.Nombre);
                        (Palabras.All(w => p.Nombre.Contains(w)) || p.ServiciosAdicionales.Any(sp => sp.Nombre.StartsWith(Busqueda)) || p.Categoria.Any(c => c.Nombre.Contains(Busqueda))))).OrderBy(p => p.IdProducto);
                return lstProducto.Distinct();
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Producto.Where(c => c.Recomendado == true && c.Activo == true);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        //public IQueryable<Producto> ListarProductosCategoria(int IdCategoria)
        //{
        //    try
        //    {
        //        DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
        //        IQueryable<Producto> lstProducto = objModel.Producto.Where(p => p.id == IdCategoria);
        //        return lstProducto;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}
        public Producto ObtenerProducto(int IdProducto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Producto.SingleOrDefault(m => m.IdProducto == IdProducto);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Producto.FirstOrDefault(p => p.Url == Url);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Producto.FirstOrDefault(p => p.Recomendado.Value && p.Recomendado.Value && p.Activo == true);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                DateTime Hace30Dias = DateTimeHelper.PeruDateTime.AddDays(-30);
                return objModel.Producto.Where(p => p.Activo == true && p.FechaRegistro >= Hace30Dias).OrderByDescending(p => p.IdProducto).FirstOrDefault();
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Producto objProducto = objModel.Producto.SingleOrDefault(m => m.IdProducto == IdProducto);
                objProducto.Categoria.Clear();
                objModel.Producto.Remove(objProducto);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void CambiarEstadoProducto(int IdProducto, bool Activo)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Producto objProducto = objModel.Producto.SingleOrDefault(u => u.IdProducto == IdProducto);
                objProducto.Activo = Activo;

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public void MarcarProductoRecomendado(int IdProducto, bool Recomendado)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Producto objProducto = objModel.Producto.SingleOrDefault(u => u.IdProducto == IdProducto);
                objProducto.Recomendado = Recomendado;
                objProducto.FechaActualizacion = DateTimeHelper.PeruDateTime;
                //Desmarcar todos los demás
                //if (Recomendado)
                //{
                //    IQueryable<Producto> lstProductoOtro = objModel.Producto.Where(p => p.IdProducto != IdProducto && p.Recomendado==true);
                //    foreach (Producto objProductoOtro in lstProductoOtro)
                //        objProductoOtro.Recomendado = false;
                //}

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public int GuardarProducto(Producto objProducto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                if (objProducto.IdProducto == 0)
                {
                    objProducto.Activo = true;
                    objProducto.Recomendado = false;
                    objProducto.FechaRegistro = DateTimeHelper.PeruDateTime;
                    objModel.Producto.Add(objProducto);
                }
                else
                {
                    Producto objProductoBD = objModel.Producto.SingleOrDefault(m => m.IdProducto == objProducto.IdProducto);

        
                    objProductoBD.Nombre = objProducto.Nombre;
                    objProductoBD.Url = objProducto.Url;
                    objProductoBD.Descripcion = objProducto.Descripcion;
                    objProductoBD.Precio = objProducto.Precio;
                    objProductoBD.PrecioPromocional = objProducto.PrecioPromocional;
                    objProductoBD.VideoUrl = objProducto.VideoUrl;
                    objProductoBD.FechaActualizacion = DateTimeHelper.PeruDateTime;
                }

                objModel.SaveChanges();
                return objProducto.IdProducto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void GuardarCategoriasxProducto(int IdProducto, int[] IdCategorias)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Producto objProducto = objModel.Producto.SingleOrDefault(p => p.IdProducto == IdProducto);

                objProducto.Categoria.Clear();
                foreach (int IdCategoria in IdCategorias)
                {
                    Categoria objCategoria = objModel.Categoria.SingleOrDefault(c => c.IdCategoria == IdCategoria);
                    objProducto.Categoria.Add(objCategoria);
                }

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
