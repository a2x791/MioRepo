using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mio.Models.Text
{
    public class Comment : TextContent
    {
        [Required]
        public int ParentContentID { get; set; }
    }
}
