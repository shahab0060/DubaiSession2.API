﻿using System;
using System.Collections.Generic;

namespace DubaiSession2.API.Entities
{
    public partial class UserType
    {
        public UserType()
        {
            Users = new HashSet<User>();
        }

        public long Id { get; set; }
        public Guid Guid { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
