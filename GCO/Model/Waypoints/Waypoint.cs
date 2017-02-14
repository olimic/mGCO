using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace GCO.Model.Waypoints
{    
    public enum WaypointToStringOption
    {
        Normal, EmailDescription, GPXExport
    }

    public class Waypoint
    {
        protected PointD mPosition = new PointD();
        protected double mElevation = 0.0;
        protected string mName = string.Empty;
        protected DateTime mDateTime = System.DateTime.MinValue;
        protected string mDescription = string.Empty;
        protected string mComment = string.Empty;
        protected string mSymbol = string.Empty;
        protected string mSource = string.Empty;
        protected Link mLink = new Link();
        protected string mType = string.Empty;
        protected string mExtension = string.Empty;
        protected WaypointType mWaypointType = null;

        protected const double DEG_TO_RAD = 0.017453292519943295769236907684886;
        protected const double EARTH_RADIUS_IN_KM = 6372.797560856;

        public Waypoint() { }

        public Waypoint(Waypoint other)
        {
            mPosition.Set(other.mPosition);
            mElevation = other.mElevation;
            mName = other.mName;
            mDateTime = other.mDateTime;
            mDescription = other.mDescription;
            mComment = other.mComment;
            mSymbol = other.mSymbol;
            mSource = other.mSource;
            mLink = new Link(other.mLink);
            mType = other.mType;
            mExtension = other.mExtension;
            mWaypointType = other.mWaypointType;
        }

        public Waypoint(double lon, double lat)
        {
            mPosition.X = lon;
            mPosition.Y = lat;
        }

        public virtual Waypoint Clone()
        {
            Waypoint copy = new Waypoint(this);
            return copy;
        }

        #region Properties
        /// <summary>
        /// Position in world coordinates
        /// </summary>
        public PointD Position
        {
            get { return mPosition; }
            set { mPosition.Set(value); }
        }

        public double Latitude
        {
            get { return mPosition.Y; }
            set { mPosition.Y = value; }
        }

        public double Longitude
        {
            get { return mPosition.X; }
            set { mPosition.X = value; }
        }

        public double Elevation
        {
            get { return mElevation; }
            set { mElevation = value; }
        }

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public DateTime DateTime
        {
            get { return mDateTime; }
            set { mDateTime = value; }
        }

        public string Description
        {
            get { return mDescription; }
            set { mDescription = value; }
        }

        public string Comment
        {
            get { return mComment; }
            set { mComment = value; }
        }

        public string Symbol
        {
            get { return mSymbol; }
            set { mSymbol = value; }
        }

        public string Source
        {
            get { return mSource; }
            set { mSource = value; }
        }

        public Link Link
        {
            get { return mLink; }
            set { mLink = value; }
        }

        public string Type
        {
            get { return mType; }
            set { mType = value; }
        }

        public string Extension
        {
            get { return mExtension; }
            set { mExtension = value; }
        }

        public WaypointType WaypointType
        {
            get { return mWaypointType; }
            set { mWaypointType = value; }
        }

        public virtual string Title
        {
            get { return Name; }
        }

        public virtual string SubTitle
        {
            get { return Comment; }
        }
        #endregion

        /// <summary>
        /// calculate distance on earth
        /// </summary>
        /// <param name="other">the other point</param>
        /// <param name="flat">without curve of earth surface</param>
        /// <returns>distance in Meter</returns>
        public double Distance(Waypoint other, bool flat = true)  // [m]
        {
            return Distance(other.Position, other.Elevation, flat);
        }

        public double Distance(PointD other, double elevation, bool flat = true)  // [m]
        {
            // Version 1 -----------------
            double latitudeArc = (mPosition.Latitude - other.Latitude) * DEG_TO_RAD;
            double longitudeArc = (mPosition.Longitude - other.Longitude) * DEG_TO_RAD;
            double latitudeH = Math.Sin(latitudeArc * 0.5);
            double longitudeH = Math.Sin(longitudeArc * 0.5);

            latitudeH *= latitudeH;     // ^2
            longitudeH *= longitudeH;   // ^2

            double tmp = Math.Cos(mPosition.Latitude * DEG_TO_RAD) * Math.Cos(other.Latitude * DEG_TO_RAD);
            double d = 2.0 * Math.Sin(Math.Sqrt(latitudeH + tmp * longitudeH)) * EARTH_RADIUS_IN_KM * 1000.0;

            if (flat)
                return d;

            double dEle = (mElevation - elevation);

            return Math.Sqrt(d * d + dEle * dEle);
        }

        public double Speed(Waypoint other)     // [m/s]
        {
            if (mDateTime > System.DateTime.MinValue && other.DateTime > System.DateTime.MinValue)
            {
                double dist = Distance(other, true);

                if (dist > 0.0)
                {
                    double seconds = Math.Abs(mDateTime.Subtract(other.DateTime).TotalSeconds);
                    return dist / seconds;
                }
            }
            return 0.0;
        }

        public override string ToString()
        {
            return "(Lon: " + mPosition.Longitude.ToString("0.0000000°") + "  Lat: " + mPosition.Latitude.ToString("0.0000000°") + ")";
        }

        public virtual string ToString(WaypointToStringOption option)
        {
            if (option == WaypointToStringOption.Normal)
                return Name;

            if (option == WaypointToStringOption.GPXExport)
            {
                CultureInfo en = new CultureInfo("en-us");

                string gpx = string.Format("<wpt lat=\"{0}\" lon=\"{1}\">" +
                                            "<name>{2}</name>" +
                                            "<desc>{3}</desc>" +
                                            "<cmt>{4}</cmt>" +
                                            "<url>{5}</url>" +
                                            "<urlname>{6}</urlname>" +
                                            "<type>{7}</type>" +
                                            "<sym>{8}</sym>" +
                                            "<ele>{9}</ele></wpt>",
                                            Latitude.ToString(en),
                                            Longitude.ToString(en),
                                            EscapeString(Name),
                                            EscapeString(Description),
                                            EscapeString(Comment),
                                            Link.Href,
                                            EscapeString(Link.Text),
                                            Type,
                                            Symbol,
                                            Elevation);
                return gpx;
            }

            return string.Format("Name: {0},  Description: {1}", Name, Description);
        }

        protected string EscapeString(string content)
        {
            //return Uri.EscapeUriString(content);
            if (content != null)
                return content.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;");
            return string.Empty;
        }
    }
}
