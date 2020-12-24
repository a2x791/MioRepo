using Mio.Models.Time;
using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Relations
{
    public class UserTimeslots
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime EarliestDate { get; set; }

        [Required]
        public string Repitition { get; set; }

        public bool Available { get; set; } = true;

        [Required]
        public int TimeslotID { get; set; }
        public Timeslot Timeslot { get; set; }
        [Required]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
