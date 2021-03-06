using System;
using System.Collections.Generic;

namespace HSourcer.Domain.Entities
{
    public class Organization
    {
        public Organization()
        {
            Teams = new HashSet<Team>();
        }
        public int OrganizationId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Team> Teams { get; set; }
    }
}

