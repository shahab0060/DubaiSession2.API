using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();
            ItemScores = new HashSet<ItemScore>();
            Items = new HashSet<Item>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public long UserTypeId { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public bool Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int FamilyCount { get; set; }

        public virtual UserType UserType { get; set; } = null!;
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<ItemScore> ItemScores { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
