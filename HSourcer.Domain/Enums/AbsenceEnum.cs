using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace HSourcer.Domain.Enums
{
    public enum AbsenceEnum
    {
        [Description("Choroba")]
        SICK_LEAVE = 0,
        [Description("Urlop")]
        HOLIDAYS = 1,
        [Description("Praca zdalna")]
        REMOTE_WORK = 2
    }
}