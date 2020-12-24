using Mio.Models.Sale;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Text
{
    public class Review : TextContent
    {
        [Required]
        public double Rating { get; set; }
        [Required]
        public int ProductID { get; set; }

    }
}
