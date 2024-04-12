using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.DL.DA
{
    public class VarianteProductoDA
    {
        public IQueryable<VarianteProducto> ListarVariantesProducto(int? IdMarca, String Busqueda, ValueContainer TotalPages, int? Page, int? PageSize)
        {
            try
            {
                String[] Palabras = { };

                if (!String.IsNullOrWhiteSpace(Busqueda))
                    Palabras = Busqueda.Split(' ');
                else
                    Busqueda = null;

                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<VarianteProducto> lstVarianteProducto = objModel.VarianteProducto.Where(vp =>
                    (IdMarca == null ) &&
                    (Busqueda == null || Palabras.All(w => vp.Producto.Nombre.Contains(w)) ));

                if (TotalPages != null && Page != null && PageSize != null)
                {
                    TotalPages.Value = (int)Math.Ceiling(lstVarianteProducto.Count() / (double)PageSize.Value);
                    lstVarianteProducto = lstVarianteProducto.OrderBy(vp => vp.Producto.Nombre).Skip((Page.Value - 1) * PageSize.Value).Take(PageSize.Value);
                }
                else if (TotalPages != null)
                    TotalPages.Value = 1;

                return lstVarianteProducto;
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.VarianteProducto.Where(v => IdVariantesProducto.Any(i => i == v.IdVarianteProducto));
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                return objModel.VarianteProducto.SingleOrDefault(v => IdVarianteProducto == v.IdVarianteProducto);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                int CantidadVariantes = IdVariantes.Length;
                return objModel.VarianteProducto.FirstOrDefault(vp => IdVariantes.All(i => vp.Variante.Any(v => i == v.IdVariante)) && vp.Variante.Count == CantidadVariantes);
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                Producto objProducto = objModel.Producto.SingleOrDefault(p => p.IdProducto == IdProducto);

                int CantidadVariantesProducto = 1;
                int CantidadTiposVariante = objProducto.TipoVariante.Count;

                foreach (TipoVariante objTipoVariante in objProducto.TipoVariante)
                    CantidadVariantesProducto = CantidadVariantesProducto * objTipoVariante.Variante.Count;

                int[,] IndicesVariantes = new int[CantidadVariantesProducto, CantidadTiposVariante];
                for (int j = 0; j < CantidadTiposVariante; j++)
                {
                    int CantidadConsecutivos = CantidadVariantesProducto;
                    for (int k = 0; k <= j; k++)
                        CantidadConsecutivos /= objProducto.TipoVariante.ElementAt(k).Variante.Count;

                    int indice = 0;
                    int contadorConsecutivos = 0;
                    for (int i = 0; i < CantidadVariantesProducto; i++)
                    {
                        IndicesVariantes[i, j] = indice;
                        contadorConsecutivos++;
                        if (contadorConsecutivos == CantidadConsecutivos)
                        {
                            indice++;
                            contadorConsecutivos = 0;

                            if (indice > objProducto.TipoVariante.ElementAt(j).Variante.Count - 1)
                                indice = 0;
                        }
                    }
                }

                List<VarianteProducto> lstVarianteProducto = new List<VarianteProducto>();
                for (int i = 0; i < CantidadVariantesProducto; i++)
                {
                    VarianteProducto objVarianteProducto = new VarianteProducto();
                    objVarianteProducto.IdProducto = objProducto.IdProducto;
                    //objVarianteProducto.Precio = objProducto.Precio;
                    //objVarianteProducto.PrecioPromocional = objProducto.PrecioPromocional;
                    objVarianteProducto.Activo = true;

                    for (int j = 0; j < CantidadTiposVariante; j++)
                    {
                        Variante objVariante = objProducto.TipoVariante.ElementAt(j).Variante.ElementAt(IndicesVariantes[i, j]);
                        objVarianteProducto.Variante.Add(objVariante);
                    }

                    objModel.VarianteProducto.Add(objVarianteProducto);
                }

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void GuardarVariantesProducto(int IdProducto, List<VarianteProducto> lstVarianteProducto, List<int[]> lstIndiceFoto)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                List<Foto> lstFoto = objModel.Foto.Where(f => f.IdProducto == IdProducto).ToList();

                int[] IdVariantesProducto = lstVarianteProducto.Select(vp => vp.IdVarianteProducto).ToArray();
                IQueryable<VarianteProducto> lstVarianteProductoDesactivar = objModel.VarianteProducto.Where(vp => IdVariantesProducto.All(i => i != vp.IdVarianteProducto) && vp.IdProducto == IdProducto);
                foreach (VarianteProducto objVarianteProducto in lstVarianteProductoDesactivar)
                    objVarianteProducto.Activo = false;
                
                Producto objProducto = objModel.Producto.FirstOrDefault(p => p.IdProducto == IdProducto);

                IQueryable<ServiciosAdicionales> lstServiciosAdicionales = objModel.ServiciosAdicionales.Where(sa => sa.IdProducto == IdProducto);
                for (int i = 0; i < lstVarianteProducto.Count; i++)
                {
                    VarianteProducto objVarianteProducto = lstVarianteProducto[i];

                    if (objVarianteProducto.IdVarianteProducto == 0)
                    {
                        objVarianteProducto.Activo = true;
                        objModel.VarianteProducto.Add(objVarianteProducto);
                    }
                    else
                    {
                        VarianteProducto objVarianteProductoBD = objModel.VarianteProducto.SingleOrDefault(v => v.IdVarianteProducto == objVarianteProducto.IdVarianteProducto);
                        objVarianteProductoBD.IdProducto = objVarianteProducto.IdProducto;
                        objVarianteProductoBD.Link = objVarianteProducto.Link;
                        objVarianteProductoBD.Precio = objVarianteProducto.Precio;
                        objVarianteProductoBD.PrecioPromocional = objVarianteProducto.PrecioPromocional;
                        objVarianteProductoBD.Activo = objVarianteProducto.Activo;
                        objVarianteProductoBD.Foto.Clear();
                        objVarianteProductoBD.ServiciosAdicionales.Clear();

                        int[] IndiceFotos = lstIndiceFoto[i];
                        foreach (int IndiceFoto in IndiceFotos)
                            objVarianteProductoBD.Foto.Add(lstFoto[IndiceFoto]);

                        foreach (ServiciosAdicionales objServiciosAdicionales in lstServiciosAdicionales)
                            objVarianteProductoBD.ServiciosAdicionales.Add(objServiciosAdicionales);
                    }
                }

                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void CambiarEstadoVarianteProducto(int IdVarianteProducto, bool Activo)
        {
            try
            {
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                VarianteProducto objVarianteProducto = objModel.VarianteProducto.SingleOrDefault(u => u.IdVarianteProducto == IdVarianteProducto);
                objVarianteProducto.Activo = Activo;

                objModel.SaveChanges();
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
                DBMerianPartyStoreEntities objModel = new DBMerianPartyStoreEntities();
                IQueryable<VarianteProducto> lstVarianteProducto = objModel.VarianteProducto.Where(v => v.IdProducto == IdProducto);
                foreach (VarianteProducto objVarianteProducto in lstVarianteProducto)
                {
                    objVarianteProducto.Foto.Clear();
                    objVarianteProducto.Variante.Clear();
                    objVarianteProducto.ServiciosAdicionales.Clear();


                }
                objModel.VarianteProducto.RemoveRange(lstVarianteProducto);
                objModel.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
