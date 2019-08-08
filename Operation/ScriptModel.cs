using Maker.Business.Model.OperationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Model
{
    [Serializable]
    public class ScriptModel
    {
        public ScriptModel() {
            Contain = new List<string>();
            Intersection = new List<string>();
            Complement = new List<string>();
            OperationModels = new List<BaseOperationModel>();
        }
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
        //public String Parent
        //{
        //    get;
        //    set;
        //}
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
        public List<BaseOperationModel> OperationModels
        {
            get;
            set;
        }
        
    }
}
