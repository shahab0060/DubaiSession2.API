using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class Area
    {
        public Area()
        {
            Attractions = new HashSet<Attraction>();
            Items = new HashSet<Item>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Attraction> Attractions { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
