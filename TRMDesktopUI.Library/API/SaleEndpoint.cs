using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.Library.API
{
    public class SaleEndpoint : ISaleEndpoint
    {
        private IAPIHelper _apiHelper;
        public SaleEndpoint(IAPIHelper aPIHelper)
        {
            _apiHelper = aPIHelper;
        }

        public async Task PostSale(SaleModel sale)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("api/Sale", sale))
            {
                if (response.IsSuccessStatusCode)
                {
                    //Log Succesful call?
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        //public async Task<List<SaleModel>> Post()
        //{
        //    using (HttpResponseMessage response = await _apiHelper.ApiClient.GetAsync("api/Product"))
        //    {
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = await response.Content.ReadAsAsync<List<ProductModel>>();
        //            return result;
        //        }
        //        else
        //        {
        //            throw new Exception(response.ReasonPhrase);
        //        }
        //    }
        //}
    }
}
