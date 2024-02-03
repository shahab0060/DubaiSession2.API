using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class Score
    {
        public Score()
        {
            ItemScores = new HashSet<ItemScore>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ItemScore> ItemScores { get; set; }
    }
}
