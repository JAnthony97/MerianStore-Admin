using CJ.MerianPartyStore.BL.BC;
using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.PL.UI.Admin.Models;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace CJ.MerianPartyStore.PL.UI.Admin.Controllers
{
    [Authorize]
    public class CatalogoController : BaseController
    {
        // GET: Catalogo
        public const int ERROR_INCOMPLETE_DATA = 1;
        public const int ERROR_DATA_VALIDATION = 2;
        public const int ERROR_REPEATED_URL = 3;
        public const int ERROR_REPEATED_SKU = 4;

        private const int PAGE_SIZE = 20;
        public ActionResult Index(String Filtro, String Busqueda, int? Page)
        {
            try
            {
                ConfiguracionBC objConfiguracionBC = new ConfiguracionBC();
                CatalogoBC objCatalogoBC = new CatalogoBC();

                //Filtro "Todos" por default
                if (String.IsNullOrWhiteSpace(Filtro))
                    Filtro = Constants.Producto.Filtro.TODOS;

                //Página 1 por default
                if (Page == null)
                    Page = 1;
                int TotalPages = 1;

                //Lista los productos
                IEnumerable<Producto> lstProducto = objCatalogoBC.ListarProductos(Filtro, Busqueda);

                //Paginación
                TotalPages = (int)Math.Ceiling(lstProducto.Count() / (double)PAGE_SIZE);
                lstProducto = lstProducto.OrderBy(u => u.Nombre).ThenBy(u => u.Nombre).Skip((Page.Value - 1) * PAGE_SIZE).Take(PAGE_SIZE);

                List<ProductoModel> lstProductoModel = new List<ProductoModel>();
                foreach (Producto objProducto in lstProducto)
                    lstProductoModel.Add(ProductoModel.FromProducto(objProducto, true, true, false, false, false, true, null));
                
                //Obtener tipo de cambio
                ViewBag.TipoCambio = Double.Parse(objConfiguracionBC.ObtenerConfiguracion(Constants.Configuracion.TIPO_CAMBIO).Valor ?? "0.00"); 

                //Envía el filtro para que muestre dicho tab activo
                ViewBag.Filtro = Filtro;

                //Envía la paginación
                ViewBag.Page = Page;
                ViewBag.TotalPages = TotalPages;

                return View(lstProductoModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al obtener la lista de productos.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }
        private void LoadProductoData()
        {
            try
            {
                ConfiguracionBC objConfiguracionBC=new ConfiguracionBC();

                //Lista de categorias registradas
                IQueryable<Categoria> lstCategoria = new CatalogoBC().ListarCategoriasPadre();
                List<CategoriaModel> lstCategoriaModel = new List<CategoriaModel>();
                foreach (Categoria objCategoria in lstCategoria)
                    lstCategoriaModel.Add(CategoriaModel.FromCategoria(objCategoria, true));
                ViewBag.Categorias = lstCategoriaModel;


                //Obtener tipo de cambio
                ViewBag.TipoCambio = Double.Parse(objConfiguracionBC.ObtenerConfiguracion(Constants.Configuracion.TIPO_CAMBIO).Valor ?? "0.00");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #region CATEGORIAS
        public ActionResult Categorias()
        {
            try
            {
                CatalogoBC objCatalogoBC = new CatalogoBC();
                IQueryable<Categoria> lstCategoria = objCatalogoBC.ListarCategoriasPadre();
                List<CategoriaModel> lstCategoriaModel = new List<CategoriaModel>();

                foreach (Categoria objCategoria in lstCategoria)
                    lstCategoriaModel.Add(CategoriaModel.FromCategoria(objCategoria, true));

                return View(lstCategoriaModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al cargar las categorías.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }
        public ActionResult GuardarCategoria(CategoriaModel objCategoriaModel, HttpPostedFileBase FotoFile, HttpPostedFileBase BannerFile)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                //Validación de datos generales de categoría
                if (!String.IsNullOrWhiteSpace(objCategoriaModel.Nombre) )
                {
                    CatalogoBC objCatalogoBC = new CatalogoBC();
                    Categoria objCategoria = objCatalogoBC.ObtenerCategoria(objCategoriaModel.IdCategoria);

                    objCategoriaModel.Url = StringHelper.ToUrl(objCategoriaModel.Nombre, 100);
                    //Categoria objCategoriaConUrl = objCatalogoBC.ObtenerCategoria(objCategoriaModel.Url);

                    ////Validación de URL existente
                    //if (objCategoriaConUrl == null || objCategoriaModel.IdCategoria == objCategoriaConUrl.IdCategoria)
                    //{
                        //Guardado de imagen
                        if (FotoFile != null)
                        {
                            //Elimina la imagen anterior
                            if (objCategoria != null && !String.IsNullOrWhiteSpace(objCategoria.Imagen))
                                new FileInfo(Path.Combine(Server.MapPath(CategoriaModel.IMAGE_PATH), objCategoria.Imagen)).Delete();

                            //Guarda la nueva imagen
                            objCategoriaModel.Imagen = Guid.NewGuid().ToString() + Path.GetExtension(FotoFile.FileName);
                            Directory.CreateDirectory(Server.MapPath(CategoriaModel.IMAGE_PATH));
                            FotoFile.SaveAs(Path.Combine(Server.MapPath(CategoriaModel.IMAGE_PATH), objCategoriaModel.Imagen));
                        }
                        //Guardado de banner
                        if (BannerFile != null)
                        {
                            Directory.CreateDirectory(Server.MapPath(CategoriaModel.IMAGE_PATH));
                            Directory.CreateDirectory(Server.MapPath(CategoriaModel.BANNER_CATEGORIA_PATH));
                            //Elimina la imagen banner anterior
                            if (objCategoria != null && !String.IsNullOrWhiteSpace(objCategoria.Banner))
                                new FileInfo(Path.Combine(Server.MapPath(CategoriaModel.BANNER_CATEGORIA_PATH), objCategoria.Banner)).Delete();

                            //Guarda la nueva imagen banner
                            objCategoriaModel.Banner = Guid.NewGuid().ToString() + Path.GetExtension(BannerFile.FileName);
                            Directory.CreateDirectory(Server.MapPath(CategoriaModel.IMAGE_PATH));
                            BannerFile.SaveAs(Path.Combine(Server.MapPath(CategoriaModel.BANNER_CATEGORIA_PATH), objCategoriaModel.Banner));
                        }
                        objCategoria = objCategoriaModel.ToCategoria();

                        //Guarda la categoría
                        objCategoria.IdCategoria = objCatalogoBC.GuardarCategoria(objCategoria);

                        objResultObject.Code = 0;
                        objResultObject.Message = "¡La categoría se ha guardado con éxito!";
                    //}
                    //else
                    //{
                    //    objResultObject.Code = ERROR_REPEATED_URL;
                    //    objResultObject.Message = "Ya existe una categoría con dicho nombre. Por favor, elige un nombre distinto.";
                    //}
                }
                else
                {
                    objResultObject.Code = ERROR_INCOMPLETE_DATA;
                    objResultObject.Message = "Debes completar todos los campos requeridos.";
                }
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al guardar los datos de la categoría. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }
        public ActionResult EliminarCategoria(int IdCategoria)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                CatalogoBC objCatalogoBC = new CatalogoBC();
                Categoria objCategoria = objCatalogoBC.ObtenerCategoria(IdCategoria);

                //Elimina la categoría
                objCatalogoBC.EliminarCategoria(IdCategoria);

                //Elimina su imagen
                if (!String.IsNullOrWhiteSpace(objCategoria.Imagen))
                    new FileInfo(Path.Combine(Server.MapPath(CategoriaModel.IMAGE_PATH), objCategoria.Imagen)).Delete();

                objResultObject.Code = 0;
                objResultObject.Message = "¡La categoría se ha eliminado con éxito!";
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al eliminar la categoría. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }
        public ActionResult DetalleCategoria(int? IdCategoria, int? IdCategoriaPadre)
        {

            try
            {
                CategoriaModel objCategoriaModel = new CategoriaModel();
                CatalogoBC objCatalogoBC = new CatalogoBC();

                if (IdCategoriaPadre != null)
                    objCategoriaModel.IdCategoriaPadre = IdCategoriaPadre;
                if (IdCategoria != null)
                {
                    Categoria objCategoria = objCatalogoBC.ObtenerCategoria(IdCategoria.Value);
                    objCategoriaModel = CategoriaModel.FromCategoria(objCategoria, true);
                }
                return View(objCategoriaModel);
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al cargar las categorías.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }
        public ActionResult NuevaCategoria()
        {

            return View("DetalleCategoria");
        }
        #endregion

        [PermissionsFilter(new String[] { Constants.Usuario.Perfil.ADMINISTRADOR })]
        public ActionResult NuevoProducto()
        {
            try
            {
                LoadProductoData();
                return View("Producto");
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Ocurrió un error al cargar la interfaz de nuevo producto.";
                ViewBag.exMessage = ex.Message;
                ViewBag.exStackTrace = ex.StackTrace;
                return View("Error");
            }
        }

        public ActionResult Producto(int id)
        {
            CatalogoBC objCatalogoBC = new CatalogoBC();
            Producto objProducto = objCatalogoBC.ObtenerProducto(id);

            LoadProductoData();
            return View(ProductoModel.FromProducto(objProducto, true, true, true, true, true, true, null));
        }

        public ActionResult Foto(FotoModel objFotoModel)
        {
            return PartialView(objFotoModel);
        }

        [PermissionsFilter(new String[] { Constants.Usuario.Perfil.ADMINISTRADOR })]

        public ActionResult GuardarProducto(ProductoModel objProductoModel, HttpPostedFileBase[] FotoFile, int[] IdFotos, int[] IdCategorias, int[] IdVariantesProducto, String[] IndiceFotosSeleccionadas, String[] URLYouTube, int[] IdServiciosAdicionales, String[] NombreServicioAdicional, String[] LinkServicioAdicional, double[] PrecioServicioAdicional, string[] DescripcionEjemplo)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                //Validación de datos generales de producto
                if (
                    !String.IsNullOrWhiteSpace(objProductoModel.Nombre) &&
                    objProductoModel.Precio > 0)
                {



                    if ((objProductoModel.PrecioPromocional == null || objProductoModel.PrecioPromocional < objProductoModel.Precio))
                    {
                        CatalogoBC objCatalogoBC = new CatalogoBC();
                        Producto objProducto = objCatalogoBC.ObtenerProducto(objProductoModel.IdProducto);

                        objProductoModel.Url = StringHelper.ToUrl(objProductoModel.Nombre, 100);
                        Producto objProductoConUrl = objCatalogoBC.ObtenerProducto(objProductoModel.Url);

                        //Validación de URL existente
                        if (objProductoConUrl == null || objProductoModel.IdProducto == objProductoConUrl.IdProducto)
                        {
                            //IEnumerable<String> SKUsUnicos = SKUs.Distinct();
                            //if (SKUsUnicos.Count() == SKUs.Length)
                            //{

                            //Elimina las imagenes anteriores
                            if (objProducto != null)
                            {
                                if (IdFotos == null)
                                    IdFotos = new int[] { };
                                IEnumerable<Foto> lstFotoEliminar = objProducto.Foto.Where(b => IdFotos.All(i => b.IdFoto != i));
                                foreach (Foto objFoto in lstFotoEliminar)
                                    if (!String.IsNullOrWhiteSpace(objFoto.Imagen))
                                        new FileInfo(Path.Combine(Server.MapPath(FotoModel.IMAGE_PATH), objFoto.Imagen)).Delete();
                            }

                            //Guarda las nuevas fotos
                            List<Foto> lstFotoNueva = null;
                            if (FotoFile != null)
                            {
                                Directory.CreateDirectory(Server.MapPath(FotoModel.IMAGE_PATH));

                                lstFotoNueva = new List<Foto>();
                                foreach (HttpPostedFileBase objFotoFile in FotoFile)
                                {
                                    if (objFotoFile != null)
                                    {
                                        Foto objFoto = new Foto();
                                        objFoto.Imagen = Guid.NewGuid().ToString() + Path.GetExtension(objFotoFile.FileName);
                                        objFotoFile.SaveAs(Path.Combine(Server.MapPath(FotoModel.IMAGE_PATH), objFoto.Imagen));

                                        lstFotoNueva.Add(objFoto);
                                    }
                                }
                            }

                            if (IdCategorias == null)
                                IdCategorias = new int[] { };

                            objProducto = objProductoModel.ToProducto();

                            //Guardado de servicioAdicional
                            List<ServiciosAdicionales> lstServiciosAdicionales = new List<ServiciosAdicionales>(); ;
                            if (IdServiciosAdicionales != null)
                            {
                                for (int j = 0; j < IdServiciosAdicionales.Length; j++)
                                {

                                    ServiciosAdicionales objServiciosAdicionales = new ServiciosAdicionales();
                                    objServiciosAdicionales.IdServicioAdicionales = IdServiciosAdicionales[j];
                                    objServiciosAdicionales.IdProducto = objProducto.IdProducto;
                                    objServiciosAdicionales.Nombre = NombreServicioAdicional[j];
                                    objServiciosAdicionales.Link = LinkServicioAdicional[j];
                                    objServiciosAdicionales.Precio = PrecioServicioAdicional[j];
                                    objServiciosAdicionales.DescripcionEjemplo = DescripcionEjemplo[j];
                                    lstServiciosAdicionales.Add(objServiciosAdicionales);

                                }
                            }
                            //Guardado de variantes de producto
                            List<VarianteProducto> lstVarianteProducto = null;
                            List<int[]> lstIndiceFoto = null;

                            List<int> ProductoVarianteExistentes = new List<int>();

                            if (objProducto.IdProducto > 0)
                            {
                                lstVarianteProducto = new List<VarianteProducto>();
                                lstIndiceFoto = new List<int[]>();
                                if (IdVariantesProducto != null)
                                {
                                    for (int i = 0; i < IdVariantesProducto.Length; i++)
                                    {

                                        VarianteProducto objVarianteProducto = new VarianteProducto();
                                        objVarianteProducto.IdVarianteProducto = IdVariantesProducto[i];
                                        objVarianteProducto.IdProducto = objProducto.IdProducto;

                                        objVarianteProducto.Link = URLYouTube[i];
                                        objVarianteProducto.Activo = true;

                                        if (!String.IsNullOrWhiteSpace(IndiceFotosSeleccionadas[i]))
                                            lstIndiceFoto.Add(IndiceFotosSeleccionadas[i].Split(',').Select(f => Int32.Parse(f)).ToArray());
                                        else
                                            lstIndiceFoto.Add(new int[] { });

                                        lstVarianteProducto.Add(objVarianteProducto);

                                    }
                                }
                               
                            }
                            else
                            {

                                lstVarianteProducto = new List<VarianteProducto>();
                                //lstVarianteProducto.Add(new VarianteProducto() { Link = URLYouTube[0]??"" ,Precio=objProducto.Precio,PrecioPromocional= (objProducto.PrecioPromocional==null?null: objProducto.PrecioPromocional) });
                                lstVarianteProducto.Add(new VarianteProducto() {Precio = objProducto.Precio });
                                //lstServiciosAdicionales = new List<ServiciosAdicionales>();
                                //lstServiciosAdicionales.Add(new ServiciosAdicionales() { Nombre = NombreServicioAdicional[0], Precio = PrecioServicioAdicional[0], Link = LinkServicioAdicional[0] });

                            }

                            //Guarda el producto
                            objProducto.IdProducto = objCatalogoBC.GuardarProducto(objProducto, lstFotoNueva, IdFotos, IdCategorias, lstVarianteProducto, lstIndiceFoto, lstServiciosAdicionales);

                            objResultObject.Code = 0;
                            objResultObject.Data = objProducto.IdProducto;
                            objResultObject.Message = "¡El producto se ha guardado con éxito!";

                        }
                        else
                        {
                            objResultObject.Code = ERROR_REPEATED_URL;
                            objResultObject.Message = "Ya existe un producto con dicho nombre. Por favor, elige un nombre distinto.";
                        }
                    }
                    else
                    {
                        objResultObject.Code = ERROR_REPEATED_URL;
                        objResultObject.Message = "El precio promocional del producto y de todas sus variantes debe ser menor al precio regular.";
                    }
                }
                else
                {
                    objResultObject.Code = ERROR_INCOMPLETE_DATA;
                    objResultObject.Message = "Debes completar todos los campos requeridos.";
                }
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al guardar los datos del producto. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }
        [PermissionsFilter(new String[] { Constants.Usuario.Perfil.ADMINISTRADOR })]
        public ActionResult EliminarProducto(int IdProducto)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                CatalogoBC objCatalogoBC = new CatalogoBC();
                Producto objProducto = objCatalogoBC.ObtenerProducto(IdProducto);
                List<Foto> lstFoto = objProducto.Foto.ToList();

                //Elimina el producto
                objCatalogoBC.EliminarProducto(IdProducto);

                //Elimina todas sus fotos
                foreach (Foto objFoto in lstFoto)
                    if (!String.IsNullOrWhiteSpace(objFoto.Imagen))
                        new FileInfo(Path.Combine(Server.MapPath(FotoModel.IMAGE_PATH), objFoto.Imagen)).Delete();

                objResultObject.Code = 0;
                objResultObject.Message = "¡El producto se ha eliminado con éxito!";
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al eliminar el producto. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }

        [PermissionsFilter(new String[] { Constants.Usuario.Perfil.ADMINISTRADOR })]
        public ActionResult CambiarEstadoProducto(int IdProducto, bool Activo)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                CatalogoBC objCatalogoBC = new CatalogoBC();

                //Cambia el estado de producto
                objCatalogoBC.CambiarEstadoProducto(IdProducto, Activo);

                objResultObject.Code = 0;
                objResultObject.Message = "¡El estado del producto se ha modificado con éxito!";
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al cambiar el estado del producto. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }

        [PermissionsFilter(new String[] { Constants.Usuario.Perfil.ADMINISTRADOR })]
        public ActionResult MarcarProductoRecomendado(int IdProducto, bool Recomendado)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                CatalogoBC objCatalogoBC = new CatalogoBC();

                //Cambia el estado de producto
                objCatalogoBC.MarcarProductoRecomendado(IdProducto, Recomendado);

                objResultObject.Code = 0;
                objResultObject.Message = "¡El producto se ha marcado como destacado con éxito!";
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al marcar el producto como destacado. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }

        public ActionResult ServicioAdicional(ServiciosAdicionalesModel objServiciosAdicionalesModel)
        {
            ConfiguracionBC objConfiguracionBC=new ConfiguracionBC();
            //Obtener tipo de cambio
          //  objServiciosAdicionalesModel.TipoCambio = Double.Parse(objConfiguracionBC.ObtenerConfiguracion(Constants.Configuracion.TIPO_CAMBIO).Valor ?? "0.00");
            return PartialView(objServiciosAdicionalesModel);
        }

        #region TIPOS DE VARIANTE
        public ActionResult TipoVariante(TipoVarianteModel objTipoVarianteModel)
        {
            return PartialView(objTipoVarianteModel);
        }

        [PermissionsFilter(new String[] { Constants.Usuario.Perfil.ADMINISTRADOR })]
        public ActionResult GuardarTiposVariante(int IdProducto, int[] IdTipoVariantes, String[] Nombres, String[] Tipos, String[] Variantes, bool Restablecer)
        {
            ResultObject objResultObject = new ResultObject();

            try
            {
                CatalogoBC objCatalogoBC = new CatalogoBC();

                if (IdTipoVariantes == null)
                {
                    IdTipoVariantes = new int[] { };
                    Nombres = new String[] { };
                    Tipos = new String[] { };
                    Variantes = new String[] { };
                }

                //Elimina todas las variantes de producto si ha hecho modificaciones
                if (Restablecer)
                    objCatalogoBC.EliminarVariantesProducto(IdProducto);

                //Elimina los tipos de variante borrados por el usuario
                objCatalogoBC.EliminarTiposVarianteProductoExcepto(IdProducto, IdTipoVariantes.Where(i => i != 0).ToArray());

                //Lee la lista de tipos de variante
                List<TipoVariante> lstTipoVariante = new List<TipoVariante>();
                for (int i = 0; i < IdTipoVariantes.Length; i++)
                {
                    TipoVariante objTipoVariante = new TipoVariante();
                    objTipoVariante.IdProducto = IdProducto;
                    objTipoVariante.IdTipoVariante = IdTipoVariantes[i];
                    objTipoVariante.Nombre = Nombres[i];
                    objTipoVariante.Tipo = Tipos[i];

                    String[] lstVarianteString = Variantes[i].Split(',');
                    List<Variante> lstVariante = new List<Variante>();
                    foreach (String VarianteString in lstVarianteString)
                    {
                        Variante objVariante = new Variante();
                        int indexStartColor = VarianteString.IndexOf('[') + 1;
                        int indexEndColor = VarianteString.IndexOf(']');
                        if (indexStartColor >= 0 && indexEndColor >= 0)
                            objVariante.Color = VarianteString.Substring(indexStartColor, indexEndColor - indexStartColor);

                        if (indexEndColor >= 0)
                            objVariante.Nombre = VarianteString.Substring(indexEndColor + 1);
                        else
                            objVariante.Nombre = VarianteString;

                        lstVariante.Add(objVariante);
                    }

                    //Guarda el tipo de variante
                    objCatalogoBC.GuardarTipoVariante(objTipoVariante, lstVariante);
                }

                if (Restablecer)
                    objCatalogoBC.GenerarVariantesProducto(IdProducto);

                objResultObject.Code = 0;
                objResultObject.Data = IdProducto;
                objResultObject.Message = "¡Las variantes se han guardado con éxito!";
            }
            catch (Exception ex)
            {
                objResultObject.CaptureException(ex, "Ocurrió un error al guardar las variantes. Inténtalo nuevamente.");
            }

            return new JsonResult() { Data = objResultObject };
        }
        #endregion
    }
}