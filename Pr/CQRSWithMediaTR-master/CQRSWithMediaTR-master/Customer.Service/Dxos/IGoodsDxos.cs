using Customer.Domain.Models;
using Shop.Domain.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service.Data_Exchange_Objects
{
    public interface IGoodsDxos
    {
        List<GoodsDto> MapListGoodDto(IEnumerable<Good> goods);
    }
}
