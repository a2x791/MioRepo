using Mio.Models.Relations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mio.Models
{
    public class SessionState
    {
        public IDictionary<int, UserTextReactions> UserTextInteractions { get; set; }
    }
}
