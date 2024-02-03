using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class Booking
    {
        public Booking()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long UserId { get; set; }
        public DateTime BookingDate { get; set; }
        public long? CouponId { get; set; }
        public long? TransactionId { get; set; }
        public decimal? AmountPaid { get; set; }

        public virtual Coupon? Coupon { get; set; }
        public virtual Transaction? Transaction { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
