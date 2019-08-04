using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class CreateFromFileOperationModel : CreateOperationModel
    {

        public String FileName
        {
            get;
            set;
        }

        public CreateFromFileOperationModel()
        {

        }

        public CreateFromFileOperationModel(String fileName)
        {
            FileName = fileName;
        }
    }
}
