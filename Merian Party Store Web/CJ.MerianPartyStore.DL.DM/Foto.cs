//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CJ.MerianPartyStore.DL.DM
{
    using System;
    using System.Collections.Generic;
    
    public partial class Foto
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Foto()
        {
            this.VarianteProducto = new HashSet<VarianteProducto>();
        }
    
        public int IdFoto { get; set; }
        public Nullable<int> IdProducto { get; set; }
        public string Imagen { get; set; }
        public Nullable<System.DateTime> FechaRegistro { get; set; }
        public Nullable<System.DateTime> FechaActualizacion { get; set; }
        public Nullable<bool> Eliminado { get; set; }
    
        public virtual Producto Producto { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VarianteProducto> VarianteProducto { get; set; }
    }
}
