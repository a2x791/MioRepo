using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Sale
{
    public class Commodity : Product
    {
        public bool RentAvailable { get; set; } = false;

        [Required]
        public int Units { get; set; }

        [Required]
        public int ProductOptionID { get; set; }
        public ProductOption ProductOption { get; set; }
    }
}
