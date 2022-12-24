using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCOM.HomeMade
{
    internal abstract class Device
    {
        public string internetServer { get; set; }
        public string soloServer { get; set; }
        public bool UPS { get; set; }
        public bool Internet { get; set; }
        public string UPSURL { get; set; }
        public string UPSSearch { get; set; }
        public int rainSensor { get; set; }

    }
}
