
namespace Customer.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class SalePos: ModelBase
    {
        public int SalePosId { get; set; }
        public int SaleId { get; set; }
        public int GoodId { get; set; }
        public int CountGood { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Summa { get; set; }
    
        public virtual Good Good { get; set; }
        public virtual Sale Sale { get; set; }
    }
}
