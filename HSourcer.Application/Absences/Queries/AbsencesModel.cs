﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Enums;

namespace HSourcer.Application.Absences.Queries
{
    public class AbsencesModel
    {
        public int Id { get; set; }
        public AbsenceEnum Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ContactPersonId { get; set; }
        public int UserId { get; set; }
        public int TeamLeaderId { get; set; }
        public int TeamId { get; set; }
        public StatusEnum Status { get; set; }

        public static Expression<Func<Absence, AbsencesModel>> Projection
        {
            get
            {
                return absence => new AbsencesModel
                {
                    Id = absence.AbsenceId,
                    Type = (AbsenceEnum)absence.TypeId,
                    StartDate=absence.StartDate,
                    EndDate = absence.EndDate,
                    ContactPersonId = absence.ContactPersonId,
                    UserId = absence.UserId,
                    TeamLeaderId = absence.TeamLeaderId ?? 0,
                    TeamId =absence.TypeId,
                    Status = (StatusEnum)absence.Status
                };
            }
        }

        public static AbsencesModel Create(Absence absence)
        {
            return Projection.Compile().Invoke(absence);
        }
    }

}