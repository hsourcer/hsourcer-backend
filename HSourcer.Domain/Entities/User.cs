using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace HSourcer.Domain.Entities
{
    public class User : IdentityUser<int>
    {
        public User()
        {
            Absence = new HashSet<Absence>(); //user's Absence
            AbsenceTeamLeader = new HashSet<Absence>(); //Absence which were approved by the team leader
            AbsenceContactPerson = new HashSet<Absence>(); //Absence to which user was appointed as contact person
            UsersCreatedBy = new HashSet<User>(); //not sure if good idea
        }
        public bool Active { get; set; }
        public int? CreatedByUserId { get; set; }
        public User CreatedBy { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? DeactivationDate { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public string FirstName { get; set; }
        public string UserRole { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhotoPath { get; set; }
        public virtual ICollection<Absence> Absence { get; set; }
        public virtual ICollection<Absence> AbsenceTeamLeader { get; set; }
        public virtual ICollection<Absence> AbsenceContactPerson { get; set; }
        public virtual ICollection<User> UsersCreatedBy { get; set; }
    }
}