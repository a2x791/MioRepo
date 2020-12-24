using Mio.Models.Relations;
using System.ComponentModel.DataAnnotations;

namespace Mio.Models.Sale
{
    public class Service : Product
    {
        [Required]
        public string ImagePath { get; set; }
    }
}
