using Mio.Models.Sale;
using Mio.Models.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Relations
{
    public class ServiceTimeslot
    {
        public int ID { get; set; }
        [Required]
        public int ServiceID { get; set; }
        public Service Service { get; set; }

        [Required]
        public double Duration { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public int TimeslotID { get; set; }
        public Timeslot Timeslot { get; set; }

        [Required]
        public string CustomerID { get; set; }
        [Required]
        public string ServerID { get; set; }
    }
}
