using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GCO.Model.Waypoints;

namespace GCO.Model.Geocaching
{
    public class WayPointFilterGeocache : IWaypointFilter
    {
        protected int mDifficultyFrom;
        protected int mDifficultyTo;
        protected int mTerrainFrom;
        protected int mTerrainTo;
        protected int mSizeFrom;
        protected int mSizeTo;

        public bool CacheTypeTraditional { get; set; }
        public bool CacheTypeMulti { get; set; }
        public bool CacheTypeUnknown { get; set; }
        public bool CacheTypeEarth { get; set; }
        public bool CacheTypeEvent { get; set; }
        public bool CacheTypeWhereigo { get; set; }
        public bool CacheTypeVirtual { get; set; }
        public bool CacheTypeWebcam { get; set; }
        public bool CacheTypeLetterbox { get; set; }
        public bool CacheTypeMegaEvent { get; set; }
        public bool CacheTypeTrashOut { get; set; }

        public bool HideMyFinds { get; set; }
        public bool HideMyCaches { get; set; }
        public bool HideInactiveCaches { get; set; }
        public bool ShowAdditionalWaypoints { get; set; }

        public bool ContainsTrackables { get; set; }

        public int MinFavoritePoints { get; set; }

        public string Name { get; set; }
        public string Owner { get; set; }

        public bool CacheSizeMicro { get; set; }
        public bool CacheSizeSmall { get; set; }
        public bool CacheSizeRegular { get; set; }
        public bool CacheSizeBig { get; set; }

        #region Properties
        public int DifficultyFrom
        {
            get
            {
                return mDifficultyFrom;
            }
            set
            {
                if (value >= 1 && value <= 5)
                    mDifficultyFrom = value;
            }
        }

        public int DifficultyTo
        {
            get
            {
                return mDifficultyTo;
            }
            set
            {
                if (value >= 1 && value <= 5)
                    mDifficultyTo = value;
            }
        }

        public int TerrainFrom
        {
            get
            {
                return mTerrainFrom;
            }
            set
            {
                if (value >= 1 && value <= 5)
                    mTerrainFrom = value;
            }
        }

        public int TerrainTo
        {
            get
            {
                return mTerrainTo;
            }
            set
            {
                if (value >= 1 && value <= 5)
                    mTerrainTo = value;
            }
        }

        public int SizeFrom
        {
            get
            {
                return mSizeFrom;
            }
            set
            {
                if (value >= 0 && value <= 5)
                    mSizeFrom = value;
            }
        }

        public int SizeTo
        {
            get
            {
                return mSizeTo;
            }
            set
            {
                if (value >= 0 && value <= 5)
                    mSizeTo = value;
            }
        }
        #endregion

        public WayPointFilterGeocache()
        {
            DifficultyFrom = 1;
            DifficultyTo = 5;
            TerrainFrom = 1;
            TerrainTo = 5;
            mSizeFrom = 0;
            mSizeTo = 5;

            CacheTypeTraditional = true;
            CacheTypeMulti = true;
            CacheTypeUnknown = false;
            CacheTypeEarth = true;
            CacheTypeEvent = true;
            CacheTypeWhereigo = true;
            CacheTypeLetterbox = true;
            CacheTypeMegaEvent = true;
            CacheTypeTrashOut = true;
            CacheTypeVirtual = true;
            CacheTypeWebcam = true;

            HideMyFinds = false;
            HideMyCaches = false;
            HideInactiveCaches = false;
            ShowAdditionalWaypoints = false;

            ContainsTrackables = false;

            MinFavoritePoints = 0;

            Name = string.Empty;
            Owner = string.Empty;

            CacheSizeMicro = true;
            CacheSizeSmall = true;
            CacheSizeRegular = true;
            CacheSizeBig = true;
        }

        public bool IsFilterOut(Waypoint waypoint)
        {
            if (waypoint.GetType() == typeof(GeocacheWaypoint))
            {
                GeocacheWaypoint gc = (GeocacheWaypoint)waypoint;

                if (gc.Difficulty < mDifficultyFrom || gc.Difficulty > mDifficultyTo)
                    return true;
                if (gc.Terrain < mTerrainFrom || gc.Terrain > mTerrainTo)
                    return true;

                if (HideMyFinds && gc.Found)
                    return true;

                if (HideInactiveCaches && !gc.Available)
                    return true;

                if (!CacheTypeTraditional && gc.CacheType == GeocacheType.Traditional)
                    return true;

                if (!CacheTypeMulti && gc.CacheType == GeocacheType.Multi)
                    return true;

                if (!CacheTypeUnknown && gc.CacheType == GeocacheType.Mystery)
                    return true;

                if (!CacheTypeEarth && gc.CacheType == GeocacheType.Earthcache)
                    return true;

                if (!CacheTypeEvent && gc.CacheType == GeocacheType.Event)
                    return true;

                if (!CacheTypeWhereigo && gc.CacheType == GeocacheType.Whereigo)
                    return true;

                if (!CacheTypeVirtual && gc.CacheType == GeocacheType.Virtual)
                    return true;

                if (!CacheTypeWebcam && gc.CacheType == GeocacheType.Webcam)
                    return true;

                if (!CacheTypeMegaEvent && gc.CacheType == GeocacheType.MegaEvent)
                    return true;

                if (!CacheTypeLetterbox && gc.CacheType == GeocacheType.Letterbox)
                    return true;

                if (!CacheTypeTrashOut && gc.CacheType == GeocacheType.TrashOut)
                    return true;

                if (Name.Length > 0 && !gc.Description.Contains(Name))
                    return true;

                if (Owner.Length > 0 && !gc.Owner.Contains(Owner))
                    return true;

                if (gc.Container == GeocacheSize.Micro && !CacheSizeMicro)
                    return true;

                if (gc.Container == GeocacheSize.Small && !CacheSizeSmall)
                    return true;

                if (gc.Container == GeocacheSize.Regular && !CacheSizeRegular)
                    return true;

                if (gc.Container == GeocacheSize.Big && !CacheSizeBig)
                    return true;

            }

            return false;
        }

        public override string ToString()
        {
            string dt = string.Format("D:{0}-{1}  T:{2}-{3}", DifficultyFrom, DifficultyTo, TerrainFrom, TerrainTo);

            string types_s = string.Empty;
            List<string> types = new List<string>();

            if (CacheTypeTraditional)
                types.Add("Traditional");

            if (CacheTypeMulti) types.Add("Multi");
            if (CacheTypeUnknown) types.Add("Unknown");
            if (CacheTypeEarth) types.Add("Earth");
            if (CacheTypeEvent) types.Add("Event");
            if (CacheTypeLetterbox) types.Add("Letterbox");
            if (CacheTypeMegaEvent) types.Add("MegaEvent");
            if (CacheTypeWhereigo) types.Add("WhereIGo");
            if (CacheTypeVirtual) types.Add("Virtual");
            if (CacheTypeWebcam) types.Add("Webcam");
            if (CacheTypeTrashOut) types.Add("TrashOut");

            int max_types = 7;
            if (types.Count < max_types)
                max_types = types.Count;

            for (int i = 0; i < max_types; i++)
            {
                types_s = string.Format("{0}{1}, ", types_s, types[i]);
            }
            if (types_s.Length > 2)
                types_s = types_s.Substring(0, types_s.Length - 2);

            string hide = string.Empty;
            if (HideMyFinds || HideMyCaches || HideInactiveCaches)
                hide = "Hide: ";
            if (HideMyFinds)
                hide = hide + "My Finds, ";
            if (HideMyCaches)
                hide = hide + "My Caches, ";
            if (HideInactiveCaches)
                hide = hide + "Inactive Caches, ";
            if (hide.Length > 2)
                hide = hide.Substring(0, hide.Length - 2);

            return string.Format("{0} \r\n{1} \r\n{2}", dt, types_s, hide);
        }
    }
}
