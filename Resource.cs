//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TPQR_Session1_7_9
{
    using System;
    using System.Collections.Generic;
    
    public partial class Resource
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resource()
        {
            this.Resource_Allocation = new HashSet<Resource_Allocation>();
        }
    
        public int resId { get; set; }
        public string resName { get; set; }
        public int resTypeIdFK { get; set; }
        public int remainingQuantity { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Resource_Allocation> Resource_Allocation { get; set; }
        public virtual Resource_Type Resource_Type { get; set; }
    }
}
