using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Maxs.ViewModels
{
    public class GoodsViewModel
    {
        public GoodsViewModel(List<GoodsDto> GoodsDtos) : base()
        {
            this.GoodsDtos = GoodsDtos;
        }
        public List<GoodsDto> GoodsDtos { get; set; }
    }
    public class GoodsDto
    {
        public GoodsDto()
        {

        }
        public int GoodId { get; set; }
        public string GoodName { get; set; }
        public int ManufacturerId { get; set; }
        public int CategoryId { get; set; }
        public string Price { get; set; }
        public decimal GoodCount { get; set; }
    }
}
