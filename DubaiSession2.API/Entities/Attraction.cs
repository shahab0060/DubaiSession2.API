using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class Attraction
    {
        public Attraction()
        {
            ItemAttractions = new HashSet<ItemAttraction>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long AreaId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;

        public virtual Area Area { get; set; } = null!;
        public virtual ICollection<ItemAttraction> ItemAttractions { get; set; }
    }
}
