using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GCO.Model.Geocaching;
using GCO.Service;
using GeocachingLiveAPI;

using Xamarin.Forms;

namespace XamarinForms
{
    public partial class App : Application
    {
        public static UserAccess UserAccess = new UserAccess();
        public static User User = new User();

        public static IGeocachingService GeocachingService;

        public App()
        {
            InitializeComponent();

            UserAccess.AccessToken = "<insert your accesstoken here>";

            if (GeocachingService == null)
                GeocachingService = new GCLiveAPIRestService(UserAccess);

            MainPage = new XamarinForms.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
