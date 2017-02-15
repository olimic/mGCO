using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;
using GCO.Model;
using GCO.Model.Geocaching;
using GCO.Service;

namespace GeocachingLiveAPI
{
    public class GCLiveAPIRestService : IGeocachingService
    {
        HttpClient mClient;
        UserAccess mUserAccess;

        public GCLiveAPIRestService(UserAccess useraccess)
        {
            mUserAccess = useraccess;
            mClient = new HttpClient();
            mClient.MaxResponseContentBufferSize = 256000;
        }

        public Task<GeocacheWaypoint> GetCacheDetails(string cache_code)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetOwnUserDetails()
        {
            try
            {
                User user = new User();

                var uri = new Uri(GetApiUrl() + "/GetYourUserProfile");
                string xml = string.Format(Constants.GetYourUserProfileRequest, mUserAccess.AccessToken);
                HttpContent post = new StringContent(xml, Encoding.UTF8, "application/xml");
                var response = await mClient.PostAsync(uri, post);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    XDocument xd = XDocument.Parse(content);

                    XElement root = xd.Root;
                    XNamespace ns = root.GetDefaultNamespace();
                    XElement prof = root.Element(ns + "Profile");
                    XNamespace nsa = "http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types";
                    XElement xuser = prof.Element(nsa + "User");

                    user.UserName = xuser.Element(nsa + "UserName").Value;
                    user.FoundCaches = System.Convert.ToInt32(xuser.Element(nsa + "FindCount").Value);
                    user.AvatarUrl = xuser.Element(nsa + "AvatarUrl").Value;
                }
                return user;
            }
            catch   //(Exception ex)
            {
                return null;    // ToDo: log4net
            }
        }

        public Task<List<GeocacheWaypoint>> SearchCaches(BoundingBox bounding_box, WayPointFilterGeocache filter)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Ping()
        {
            try
            {
                var uri = new Uri(GetApiUrl() + "/Ping");
                var response = await mClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                    return true;
            }
            catch{ }
            return false;
        }

        private string GetApiUrl()
        {
            var base64EncodedBytes = System.Convert.FromBase64String(Constants.RestUrl);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
    }
}
