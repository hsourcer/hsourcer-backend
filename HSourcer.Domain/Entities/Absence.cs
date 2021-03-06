using System;
using System.Collections.Generic;

namespace HSourcer.Domain.Entities
{
    public class Absence
    {
        public Absence()
        {
        }
        public int AbsenceId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int TypeId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public User ContactPerson { get; set; }
        public int? ContactPersonId { get; set; }
        public User TeamLeader{ get; set; }
        public int? TeamLeaderId { get; set; }
        public DateTime? DecisionDate { get; set; }
        public int Status { get; set; }
        public string Comment { get; set; }
    }
}

