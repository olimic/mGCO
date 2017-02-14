using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCO.Model;
using GCO.Model.Geocaching;
using GCO.Service;
using GCOTestUI.GCLiveAPI;

namespace GCOTestUI.Service
{
    public class GCLiveAPISoapService : IGeocachingService
    {
        public UserAccess mUserAccess;

        public LiveClient LiveClient = null;

        public GCLiveAPISoapService(UserAccess useraccess)
        {
            mUserAccess = useraccess;
            LiveClient = new LiveClient();
        }

        public Task<GeocacheWaypoint> GetCacheDetails(string cache_code)
        {
            throw new NotImplementedException();
        }

        public Task<List<GeocacheWaypoint>> SearchCaches(BoundingBox bounding_box, WayPointFilterGeocache filter)
        {
            throw new NotImplementedException();
        }
        
        public async Task<User> GetOwnUserDetails()
        {
            try
            {
                GetYourUserProfileRequest request = new GetYourUserProfileRequest();
                request.AccessToken = mUserAccess.AccessToken;
                request.DeviceInfo = new DeviceData();

                GetYourUserProfileResponse res = await LiveClient.GetYourUserProfileAsync(request);

                User user = new User();
                user.AvatarUrl = res.Profile.User.AvatarUrl;
                user.FoundCaches = res.Profile.User.FindCount.GetValueOrDefault(0);
                user.Guid = res.Profile.User.PublicGuid.ToString();
                user.UserName = res.Profile.User.UserName;

                return user;
            }
            catch(Exception ex)
            {
                throw ex;   // ToDo: log4net 
            }
        }

        public Task<bool> Ping()
        {
            throw new NotImplementedException();
        }
    }
}
