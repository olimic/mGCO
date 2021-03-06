﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeocachingLiveAPI
{
    public static class Constants
    {
        public static string RestUrl = "aHR0cHM6Ly9hcGkuZ3JvdW5kc3BlYWsuY29tL0xpdmVWNi9nZW9jYWNoaW5nLnN2Yw==";

        public static string GetYourUserProfileRequest = "<GetYourUserProfileRequest xmlns=\"http://www.geocaching.com/Geocaching.Live/data\">" +
                                          "<AccessToken>{0}</AccessToken>" +
                                          "<ProfileOptions>" +
                                            "<ChallengesData xmlns = \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >false</ChallengesData>" +
                                            "<FavoritePointsData xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >false</FavoritePointsData>" +
                                            "<GeocacheData xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >false</GeocacheData>" +
                                            "<PublicProfileData xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >true</PublicProfileData>" +
                                            "<SouvenirData xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >false</SouvenirData>" +
                                            "<TrackableData xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >false</TrackableData>" +
                                            "<EmailData xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >false</EmailData>" +
                                          "</ProfileOptions>" +
                                          "<DeviceInfo>" +
                                            "<ApplicationCurrentMemoryUsage xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >214</ApplicationCurrentMemoryUsage>" +
                                            "<ApplicationPeakMemoryUsage xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >214</ApplicationPeakMemoryUsage>" +
                                            "<ApplicationSoftwareVersion xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >1.0</ApplicationSoftwareVersion>" +
                                            "<DeviceManufacturer xmlns = \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >Microsoft</DeviceManufacturer>" +
                                            "<DeviceName xmlns = \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >Lumia 950</DeviceName>" +
                                            "<DeviceOperatingSystem xmlns = \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >WP8.1</DeviceOperatingSystem>" +
                                            "<DeviceTotalMemoryInMB xmlns = \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" > 742 </DeviceTotalMemoryInMB>" +
                                            "<DeviceUniqueId xmlns= \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >PC</DeviceUniqueId>" +
                                            "<MobileHardwareVersion xmlns = \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >PC1</MobileHardwareVersion>" +
                                            "<WebBrowserVersion xmlns = \"http://schemas.datacontract.org/2004/07/Tucson.Geocaching.WCF.API.Geocaching.Types\" >No</WebBrowserVersion>" +
                                          "</DeviceInfo>" +
                                        "</GetYourUserProfileRequest>";
    }
}
