using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCO.Model.Waypoints
{
    public class Link
    {
        protected string mHref = string.Empty;
        protected string mText = string.Empty;
        protected string mType = string.Empty;

        public Link() { }

        public Link(Link other)
        {
            if (other == null) return;
            mHref = other.mHref;
            mText = other.mText;
            mType = other.mType;
        }

        public string Href
        {
            get { return mHref; }
            set { mHref = value; }
        }

        public string Text
        {
            get { return mText; }
            set { mText = value; }
        }

        public string Type
        {
            get { return mType; }
            set { mType = value; }
        }
    }
}
