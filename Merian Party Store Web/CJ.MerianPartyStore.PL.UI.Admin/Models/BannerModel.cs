using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class BannerModel
    {
        public int IdBanner { get; set; }
        public String Imagen { get; set; }
        public String Link { get; set; }

        public const String IMAGE_MARCA_PATH = "~/Images/Marcas/Banners/";
        public const String IMAGE_HOME_PATH = "~/Images/Home/";

        public static BannerModel FromBanner(Banner objBanner)
        {
            try
            {
                BannerModel objBannerModel = new BannerModel();

                objBannerModel.IdBanner = objBanner.IdBanner;
                objBannerModel.Imagen = Path.Combine(IMAGE_MARCA_PATH, objBanner.Imagen);
                return objBannerModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static BannerModel FromConfiguracion(Configuracion objConfiguracion)
        {
            try
            {
                BannerModel objBannerModel = new BannerModel();

                objBannerModel.IdBanner = objConfiguracion.IdConfiguracion;
                objBannerModel.Imagen = Path.Combine(IMAGE_HOME_PATH, objConfiguracion.Valor);
                objBannerModel.Link = objConfiguracion.Valor2;
                return objBannerModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Banner ToBanner()
        {
            try
            {
                Banner objBanner = new Banner();
                objBanner.IdBanner = IdBanner;
                objBanner.Imagen = Imagen.Replace(IMAGE_MARCA_PATH, "");

                return objBanner;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}