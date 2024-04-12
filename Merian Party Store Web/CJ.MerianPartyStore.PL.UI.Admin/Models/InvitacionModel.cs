using CJ.MerianPartyStore.DL.DM;
using CJ.MerianPartyStore.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Web;

namespace CJ.MerianPartyStore.PL.UI.Admin.Models
{
    public class InvitacionModel
    {
        public int IdInvitacion { get; set; }
        public int TiempoCarga { get; set; }
        public string ClienteInvitacion { get; set; }
        public DateTime? FechaEvento { get; set; }
        public TimeSpan? HoraEvento { get; set; }
        public string ColorAudio { get; set; }
        public string ContactoAsistenacia { get; set; }
        public string ContactoOpcional { get; set; }
        public List<FotoModel> Fotos { get; set; }
        public string BodaCivil { get; set; }
        public string BodaReligiosa { get; set; }
        public string TipoInvitacion { get; set; }
        public string MesaRegalos { get; set; }
        public string EntidadBancaria { get; set; }
        public string NumeroCuenta { get; set; }
        public string CCI { get; set; }
        public string ColorContador { get; set; }
        public string FrasePrincipal { get; set; }
        public bool? MostrarIcono { get; set; }
        public string LinkCalendario { get; set; }
        public string PDFFile { get; set; }
        public string AudioFile { get; set; }
        public string FondoFile { get; set; }
        public string VideoFile { get; set; }
        public const String INVITACION_PATH = "~/Images/Invitacion/";
        public const String VIDEO_INVITACION_PATH = "~/Images/Invitacion/Video";
        public const String PDF_INVITACION_PATH = "~/Images/Invitacion/PDF";
        public const String AUDIO_INVITACION_PATH = "~/Images/Invitacion/Audio";
        public const String FONDO_INVITACION_PATH = "~/Images/Invitacion/Fondo";
        public static InvitacionModel FromInvitacion(Invitacion objInvitacion, bool Fotos)
        {
            try
            {
                InvitacionModel objInvitacionModel = new InvitacionModel();

                objInvitacionModel.IdInvitacion = objInvitacion.IdInvitacion;
                objInvitacionModel.ClienteInvitacion = objInvitacion.Cliente;
                objInvitacionModel.FechaEvento = objInvitacion.FechaInvitacion;
                objInvitacionModel.HoraEvento = objInvitacion.HoraInvitacion;

                if (objInvitacion.FotoInvitacion != null && Fotos)
                {
                    objInvitacionModel.Fotos = new List<FotoModel>();
                    foreach (FotoInvitacion objFotoInvitacion in objInvitacion.FotoInvitacion)
                        objInvitacionModel.Fotos.Add(FotoModel.FromFotoInvitacion(objFotoInvitacion));
                }

                objInvitacionModel.ColorAudio = objInvitacion.Color;
                objInvitacionModel.ContactoAsistenacia = objInvitacion.TelefonoContactoInvitacion;
                objInvitacionModel.ContactoOpcional = objInvitacion.TelefonoContactoInvitacion2;
                objInvitacionModel.BodaCivil = objInvitacion.UbicacionGoogleMaps;
                objInvitacionModel.BodaReligiosa = objInvitacion.UbicacionGoogleMaps2;
                objInvitacionModel.TipoInvitacion = objInvitacion.TipoInvitacion;
                objInvitacionModel.LinkCalendario = objInvitacion.LinkCalendario;
                objInvitacionModel.MostrarIcono = objInvitacion.MostrarIcono??true;
                objInvitacionModel.FrasePrincipal = objInvitacion.Titulo;
                objInvitacionModel.TiempoCarga= objInvitacion.TiempoCarga ?? 0;

                if (objInvitacion.UrlPDFInvitacion != null)
                    objInvitacionModel.PDFFile = Path.Combine(PDF_INVITACION_PATH, objInvitacion.UrlPDFInvitacion);

                if (objInvitacion.Audio != null)
                    objInvitacionModel.AudioFile = Path.Combine(AUDIO_INVITACION_PATH, objInvitacion.Audio);

                if (objInvitacion.Fondo != null)
                    objInvitacionModel.FondoFile = Path.Combine(FONDO_INVITACION_PATH, objInvitacion.Fondo);

                objInvitacionModel.MesaRegalos = objInvitacion.LinkMesaRegalos;
                objInvitacionModel.EntidadBancaria = objInvitacion.TipoEntidadBancaria;
                objInvitacionModel.NumeroCuenta = objInvitacion.NumeroCuenta;
                objInvitacionModel.CCI = objInvitacion.CCI;
                objInvitacionModel.ColorContador = objInvitacion.CountdownColor;

                if (objInvitacion.VideoInvitacion != null)
                    objInvitacionModel.VideoFile = Path.Combine(VIDEO_INVITACION_PATH, objInvitacion.VideoInvitacion);
                return objInvitacionModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static InvitacionModel FromInvitacionVideo(Invitacion objInvitacion)
        {
            try
            {
                InvitacionModel objInvitacionModel = new InvitacionModel();

                objInvitacionModel.IdInvitacion = objInvitacion.IdInvitacion;
                objInvitacionModel.ClienteInvitacion = objInvitacion.Cliente;
                objInvitacionModel.FechaEvento = objInvitacion.FechaInvitacion;
                objInvitacionModel.HoraEvento = objInvitacion.HoraInvitacion;
                objInvitacionModel.ColorAudio = objInvitacion.Color;       
                objInvitacionModel.TipoInvitacion = objInvitacion.TipoInvitacion;
                objInvitacionModel.ColorContador = objInvitacion.CountdownColor;
                objInvitacionModel.FrasePrincipal = objInvitacion.Titulo;
                objInvitacionModel.TiempoCarga = objInvitacion.TiempoCarga??0;

                if (objInvitacion.UrlPDFInvitacion != null)
                    objInvitacionModel.PDFFile = objInvitacion.UrlPDFInvitacion;

                if (objInvitacion.VideoInvitacion != null)
                    objInvitacionModel.VideoFile =  objInvitacion.VideoInvitacion;

                if (objInvitacion.Fondo != null)
                    objInvitacionModel.FondoFile = Path.Combine(FONDO_INVITACION_PATH, objInvitacion.Fondo);

                return objInvitacionModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static InvitacionModel FromInvitacionPDF(Invitacion objInvitacion)
        {
            try
            {
                InvitacionModel objInvitacionModel = new InvitacionModel();

                objInvitacionModel.IdInvitacion = objInvitacion.IdInvitacion;
                objInvitacionModel.ClienteInvitacion = objInvitacion.Cliente;
                objInvitacionModel.FechaEvento = objInvitacion.FechaInvitacion;
                objInvitacionModel.HoraEvento = objInvitacion.HoraInvitacion;
                objInvitacionModel.ColorAudio = objInvitacion.Color;
                objInvitacionModel.TipoInvitacion = objInvitacion.TipoInvitacion;
                objInvitacionModel.ColorContador = objInvitacion.CountdownColor;
                objInvitacionModel.FrasePrincipal = objInvitacion.Titulo;
                objInvitacionModel.TiempoCarga = objInvitacion.TiempoCarga ?? 0;

                if (objInvitacion.UrlPDFInvitacion != null)
                    objInvitacionModel.PDFFile = objInvitacion.UrlPDFInvitacion;

                if (objInvitacion.Audio != null)
                    objInvitacionModel.AudioFile = Path.Combine(AUDIO_INVITACION_PATH, objInvitacion.Audio);

                if (objInvitacion.Fondo != null)
                    objInvitacionModel.FondoFile = objInvitacion.Fondo;

                return objInvitacionModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Invitacion ToInvitacion()
        {
            try
            {
                Invitacion objInvitacion = new Invitacion();
                objInvitacion.IdInvitacion = IdInvitacion;
                objInvitacion.Cliente = ClienteInvitacion;
                objInvitacion.FechaInvitacion = FechaEvento;
                objInvitacion.HoraInvitacion = HoraEvento;
                objInvitacion.Color = ColorAudio;
                objInvitacion.TelefonoContactoInvitacion = ContactoAsistenacia;
                objInvitacion.TelefonoContactoInvitacion2 = ContactoOpcional;
                objInvitacion.UbicacionGoogleMaps = BodaCivil;
                objInvitacion.UbicacionGoogleMaps2 = BodaReligiosa;
                objInvitacion.TipoInvitacion = TipoInvitacion;
                objInvitacion.LinkMesaRegalos = MesaRegalos;
                objInvitacion.TipoEntidadBancaria = EntidadBancaria;
                objInvitacion.NumeroCuenta = NumeroCuenta;
                objInvitacion.CCI = CCI;
                objInvitacion.CountdownColor = ColorContador;
                objInvitacion.Titulo = FrasePrincipal;
                objInvitacion.MostrarIcono = MostrarIcono;
                objInvitacion.LinkCalendario = LinkCalendario;
                objInvitacion.TiempoCarga = TiempoCarga;

                if (PDFFile != null)
                    objInvitacion.UrlPDFInvitacion = PDFFile.Replace(PDF_INVITACION_PATH, "");

                if (AudioFile != null)
                    objInvitacion.Audio = AudioFile.Replace(AUDIO_INVITACION_PATH, "");

                if (VideoFile != null)
                    objInvitacion.VideoInvitacion = VideoFile.Replace(VIDEO_INVITACION_PATH, "");

                if (FondoFile != null)
                    objInvitacion.Fondo = FondoFile.Replace(FONDO_INVITACION_PATH, "");

                return objInvitacion;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}