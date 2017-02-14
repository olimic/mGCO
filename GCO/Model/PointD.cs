using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCO.Model
{
    public class PointD
    {
        protected const double DEG_TO_RAD = 0.017453292519943295769236907684886;
        protected const double EARTH_RADIUS_IN_KM = 6372.797560856;

        protected double mX = 0.0;
        protected double mY = 0.0;

        public PointD() { }

        public PointD(double x, double y)
        {
            mX = x;
            mY = y;
        }

        public PointD(PointD other)
        {
            mX = other.X;
            mY = other.Y;
        }

        #region Properties
        public double X
        {
            get { return mX; }
            set { mX = value; }
        }

        public double Y
        {
            get { return mY; }
            set { mY = value; }
        }

        public double Latitude
        {
            get { return Y; }
            set { Y = value; }
        }

        public double Longitude
        {
            get { return X; }
            set { X = value; }
        }
        #endregion

        public void Set(PointD p)
        {
            mX = p.mX;
            mY = p.mY;
        }

        public void Set(double x, double y, bool check_latlon = false)
        {
            mX = x;
            mY = y;
            if (check_latlon)
            {
                if (mX > 180.0)
                    mX = 180.0;
                if (mX < -180.0)
                    mX = -180.0;
                if (mY > 90.0)
                    mY = 90.0;
                if (mY < -90.0)
                    mY = -90.0;
            }
        }

        public void Set(int x, int y)
        {
            mX = (double)x * 1e-6;
            mY = (double)y * 1e-6;
        }

        public void Add(double x, double y)
        {
            mX += x;
            mY += y;
        }

        public void Add(PointD other)
        {
            mX += other.X;
            mY += other.Y;
        }

        public void Sub(PointD other)
        {
            mX -= other.mX;
            mY -= other.mY;
        }

        public double Length()
        {
            return Math.Sqrt(mX * mX + mY * mY);
        }

        public double Normalize()
        {
            double l = Length();

            if (Math.Abs(l) < double.Epsilon)
                return 0.0;

            double l2 = 1.0 / l;
            mX *= l2;
            mY *= l2;

            return l;
        }

        public double Distance(PointD other)
        {
            double dx = other.X - mX;
            double dy = other.Y - mY;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public double DistanceMeters(PointD other)  // [m]
        {
            double latitudeArc = (Latitude - other.Latitude) * DEG_TO_RAD;
            double longitudeArc = (Longitude - other.Longitude) * DEG_TO_RAD;
            double latitudeH = Math.Sin(latitudeArc * 0.5);
            double longitudeH = Math.Sin(longitudeArc * 0.5);

            latitudeH *= latitudeH;     // ^2
            longitudeH *= longitudeH;   // ^2

            double tmp = Math.Cos(Latitude * DEG_TO_RAD) * Math.Cos(other.Latitude * DEG_TO_RAD);
            double d = 2.0 * Math.Sin(Math.Sqrt(latitudeH + tmp * longitudeH)) * EARTH_RADIUS_IN_KM * 1000.0;

            return d;
        }

        public void Scale(double scale)
        {
            mX *= scale;
            mY *= scale;
        }

        public double Scalar(PointD other)
        {
            return mX * other.mX + mY * other.mY;
        }

        public void Invert()
        {
            mX = -mX;
            mY = -mY;
        }

        public void Rotate(double radian)
        {
            double d1 = mX;
            double d2 = mY;
            double cos = (double)Math.Cos(radian);
            double sin = (double)Math.Sin(radian);
            mX = d1 * cos - d2 * sin;
            mY = d1 * sin + d2 * cos;
        }

        public void RotateDegree(double degree)
        {
            Rotate(degree * 0.017453292519943295769236907684886);
        }

        public double Radian()
        {
            if (mY >= 0)
                return Math.Acos(mX);
            else
                return Math.PI * 2.0 - Math.Acos(mX);
        }

        public double Degree()
        {
            return Radian() * (180.0 / Math.PI);
        }

        public bool IsZero()
        {
            return (mX == 0.0 && mY == 0.0);
        }
    }
}
