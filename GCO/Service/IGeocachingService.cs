using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCO.Model;
using GCO.Model.Geocaching;

namespace GCO.Service
{
    public interface IGeocachingService
    {
        Task<User> GetOwnUserDetails();

        Task<GeocacheWaypoint> GetCacheDetails(string cache_code);

        Task<List<GeocacheWaypoint>> SearchCaches(BoundingBox bounding_box, WayPointFilterGeocache filter);

        Task<bool> Ping();
    }
}
