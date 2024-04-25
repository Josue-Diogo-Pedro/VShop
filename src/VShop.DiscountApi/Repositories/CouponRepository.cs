using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop.DiscountApi.Context;
using VShop.DiscountApi.DTOs;

namespace VShop.DiscountApi.Repositories;

public class CouponRepository : ICouponRepository
{
    private readonly DiscountApiDbContext _context;
    private readonly IMapper _mapper;

    public CouponRepository(DiscountApiDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CouponDTO> GetCouponByCode(string code)
    {
        var coupon = await _context.Coupons?.SingleOrDefaultAsync(c => c.CouponCode == code);

        return _mapper.Map<CouponDTO>(coupon);
    }
}
