using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
    public class ScriptModel
    {
        public String Name
        {
            get;
            set;
        }
        public String Value
        {
            get;
            set;
        }
        public String Parent
        {
            get;
            set;
        }
        public bool Visible
        {
            get;
            set;
        }
        public List<String> Contain
        {
            get;
            set;
        }
        public List<String> Intersection
        {
            get;
            set;
        }
        public List<String> Complement
        {
            get;
            set;
        }
    }
}
