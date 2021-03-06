using System;
using System.Collections.Generic;

namespace HSourcer.Domain.Entities
{
    public class Team
    {
        public Team()
        {
            Users = new HashSet<User>();
        }
        public int TeamId { get; set; }
        public int OrganizationId { get; set; }
        public Organization Organization { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }

        public ICollection<User> Users { get; set; }

    }
}

