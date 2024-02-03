using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class TransactionType
    {
        public TransactionType()
        {
            Transactions = new HashSet<Transaction>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
