using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Absences.Queries
{
    public class AbsenceModel
    {
        public int Id { get; set; }
        public AbsenceEnum Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ContactPersonId { get; set; }
        public int UserId { get; set; }
        public int TeamLeaderId { get; set; }
        public StatusEnum Status { get; set; }

        public static Expression<Func<Absence, AbsenceModel>> Projection
        {
            get
            {
                return absence => new AbsenceModel
                {
                    Id = absence.AbsenceId,
                    Type = (AbsenceEnum)absence.TypeId,
                    StartDate=absence.StartDate,
                    EndDate = absence.EndDate,
                    ContactPersonId = absence.ContactPersonId,
                    UserId = absence.UserId,
                    TeamLeaderId = absence.TeamLeaderId ?? 0,
                    Status = (StatusEnum)absence.Status
                };
            }
        }

        public static AbsenceModel Create(Absence absence)
        {
            return Projection.Compile().Invoke(absence);
        }
    }

}