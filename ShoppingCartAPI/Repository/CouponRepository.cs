using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ShoppingCartAPI.Models.Dto;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        public CouponRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient 
            { 
                BaseAddress = new Uri(_configuration.GetSection("ServiceUrls:CouponAPI").Value)
            };
        }

        public async Task<CouponDto> GetCoupon(string couponName)
        {
            var response = await _client.GetAsync($"/api/coupon/{couponName}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            if (resp.IsSuccess)
            {
                return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(resp.Result));
            }
            return new CouponDto();
        }
    }
}
