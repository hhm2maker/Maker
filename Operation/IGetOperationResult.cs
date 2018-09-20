using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Operation
{
    public interface IGetOperationResult
    {
        List<Light> GetOperationResult(List<Light> lightGroup, String parameter);
    }
}
