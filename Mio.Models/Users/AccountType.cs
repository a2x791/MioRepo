using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Users
{
    public class AccountType
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
