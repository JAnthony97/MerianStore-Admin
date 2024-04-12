using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CJ.MerianPartyStore.BL.BE
{
    public class ItemCatalogo
    {
      
            public int IdItemCatalogo { get; set; }
            public String Tipo { get; set; }
            public String Promocion { get; set; }
            public String Nombre { get; set; }
            public String Marca { get; set; }
            public String Url { get; set; }
            public String FotoPrincipal { get; set; }
            public double Precio { get; set; }
            public double? PrecioPromocional { get; set; }
    
    }
}
