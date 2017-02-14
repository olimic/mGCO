using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using GCO.Model.Waypoints;

namespace GCO.Model.Geocaching
{
    public enum GeocacheSize
    {
        Micro, Small, Regular, Big, NotChoosen
    }

    public enum GeocacheType
    {
        Traditional, Multi, Earthcache, Mystery, Event, Whereigo, Letterbox, Virtual, Webcam, MegaEvent, TrashOut, NA
    }

    public class GeocacheDescriptionItem
    {
        public string Text { get; set; }
    }

    public class GeocachePicture
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string OriginalPath { get; set; }
    }

    public class GeocacheAttribute
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool YesState { get; set; }

        public string Legend
        {
            get
            {
                if (Description.Length > 0)
                    return Description;
                return Name;
            }
        }

        public string IconPath
        {
            get
            {
                return string.Format("{0}{1}-{2}.png", "Attributes/", Name, YesState ? "yes" : "no");
            }
        }

        public GeocacheAttribute()
        {
            ID = 0;
            Name = string.Empty;
            Description = string.Empty;
            YesState = false;
        }

        public GeocacheAttribute(long id, string name, string desc, bool yes_state)
        {
            ID = id;
            Name = name;
            Description = desc;
            YesState = yes_state;
        }
    }

    public class GeocacheLog
    {
        public DateTime Date { get; set; }
        public string Finder { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string CacheCode { get; set; }
        public string FinderID { get; set; }
        public string FinderIconUrl { get; set; }
        public long FinderLogsCount { get; set; }
        public int LogType { get; set; }

        public string IconUrl
        {
            get
            {
                if (LogType == 0)
                    return "";
                else if (LogType == 2)
                    return "icon_found.png";
                else if (LogType == 3)
                    return "icon_not_found.png";
                else if (LogType == 4)
                    return "appbar.file.rest.png";
                else if (LogType == 22)
                    return "icon_disabled.png";
                else if (LogType == 23)
                    return "icon_enabled.png";
                else if (LogType == 45)
                    return "icon_maintenance.png";
                else if (LogType == 46)
                    return "icon_maintenance_green.png";
                return "icon_found.png";
            }
        }
    }

    public class GeocacheWaypoint : Waypoint
    {
        public double Difficulty { get; set; }
        public double Terrain { get; set; }
        public GeocacheSize Container { get; set; }
        public GeocacheType CacheType { get; set; }

        public string Code { get; set; }
        public string Owner { get; set; }
        public string Hint { get; set; }

        public bool Found { get; set; }
        public bool Available { get; set; }

        public int FavoritePoints { get; set; }

        public bool IsLite { get; set; }

        public DateTime DownloadDate { get; set; }

        public string LongDescription { get; set; }

        public ObservableCollection<GeocacheLog> Logs { get; set; }

        public ObservableCollection<GeocacheDescriptionItem> DescriptionParts { get; set; }

        public ObservableCollection<GeocachePicture> Pictures { get; set; }

        public ObservableCollection<GeocacheAttribute> Attributes { get; set; }

        public List<Waypoint> AdditionalWaypoints { get; set; }

        public long OwnerID { get; set; }

        public GeocacheWaypoint()
            : base()
        {
            Difficulty = 0.0;
            Terrain = 0.0;
            Container = GeocacheSize.NotChoosen;
            CacheType = GeocacheType.Traditional;
            Code = "GC";
            Owner = "---";
            Hint = string.Empty;
            Found = false;
            Available = false;
            IsLite = false;
            FavoritePoints = -1;
            DownloadDate = DateTime.Now;
            LongDescription = string.Empty;
            Logs = new ObservableCollection<GeocacheLog>();
            DescriptionParts = new ObservableCollection<GeocacheDescriptionItem>();
            Pictures = new ObservableCollection<GeocachePicture>();
            Attributes = new ObservableCollection<GeocacheAttribute>();
            AdditionalWaypoints = new List<Waypoint>();
            OwnerID = 0;
        }

        public GeocacheWaypoint(GeocacheWaypoint other)
            : base(other)
        {
            Difficulty = other.Difficulty;
            Terrain = other.Terrain;
            Container = other.Container;
            CacheType = other.CacheType;
            Code = other.Code;
            Owner = other.Owner;
            Hint = other.Hint;
            Found = other.Found;
            Available = other.Available;
            LongDescription = other.LongDescription;
            Logs = new ObservableCollection<GeocacheLog>(other.Logs);
            DescriptionParts = new ObservableCollection<GeocacheDescriptionItem>(other.DescriptionParts);
            Pictures = new ObservableCollection<GeocachePicture>(other.Pictures);
            Attributes = new ObservableCollection<GeocacheAttribute>(other.Attributes);
            AdditionalWaypoints = new List<Waypoint>();
            foreach (Waypoint wp in other.AdditionalWaypoints)
                AdditionalWaypoints.Add(wp.Clone());
            OwnerID = other.OwnerID;
        }

        public override Waypoint Clone()
        {
            GeocacheWaypoint copy = new GeocacheWaypoint(this);
            return copy;
        }

        public override string Title
        {
            get { return Description; }
        }

        public override string SubTitle
        {
            get
            {
                return string.Format("D:{0}, T:{1}, Size:{2}, Fav:{3}", Difficulty, Terrain, GetCacheSizeText(), FavoritePoints);
            }
        }

        protected string GetCacheSizeText()
        {
            switch (Container)
            {
                case GeocacheSize.Small:
                    return "Small";
                case GeocacheSize.Micro:
                    return "Micro";
                case GeocacheSize.Regular:
                    return "Regular";
                case GeocacheSize.Big:
                    return "Big";
            }
            return "Not chosen";
        }

        public static GeocacheSize TextToCacheSize(string name)
        {
            string s = name.ToLower();

            if (s.Equals("small"))
                return GeocacheSize.Small;
            else if (s.Equals("micro"))
                return GeocacheSize.Micro;
            else if (s.Equals("big"))
                return GeocacheSize.Big;
            else if (s.Equals("regular"))
                return GeocacheSize.Regular;
            return GeocacheSize.NotChoosen;
        }

        public static string CacheSizeToText(GeocacheSize size)
        {
            switch (size)
            {
                case GeocacheSize.Small:
                    return "small";
                case GeocacheSize.Micro:
                    return "micro";
                case GeocacheSize.Regular:
                    return "regular";
                case GeocacheSize.Big:
                    return "big";
            }
            return "not choosen";
        }

        public int ExtractPicturesFromDescription()
        {
            int insert_index = 0;

            if (LongDescription != null)
            {
                string[] imgs = LongDescription.Split(new string[] { "<img " }, StringSplitOptions.RemoveEmptyEntries);
                if (imgs.Length > 1)
                {
                    for (int i = 1; i < imgs.Length; i++)
                    {
                        string[] src = imgs[i].Split(new string[] { "src=" }, StringSplitOptions.None);
                        if (src.Length > 1)
                        {
                            string[] path = src[1].Split(new char[] { '"' });
                            {
                                if (path.Length > 2)
                                {
                                    if (path[1].Length > 10)
                                    {
                                        bool duplicate = false;
                                        foreach (GeocachePicture gp in Pictures)
                                        {
                                            string cs1 = gp.Path;
                                            string cs2 = path[1];
                                            cs1 = cs1.Substring(cs1.Length - 10, 10);
                                            cs2 = cs2.Substring(cs2.Length - 10, 10);
                                            if (cs1.Equals(cs2))
                                                duplicate = true;
                                        }
                                        if (!duplicate)
                                        {
                                            GeocachePicture p = new GeocachePicture();
                                            p.OriginalPath = path[1];
                                            p.Path = path[1];
                                            Pictures.Add(p); //.Insert(insert_index, p);
                                            insert_index++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return insert_index;
        }

        public override string ToString(WaypointToStringOption option)
        {
            if (option == WaypointToStringOption.EmailDescription ||
                option == WaypointToStringOption.Normal)
                return string.Format("Name: {0}\r\nSpec: {1}\r\n Link: http://coord.info/{2}", Title, SubTitle, Name);
            
            else if (option == WaypointToStringOption.GPXExport)
            {
                CultureInfo en = new CultureInfo("en-us");

                string attrs = "<groundspeak:attributes>";
                foreach (GeocacheAttribute a in Attributes)
                {
                    string attr = string.Format("<groundspeak:attribute id=\"{0}\" inc=\"{1}\">{2}</groundspeak:attribute>",
                        a.ID, a.YesState ? "1" : "0", a.Name);
                    attrs = string.Format("{0}{1}\r\n", attrs, attr);
                }
                attrs += "</groundspeak:attributes>";

                string logs = "<groundspeak:logs>";
                foreach (GeocacheLog l in Logs)
                {
                    string log = string.Format("<groundspeak:log>" +
                                                "<groundspeak:date>{0}</groundspeak:date>" +
                                                "<groundspeak:type>{1}</groundspeak:type>" +
                                                "<groundspeak:finder>{2}</groundspeak:finder>" +
                                                "<groundspeak:text>{3}</groundspeak:text></groundspeak:log>",
                                                l.Date.ToString(en),
                                                l.Type,
                                                EscapeString(l.Finder),
                                                EscapeString(l.Text));
                    logs = string.Format("{0}{1}\r\n", logs, log);
                }
                logs += "</groundspeak:logs>";

                string gc = string.Format("<groundspeak:cache available=\"{0}\" xmlns:groundspeak=\"http://www.groundspeak.com/cache/1/0/1\">" +
                                            "<groundspeak:name>{1}</groundspeak:name>" +
                                            "<groundspeak:owner>{2}</groundspeak:owner>" +
                                            "<groundspeak:type>{3}</groundspeak:type>" +
                                            "<groundspeak:container>{4}</groundspeak:container>" +
                                            "<groundspeak:difficulty>{5}</groundspeak:difficulty>" +
                                            "<groundspeak:terrain>{6}</groundspeak:terrain>" +
                                            "<groundspeak:long_description html=\"True\">{7}</groundspeak:long_description>" +
                                            "<groundspeak:encoded_hints>{8}</groundspeak:encoded_hints>" +
                                            "{9}{10}</groundspeak:cache>",
                                            Available,
                                            EscapeString(Title),
                                            EscapeString(Owner),
                                            GetGPXTypeName().Replace(" Found", ""),
                                            CacheSizeToText(Container),
                                            Difficulty.ToString(en),
                                            Terrain.ToString(en),
                                            EscapeString(LongDescription),
                                            EscapeString(Hint),
                                            attrs,
                                            logs);

                string gpx = string.Format("<wpt lat=\"{0}\" lon=\"{1}\">" +
                                            "<name>{2}</name>" +
                                            "<desc>{3}</desc>" +
                                            "<cmt>{4}</cmt>" +
                                            "<url>{5}</url>" +
                                            "<urlname>{6}</urlname>" +
                                            "<type>{7}</type>" +
                                            "<sym>{8}</sym>" +
                                            "<ele>{9}</ele>" +
                                            "{10}</wpt>",
                                            Latitude.ToString(en),
                                            Longitude.ToString(en),
                                            Code,
                                            EscapeString(Description),
                                            EscapeString(Comment),
                                            Link.Href,
                                            EscapeString(Link.Text),
                                            GetGPXTypeName().Replace(" Found", ""),
                                            Found ? "Geocache Found" : "Geocache",
                                            Elevation,
                                            gc);

                foreach (Waypoint wpa in AdditionalWaypoints)
                {
                    string stage = wpa.ToString(WaypointToStringOption.GPXExport);
                    gpx = gpx + "\r\n" + stage;
                }

                return gpx;
            }

            return base.ToString(option);
        }
            
        protected string GetGPXTypeName()
        {
            switch (WaypointType.TypeEnum)
            {
                case WaypointTypeEnum.GeocacheTraditional:
                    return "Geocache|Traditional Cache";
                case WaypointTypeEnum.GeocacheTraditionalFound:
                    return "Geocache|Traditional Cache Found";

                case WaypointTypeEnum.GeocacheMulti:
                    return "Geocache|Multi-cache";
                case WaypointTypeEnum.GeocacheMultiFound:
                    return "Geocache|Multi-cache Found";

                case WaypointTypeEnum.GeocacheUnknown:
                    return "Geocache|Mystery Cache";
                case WaypointTypeEnum.GeocacheUnknownFound:
                    return "Geocache|Mystery Cache Found";

                case WaypointTypeEnum.GeocacheEarth:
                    return "Geocache|Earthcache";
                case WaypointTypeEnum.GeocacheEarthFound:
                    return "Geocache|Earthcache Found";

                case WaypointTypeEnum.GeocacheWhereigo:
                    return "Geocache|Wherigo Cache";
                case WaypointTypeEnum.GeocacheWhereigoFound:
                    return "Geocache|Wherigo Cache Found";

                case WaypointTypeEnum.GeocacheTrashOut:
                    return "Geocache|Cache In Trash Out Event";
                case WaypointTypeEnum.GeocacheTrashOutFound:
                    return "Geocache|Cache In Trash Out Event Found";

                case WaypointTypeEnum.GeocacheLetterbox:
                    return "Geocache|Letterbox Hybrid";
                case WaypointTypeEnum.GeocacheLetterboxFound:
                    return "Geocache|Letterbox Hybrid Found";

                case WaypointTypeEnum.GeocacheMegaEvent:
                    return "Geocache|Mega-Event Cache";
                case WaypointTypeEnum.GeocacheMegaEventFound:
                    return "Geocache|Mega-Event Cache Found";

                case WaypointTypeEnum.GeocacheVirtual:
                    return "Geocache|Virtual Cache";
                case WaypointTypeEnum.GeocacheVirtualFound:
                    return "Geocache|Virtual Cache Found";

                case WaypointTypeEnum.GeocacheWebcam:
                    return "Geocache|Webcam Cache";
                case WaypointTypeEnum.GeocacheWebcamFound:
                    return "Geocache|Webcam Cache Found";

                case WaypointTypeEnum.GeocacheStation:
                    return "Waypoint|Question to Answer";
                case WaypointTypeEnum.ParkingArea:
                    return "Waypoint|Parking Area";
            }
            return "Standard";
        }
    }
}
