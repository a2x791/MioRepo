using Mio.Models.Time;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Packets
{
    public class UserPacket
    {
        [Required]
        public string ID { get; set; }
        public string FieldValue { get; set; }
        public Timeslot Timeslot { get; set; }
        public DateTime DateTime { get; set; }
    }
}
