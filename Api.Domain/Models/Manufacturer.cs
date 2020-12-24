

namespace Customer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Manufacturer: ModelBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Manufacturer(string manufacturerName)
        {
            this.ManufacturerName = manufacturerName;
        }
    
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Good> Goods { get; set; }
    }
}
