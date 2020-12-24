using Mio.Models.Sale;
using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Relations
{
    public class ProductRentals
    {
        public int ID { get; set; }
        [Required]
        public int ProductID { get; set; }
        public Product Product { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public string UserID { get; set; }
    }
}
