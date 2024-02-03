﻿using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class BookingDetail
    {
        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long BookingId { get; set; }
        public long ItemPriceId { get; set; }
        public bool IsRefund { get; set; }
        public DateTime? RefundDate { get; set; }
        public long? RefundCancellationPoliciyId { get; set; }

        public virtual Booking Booking { get; set; } = null!;
        public virtual ItemPrice ItemPrice { get; set; } = null!;
    }
}
