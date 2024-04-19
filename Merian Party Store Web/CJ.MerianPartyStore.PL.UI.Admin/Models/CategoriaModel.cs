using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class CategoriaModel
    {
        public int IdCategoria { get; set; }
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public String Url { get; set; }
        public String Imagen { get; set; }
        public String Banner { get; set; }
        public String FechaRegistro { get; set; }
        public List<CategoriaModel> Subcategorias { get; set; }
        public int? IdCategoriaPadre { get; set; }
        public bool Destacado { get; set; }

        public const String IMAGE_PATH = "~/Images/Categorias/";
        public const String BANNER_CATEGORIA_PATH = "~/Images/Categorias/Banner";

        public static CategoriaModel FromCategoria(Categoria objCategoria, bool Subcategorias)
        {
            try
            {
                CategoriaModel objCategoriaModel = new CategoriaModel();

                objCategoriaModel.IdCategoria = objCategoria.IdCategoria;
                objCategoriaModel.Nombre = objCategoria.Nombre;
                objCategoriaModel.Descripcion = objCategoria.Descripcion;
                objCategoriaModel.Url = objCategoria.Url;

                if (objCategoria.Imagen != null)
                    objCategoriaModel.Imagen = Path.Combine(IMAGE_PATH, objCategoria.Imagen);

                if (objCategoria.Banner != null)
                    objCategoriaModel.Banner = Path.Combine(BANNER_CATEGORIA_PATH, objCategoria.Banner);

                objCategoriaModel.IdCategoriaPadre = objCategoria.IdCategoriaPadre;


                if (Subcategorias)
                {
                    objCategoriaModel.Subcategorias = new List<CategoriaModel>();
                    foreach (Categoria objSubcategoria in objCategoria.SubCategoria)
                        objCategoriaModel.Subcategorias.Add(CategoriaModel.FromCategoria(objSubcategoria, Subcategorias));
                }

                return objCategoriaModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Categoria ToCategoria()
        {
            try
            {
                Categoria objCategoria = new Categoria();
                objCategoria.IdCategoria = IdCategoria;
                objCategoria.Nombre = Nombre;
                objCategoria.Descripcion = Descripcion;
                objCategoria.Url = Url;

                if (Imagen != null)
                    objCategoria.Imagen = Imagen.Replace(IMAGE_PATH, "");

                if (Banner != null)
                    objCategoria.Banner = Banner.Replace(BANNER_CATEGORIA_PATH, "");

                objCategoria.IdCategoriaPadre = IdCategoriaPadre;

                return objCategoria;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}