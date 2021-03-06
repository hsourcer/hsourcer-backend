﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using HSourcer.Application.Absences.Queries;
using HSourcer.Application.Interfaces.Mapping;
using HSourcer.Domain.Entities;
using HSourcer.Domain.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HSourcer.WebUI.ViewModels
{
    public class AbsenceViewModel : IMapFrom<AbsenceModel>
    {
        public int Id { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public AbsenceEnum Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ContactPersonId { get; set; }
        public int UserId { get; set; }
        public int TeamLeaderId { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public StatusEnum Status { get; set; }

    }
}