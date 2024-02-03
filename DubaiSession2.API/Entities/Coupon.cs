using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class Coupon
    {
        public Coupon()
        {
            Bookings = new HashSet<Booking>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string CouponCode { get; set; } = null!;
        public decimal DiscountPercent { get; set; }
        public decimal MaximimDiscountAmount { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
