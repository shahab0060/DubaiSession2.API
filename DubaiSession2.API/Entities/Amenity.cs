using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class Amenity
    {
        public Amenity()
        {
            ItemAmenities = new HashSet<ItemAmenity>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;
        public string? IconName { get; set; }

        public virtual ICollection<ItemAmenity> ItemAmenities { get; set; }
    }
}
