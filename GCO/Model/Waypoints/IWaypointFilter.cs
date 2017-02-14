using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCO.Model.Waypoints
{
    public interface IWaypointFilter
    {
        bool IsFilterOut(Waypoint waypoint);
    }
}
