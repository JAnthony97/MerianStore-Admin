using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.Util
{
    public class Constants
    {
        public class NavigationAction
        {
            public const String MENU = "M";
            public const String BACK = "B";
        }
        public class Section
        {
            public const String CATALOGO = "CAT";
            public const String INVITACIONES = "INV";
            public const String CONFIGURACION = "CNF";
        }

        public class SectionCliente
        {
            public const String INICIO = "INICIO";
            public const String HISTORIA = "HISTORIA";
            public const String FORMATOS = "FORMATOS";
            public const String INVITACIONES = "INVITACIONES";
            public const String OFERTAS = "OFERTAS";
            public const String PROMOCIONES = "PROMOCIONES";
            public const String OPINIONES = "OPINIONES";
            public const String CONTACTO = "CONTACTO";
        }

        public class Usuario
        {
            public const String MASTER = "master";
            public class Perfil
            {
                public const String ADMINISTRADOR = "A";
                public const String MERCHANT = "M";
                public const String MARKETING = "K";
                public const String CLIENTE = "C";
            }
        }

        public class Persona
        {
            public class TipoDocumento
            {
                public const String DNI = "D";
                public const String RUC = "R";
                public const String PASAPORTE = "P";
                public const String CE = "C";
            }

            public class Sexo
            {
                public const String HOMBRE = "H";
                public const String MUJER = "M";
            }
        }

        public class Invitacion
        {
            public class TipoInvitacion
            {
                public const String PDF = "PDF";
                public const String ANIMADO = "ANIMADO";
                public const String VIDEO = "VIDEO";
            }
        }

        public class EntidadBancaria
        {
            public const String VISA = "VISA";
            public const String MASTERCARD = "MASTERCARD";
            public const String BANBIF = "BANBIF";
            public const String AZTECA = "AZTECA";
            public const String COMERCIO = "COMERCIO";
            public const String FALABELLA = "FALABELLA";
            public const String GN = "GN";
            public const String PICHINCHA = "PICHINCHA";
            public const String RIPLEY = "RIPLEY";
            public const String SANTANDER = "SANTANDER";
            public const String CHINA = "CHINA";
            public const String BBVA = "BBVA";
            public const String BCP = "BCP";
            public const String CITIBANK = "CITIBANK";
            public const String ICBC = "ICBC";
            public const String INTERBANK = "INTERBANK";
            public const String MIBANCO = "MIBANCO";
            public const String SCOTIABANK = "SCOTIABANK";
        }
        public class Producto
        {
            public class Filtro
            {
                public const String TODOS = null;
            }

            public class Orden
            {
                public const String MAS_COMPRADOS = "MC";
                public const String A_Z = "AZ";
                public const String Z_A = "ZA";
                public const String MENOR_PRECIO = "mP";
                public const String MAYOR_PRECIO = "MP";
                public const String MEJOR_RATING = "R";
            }          

            public class Variante
            {
                public class Tipo
                {
                    public const String COLOR = "C";
                }
            }

        }
        public class Configuracion
        {
            public const String HOME_BANNER = "HOME_BANNER";
            public const String TIPO_CAMBIO = "TIPO_CAMBIO";
        }

        public class Catalogo
        {
            public class Visualizacion
            {
                public const String PROMOCIONES = "P";
                public const String OFERTA = "L";
            }
        }
    }
}
