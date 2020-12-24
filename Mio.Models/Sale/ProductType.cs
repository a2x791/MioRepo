using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Sale
{
    public class ProductType
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        public int NumberProducts { get; set; } = 0;
    }
}
