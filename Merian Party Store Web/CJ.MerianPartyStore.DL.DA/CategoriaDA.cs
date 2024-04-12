using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class CategoriaDA
    {
        public IQueryable<Categoria> ListarCategoriasPadre()
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Categoria.Where(c => c.IdCategoriaPadre == null);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Categoria.Where(c => c.CategoriaMenu ==true  && c.CategoriaPadre==null);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Categoria.Where(c => c.CategoriaMenu == false && c.IdCategoriaPadre==null);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Categoria.Where(c => objModel.CategoriasDescendientes(IdCategoria).Any(cat => cat.IdCategoria == c.IdCategoria));
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Categoria.SingleOrDefault(c => c.IdCategoria == IdCategoria);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.Categoria.SingleOrDefault(c => c.Url == Url);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Categoria objCategoria = objModel.Categoria.SingleOrDefault(m => m.IdCategoria == IdCategoria);
                objCategoria.Producto.Clear();
                objModel.Categoria.Remove(objCategoria);
                objModel.SaveChanges();
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                if (objCategoria.IdCategoria == 0)
                    objModel.Categoria.Add(objCategoria);
                else
                {
                    Categoria objCategoriaBD = objModel.Categoria.SingleOrDefault(m => m.IdCategoria == objCategoria.IdCategoria);

                    objCategoriaBD.Nombre = objCategoria.Nombre;
                    objCategoriaBD.Descripcion = objCategoria.Descripcion;
                    objCategoriaBD.Url = objCategoria.Url;

                    if (objCategoria.Imagen != null)
                        objCategoriaBD.Imagen = objCategoria.Imagen;

                    if (objCategoria.Banner != null)
                        objCategoriaBD.Banner = objCategoria.Banner;
                }

                objModel.SaveChanges();
                return objCategoria.IdCategoria;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

     
    }
}
