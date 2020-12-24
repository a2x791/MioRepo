using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Sale
{
    public class ProductOption
    {
        public int ID { get; set; }
        [Required]
        public string ImagePaths { get; set; }

        public string OptionLabel1 { get; set; }
        public string Options1 { get; set; }

        public string OptionLabel2 { get; set; }
        public string Options2 { get; set; }

        public string OptionLabel3 { get; set; }
        public string Options3 { get; set; }
    }
}
