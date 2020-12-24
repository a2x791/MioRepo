using System.Collections.Generic;

namespace Mio.Models.Packets
{
    public class ServicePacket
    {
        public int ID { get; set; }
        public string SearchString { get; set; }
        public string UserID { get; set; }
        public IEnumerable<int> Ids { get; set; }

    }
}
