using Customer.Domain.TypeofApiResponce.GetGoodList;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using Shop.Maxs.Interfaces;
using Shop.Maxs.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Maxs.Services
{
    public class ShopService: BasicService, IShopService
    {
        public string Header { get; set; }
        public ShopService(ILogger<BasicService> logger, IRestClient restClient, string Header) : base(restClient, logger, Header)
        {
            this.Header = Header;
        }
        public async Task<GetListGoods> GetGoods(string specificPredicante=null)
        {
            IRestRequest request = new RestRequest($"json/Authenticate", Method.GET);
            request.AddJsonBody(specificPredicante);
            return await PostDataAsync<GetListGoods>(request);
        }
    }
}
