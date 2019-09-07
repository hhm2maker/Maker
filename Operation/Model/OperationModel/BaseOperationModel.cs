using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Maker.Business.Model.OperationModel
{
    [Serializable]
    public class BaseOperationModel
    {
        public virtual XElement GetXElement() {
            throw new NotImplementedException();
        }

        public virtual void SetXElement(XElement xEdit)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            BinaryFormatter Formatter = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
            MemoryStream stream = new MemoryStream(); Formatter.Serialize(stream, this);
            stream.Position = 0;
            object clonedObj = Formatter.Deserialize(stream);
            stream.Close();
            return clonedObj;
        }
    }
}
