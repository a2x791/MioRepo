using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Sale
{
    public abstract class Product
    {
        public int ID { get; set; }
        public double Rating { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }

        public int NumberReviews { get; set; } = 0;
        [Required]
        public string UserID { get; set; }
        public User User { get; set; }

        [Required]
        public int ProductTypeID { get; set; }
        public ProductType ProductType { get; set; }
    }
}
