

namespace Customer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Sale: ModelBase
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sale()
        {
            this.SalePos = new HashSet<SalePos>();
        }
    
        public int SaleId { get; set; }
        public int NumberSale { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public System.DateTime DateSale { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Summa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePos> SalePos { get; set; }
    }
}
