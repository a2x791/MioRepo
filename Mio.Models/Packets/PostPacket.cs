using Mio.Models.Text;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mio.Models.Packets
{
    public class PostPacket
    {
        public Story Story { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
