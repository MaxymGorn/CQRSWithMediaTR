using Microsoft.AspNetCore.Mvc;
using Shop.Maxs.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Maxs.Controllers
{
    [Route("Goods")]
    public class GoodsController: Controller
    {

        public GoodsController() 
        {
        }
        [HttpGet("GetGoodsAsync")]
        public async Task<ActionResult> GetGoodsAsync()
        {
            GoodsViewModel goodsViewModel = new GoodsViewModel(new List<GoodsDto>());
            return PartialView("GetGoods", goodsViewModel);
        }
    }
}
