using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class Transaction
    {
        public Transaction()
        {
            Bookings = new HashSet<Booking>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long UserId { get; set; }
        public long TransactionTypeId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string GatewayReturnId { get; set; } = null!;

        public virtual TransactionType TransactionType { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
