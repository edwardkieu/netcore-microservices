using CouponAPI.Models.Dto;
using System.Threading.Tasks;

namespace CouponAPI.Respository
{
    public interface ICouponRepository
    {
        Task<CouponDto> GetCouponByCode(string couponCode);
    }
}
