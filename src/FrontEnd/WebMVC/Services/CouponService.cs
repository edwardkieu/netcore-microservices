using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Dtos;
using WebMVC.Enums;
using WebMVC.Services.IServices;

namespace WebMVC.Services
{
    public class CouponService : BaseService, ICouponService
    {
        public CouponService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
        }
        public async Task<T> GetCoupon<T>(string couponCode, string token = null)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = ApiType.GET,
                Url = Constants.CouponAPIBase + "/api/coupon/" + couponCode,
                AccessToken = token
            });
        }
    }
}
