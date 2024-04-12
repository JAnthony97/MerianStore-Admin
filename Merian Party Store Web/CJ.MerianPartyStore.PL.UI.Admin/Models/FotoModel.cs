using CJ.MerianPartyStore.DL.DM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class FotoModel
    {
        public int IdFoto { get; set; }
        public String Imagen { get; set; }
        public String Name { get; set; }

        public const String IMAGE_PATH = "~/Images/Productos/";

        public const String IMAGE_INVITACION_PATH = "~/Images/Invitacion/FotoInvitacion";
        public static FotoModel FromFoto(Foto objFoto)
        {
            try
            {
                FotoModel objFotoModel = new FotoModel();

                objFotoModel.IdFoto = objFoto.IdFoto;
                objFotoModel.Imagen = Path.Combine(IMAGE_PATH, objFoto.Imagen);
                return objFotoModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static FotoModel FromFotoInvitacion(FotoInvitacion objFotoInvitacion)
        {
            try
            {
                FotoModel objFotoModel = new FotoModel();

                objFotoModel.IdFoto = objFotoInvitacion.IdFotoInvitacion;
                objFotoModel.Imagen = Path.Combine(IMAGE_INVITACION_PATH, objFotoInvitacion.Foto);
                return objFotoModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public FotoInvitacion ToFotoInvitacion()
        {
            try
            {
                FotoInvitacion objFotoInvitacion = new FotoInvitacion();
                objFotoInvitacion.IdFotoInvitacion = IdFoto;
                objFotoInvitacion.Foto = Imagen.Replace(IMAGE_INVITACION_PATH, "");

                return objFotoInvitacion;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Foto ToFoto()
        {
            try
            {
                Foto objFoto = new Foto();
                objFoto.IdFoto = IdFoto;
                objFoto.Imagen = Imagen.Replace(IMAGE_PATH, "");

                return objFoto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}