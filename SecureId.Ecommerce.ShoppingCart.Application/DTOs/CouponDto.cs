﻿namespace SecureId.Ecommerce.ShoppingCart.Application.DTOs
{
    public class CouponDto
    {
        public Guid CouponId { get; set; }
        public string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
    }
}
