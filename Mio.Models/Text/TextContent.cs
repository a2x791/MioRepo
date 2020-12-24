using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Text
{
    public abstract class TextContent
    {
        public int ID { get; set; }
        public string Content { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        public int Upvotes { get; set; } = 0;
        public int NumComments { get; set; } = 0;

        [Required]
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
