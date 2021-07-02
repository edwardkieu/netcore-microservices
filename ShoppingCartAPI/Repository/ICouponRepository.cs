using ShoppingCartAPI.Models.Dto;
using System.Threading.Tasks;

namespace ShoppingCartAPI.Repository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCoupon(string couponName);
    }
}
