using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCO.Model.Geocaching
{
    public class UserAccess 
    {
        public string AccessToken { get; set; }
        public string ConnectionString { get; set; }
        public string UniqueID { get; set; }
        public string UserName { get; set; }
        public bool IsBasic { get; set; }

        public UserAccess()
        {
            AccessToken = string.Empty;
            ConnectionString = string.Empty;
            UniqueID = string.Format("GCO_{0:0000}{1:00}{2:00}{3:00}{4:00}{5:00}{6}", DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            UserName = string.Empty;
            IsBasic = true;
        }
    }
}
