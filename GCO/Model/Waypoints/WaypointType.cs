using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCO.Model.Waypoints
{
    public class WaypointType
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string IconFile { get; set; }
        public string Symbol { get; set; }
        public string Type { get; set; }
        public WaypointTypeEnum TypeEnum { get; set; }
        //public WriteableBitmap Icon { get; set; }
        public Dictionary<string, object> Properties = new Dictionary<string, object>();
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        public WaypointType()
        {
            Key = string.Empty;
            Name = string.Empty;
            IconFile = string.Empty;
            Symbol = string.Empty;
            Type = string.Empty;
            TypeEnum = WaypointTypeEnum.Standard;
            //Icon = null;
            OffsetX = 0;
            OffsetY = 0;
        }
    }
}
