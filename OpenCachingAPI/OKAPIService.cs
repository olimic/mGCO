using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCO.Model;
using GCO.Model.Geocaching;
using GCO.Service;

namespace OpenCachingAPI
{
    public class OKAPIService : IGeocachingService
    {
        public Task<GeocacheWaypoint> GetCacheDetails(string cache_code)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetOwnUserDetails()
        {
            throw new NotImplementedException();
        }

        public Task<List<GeocacheWaypoint>> SearchCaches(BoundingBox bounding_box, WayPointFilterGeocache filter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Ping()
        {
            throw new NotImplementedException();
        }
    }
}
