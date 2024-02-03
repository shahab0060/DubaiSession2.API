using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class ItemType
    {
        public ItemType()
        {
            Items = new HashSet<Item>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Item> Items { get; set; }
    }
}
