using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class CancellationPolicy
    {
        public CancellationPolicy()
        {
            CancellationRefundFees = new HashSet<CancellationRefundFee>();
            ItemPrices = new HashSet<ItemPrice>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;
        public decimal Commission { get; set; }

        public virtual ICollection<CancellationRefundFee> CancellationRefundFees { get; set; }
        public virtual ICollection<ItemPrice> ItemPrices { get; set; }
    }
}
