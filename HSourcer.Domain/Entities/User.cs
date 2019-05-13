using System;
using System.Collections.Generic;

namespace HSourcer.Domain.Entities
{
    public class User
    {
        public User()
        {
            Absences = new HashSet<Absence>(); //user's absences
            AbsencesTeamLeader = new HashSet<Absence>(); //absences which were approved by the team leader
            AbsencesContactPerson = new HashSet<Absence>(); //absences to which user was appointed as contact person
            UsersCreatedBy = new HashSet<User>(); //not sure if good idea
        }
        public int UserId { get; set; }
        public bool Active { get; set; }
        public int CreatedByUserId { get; set; }
        public User CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeactivationDate { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string PhotoPath { get; set; }
        public int Role { get; set; } //TOOD enum + intialize
        public ICollection<Absence> Absences { get; set; }
        public ICollection<Absence> AbsencesTeamLeader { get; set; }
        public ICollection<Absence> AbsencesContactPerson { get; set; }
        public ICollection<User> UsersCreatedBy { get; set; }
    }
}