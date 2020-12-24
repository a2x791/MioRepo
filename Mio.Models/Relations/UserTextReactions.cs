using Mio.Models.Text;
using Mio.Models.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Relations
{
    public class UserTextReactions
    {
        [Required]
        public int ID { get; set; }
        public bool Liked { get; set; } = false;
        public bool Commented { get; set; } = false;
        [Required]
        public int TextContentID { get; set; }
        public TextContent TextContent { get; set; }
        [Required]
        public string UserID { get; set; }
    }
}
