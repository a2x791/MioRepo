using System.ComponentModel.DataAnnotations;

namespace Mio.Models.Users
{
    public class Address
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(40, ErrorMessage = "Address line 1 can contain at most 40 characters")]
        public string AddressLine1 { get; set; }
        [MaxLength(40, ErrorMessage = "Address line 2 can contain at most 20 characters")]
        public string AddressLine2 { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "City can contain at most 20 characters")]
        public string City { get; set; }
        [Required]
        [MaxLength(20, ErrorMessage = "State can contain at most 20 characters")]
        public string State { get; set; }
        [Required]
        public int Zipcode { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Country can contain at most 30 characters")]
        public string Country { get; set; }

        public bool Default { get; set; }

        [Required]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
