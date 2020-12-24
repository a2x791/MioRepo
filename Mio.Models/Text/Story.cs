using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mio.Models.Text
{
    public class Story : TextContent
    {
        [Required]
        public string Title { get; set; }
        public int SpaceID { get; set; }
        public Space Space { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
