using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Time
{
    public class Timeslot
    {
        public int ID { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string Day { get; set; }

    }
}
