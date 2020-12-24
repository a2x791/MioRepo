using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Text
{
    public class Space
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public int NumberStories { get; set; } = 0;
    }
}
