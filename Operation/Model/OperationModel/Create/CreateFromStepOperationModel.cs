using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maker.Business.Model.OperationModel
{
    public class CreateFromStepOperationModel : CreateOperationModel
    {

        public String StepName
        {
            get;
            set;
        }

        public CreateFromStepOperationModel()
        {

        }

        public CreateFromStepOperationModel(String stepName)
        {
            StepName = stepName;
        }
    }
}
