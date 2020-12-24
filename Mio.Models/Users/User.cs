using System;
using System.ComponentModel.DataAnnotations;

namespace Mio.Models.Users
{
    public class User
    {
        public string ID { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [MinLength(4)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MinLength(1)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(1)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; } = DateTime.Now;

        public string ImagePath { get; set; }

        public string BloodType { get; set; }

        public float Rating { get; set; }

        [Required]
        public int AccountTypeID { get; set; }
        public AccountType AccountType { get; set; }
    }
}
