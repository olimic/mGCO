using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace GCO.Model.Geocaching
{
    public class User : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string UserName { get; set; }
        public long FoundCaches { get; set; }
        public string LastFoundCache { get; set; }
        public string ID { get; set; }
        public string AvatarUrl { get; set; }
        public string Guid { get; set; }

        public User()
        {
            UserName = "";
            FoundCaches = 0;
            LastFoundCache = "";
            ID = "";
            Guid = "";
            AvatarUrl = "";
        }

        public string ShortDescription
        {
            get { return string.Format("{0} ({1})", UserName, FoundCaches); }
            set { }
        }

        /// <summary>
        /// Raise the PropertyChanged event and pass along the property that changed
        /// </summary>
        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
