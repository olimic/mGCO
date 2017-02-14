using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCO.Model
{
    public class BoundingBox
    {
        #region Member
        protected double mLatMin = 0.0;
        protected double mLatMax = 0.0;
        protected double mLonMin = 0.0;
        protected double mLonMax = 0.0;
        #endregion

        protected const double DEG_TO_RAD = 0.017453292519943295769236907684886;
        protected const double EARTH_RADIUS_IN_KM = 6372.797560856;

        #region Ctor
        public BoundingBox()
        {
            InitInvalid();
        }

        public BoundingBox(BoundingBox other)
        {
            Set(other);
        }

        public BoundingBox(PointD center, double radius)
        {
            Set(center.Latitude - radius, center.Latitude + radius, center.Longitude - radius, center.Longitude + radius);
        }
        #endregion

        #region Properties
        public double LatMin
        {
            get { return this.mLatMin; }
            set { this.mLatMin = value; }
        }

        public double LatMax
        {
            get { return this.mLatMax; }
            set { this.mLatMax = value; }
        }

        public double LonMin
        {
            get { return this.mLonMin; }
            set { this.mLonMin = value; }
        }

        public double LonMax
        {
            get { return this.mLonMax; }
            set { this.mLonMax = value; }
        }
        #endregion

        #region Functions
        /// <summary>
        /// expand the region of the bounding box to enclose this point
        /// </summary>
        /// <param name="pt">PointD</param>
        public void Expand(PointD pt)
        {
            if (pt.Y > mLatMax) mLatMax = pt.Y;
            if (pt.X > mLonMax) mLonMax = pt.X;
            if (pt.Y < mLatMin) mLatMin = pt.Y;
            if (pt.X < mLonMin) mLonMin = pt.X;
        }

        public void Scal(double factor, PointD center)
        {
            PointD cen = center;
            if (cen == null)
                cen = GetCenter();
            mLatMin = cen.Latitude - (cen.Latitude - mLatMin) * factor;
            mLonMin = cen.Longitude - (cen.Longitude - mLonMin) * factor;
            mLatMax = cen.Latitude + (mLatMax - cen.Latitude) * factor;
            mLonMax = cen.Longitude + (mLonMax - cen.Longitude) * factor;
        }

        public void Set(double lat_min, double lat_max, double lon_min, double lon_max)
        {
            mLatMin = lat_min;
            mLatMax = lat_max;
            mLonMin = lon_min;
            mLonMax = lon_max;
        }

        public void Set(int lat_min, int lon_min, int lat_max, int lon_max)
        {
            mLatMin = (double)lat_min * 1e-6;
            mLatMax = (double)lat_max * 1e-6;
            mLonMin = (double)lon_min * 1e-6;
            mLonMax = (double)lon_max * 1e-6;
        }

        public void Set(BoundingBox other)
        {
            mLatMin = other.mLatMin;
            mLatMax = other.mLatMax;
            mLonMin = other.mLonMin;
            mLonMax = other.mLonMax;
        }

        public void InitInvalid()
        {
            mLatMin = 1e6f;
            mLatMax = -1e6f;
            mLonMin = 1e6f;
            mLonMax = -1e6f;
        }

        public double GetRadius()
        {
            double a = LonMax - LonMin;
            double b = LatMax - LatMin;
            return Math.Sqrt(a * a + b * b) * 0.5;
        }

        public PointD GetRadiusPoint()
        {
            PointD c = GetCenter();
            c.Add(0, LatMax - c.Latitude);
            return c;
        }

        public double GetRadiusOnEarthInMeter()
        {
            PointD c = GetCenter();
            PointD o = GetRadiusPoint();

            // Version 1 -----------------
            double latitudeArc = (c.Latitude - o.Latitude) * DEG_TO_RAD;
            double longitudeArc = (c.Longitude - o.Longitude) * DEG_TO_RAD;
            double latitudeH = Math.Sin(latitudeArc * 0.5);
            double longitudeH = Math.Sin(longitudeArc * 0.5);

            latitudeH *= latitudeH;     // ^2
            longitudeH *= longitudeH;   // ^2

            double tmp = Math.Cos(c.Latitude * DEG_TO_RAD) * Math.Cos(o.Latitude * DEG_TO_RAD);
            double d = 2.0 * Math.Sin(Math.Sqrt(latitudeH + tmp * longitudeH)) * EARTH_RADIUS_IN_KM * 1000.0;

            return d;
        }

        public PointD GetCenter()
        {
            PointD c = new PointD();
            c.Set((LonMax + LonMin) * 0.5, (LatMax + LatMin) * 0.5);
            return c;
        }

        public bool IsIn(double lat, double lon)
        {
            if (lat >= mLatMin && lat <= mLatMax && lon >= mLonMin && lon <= mLonMax)
                return true;
            return false;
        }

        public bool IsIn(BoundingBox bb)
        {
            int mask = 15;
            mask &= GetClipMask(bb.LonMin, bb.LatMin);
            mask &= GetClipMask(bb.LonMin, bb.LatMax);
            mask &= GetClipMask(bb.LonMax, bb.LatMin);
            mask &= GetClipMask(bb.LonMax, bb.LatMax);
            return (mask == 0);
        }

        protected int GetClipMask(double x, double y)
        {
            int mask = 0;
            if (x < LonMin) mask |= 1;
            if (y < LatMin) mask |= 2;
            if (x > LonMax) mask |= 4;
            if (y > LatMax) mask |= 8;
            return mask;
        }

        public bool IsEnclosed(BoundingBox bb)
        {
            if (IsIn(bb.LatMin, bb.LonMin) &&
                IsIn(bb.LatMin, bb.LonMax) &&
                IsIn(bb.LatMax, bb.LonMin) &&
                IsIn(bb.LatMax, bb.LonMax))
                return true;
            return false;
        }

        public bool IsEqual(BoundingBox bb, double tolerance = 0.00001)
        {
            if (Math.Abs(bb.LatMin - LatMin) <= tolerance &&
                Math.Abs(bb.LatMax - LatMax) <= tolerance &&
                Math.Abs(bb.LonMin - LonMin) <= tolerance &&
                Math.Abs(bb.LonMax - LonMax) <= tolerance)
                return true;
            return false;
        }

        public bool IsInvalid()
        {
            if (mLatMin < -90.0 || mLatMin > 90.0) return true;
            if (mLatMax < -90.0 || mLatMax > 90.0) return true;
            if (mLonMin < -180.0 || mLonMin > 180.0) return true;
            if (mLonMax < -180.0 || mLonMax > 180.0) return true;
            return false;
        }

        public override string ToString()
        {
            return string.Format("({0};{1};{2};{3})", mLatMin.ToString(), mLatMax.ToString(), mLonMin.ToString(), mLonMax.ToString());
        }
        #endregion
    }
}
