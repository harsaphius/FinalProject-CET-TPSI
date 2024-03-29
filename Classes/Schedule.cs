using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalProject.Classes
{
    public class Schedule
    {

        public int ScheduleID { get; set; }
        public DateTime TimeSlotBeginTime { get; set; }
        public DateTime TimeSlotEndTime { get; set; }
        public int AvailabilityID { get; set; }


    }
}