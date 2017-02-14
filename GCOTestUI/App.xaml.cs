using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using GCO.Model.Geocaching;
using GCO.Service;
using GCOTestUI.GCLiveAPI;
using GCOTestUI.Service;
using GeocachingLiveAPI;

namespace GCOTestUI
{
    /// <summary>
    /// Interaktionslogik für "App.xaml"
    /// </summary>
    public partial class App : Application
    {
        public static UserAccess UserAccess = new UserAccess();
        public static User User = new User();

        public static IGeocachingService GeocachingService;

        public App()
        {
            UserAccess.AccessToken = ""; 
                        
            if (GeocachingService == null)
            {
                GeocachingService = new GCLiveAPIRestService(UserAccess);
                //GeocachingService = new GCLiveAPISoapService(UserAccess.AccessToken, UserAccess.UniqueID);
            }

        }
    }
}
