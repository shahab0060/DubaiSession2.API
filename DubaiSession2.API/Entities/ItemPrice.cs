using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class ItemPrice
    {
        public ItemPrice()
        {
            BookingDetails = new HashSet<BookingDetail>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long ItemId { get; set; }
        public DateTime Date { get; set; }
        public decimal Price { get; set; }
        public long CancellationPolicyId { get; set; }

        public virtual CancellationPolicy CancellationPolicy { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
    }
}
