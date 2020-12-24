

namespace Customer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Good: ModelBase
    {
        public Good()
        {
            this.SalePos = new HashSet<SalePos>();
        }
        public Good(string GoodName, Nullable<int> ManufacturerId,
            Nullable<int> CategoryId, decimal Price, decimal GoodCount)
        {
            this.GoodName = GoodName;
            this.ManufacturerId = ManufacturerId;
            this.CategoryId = CategoryId;
            this.Price = Price;
            this.GoodCount = GoodCount;
        }
    
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public Nullable<int> ManufacturerId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal GoodCount { get; set; }
    
        public virtual Category Category { get; set; }
        public virtual Manufacturer Manufacturer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SalePos> SalePos { get; set; }
    }
}
