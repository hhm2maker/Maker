using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maker.Model;

namespace Maker.Business.Currency
{
    public class OperationUtils
    {
        public static List<Light> OperationLightToMakerLight(List<Operation.Light> lights)
        {
            List<Light> myLights = new List<Light>();
            foreach (Operation.Light light in lights)
            {
                myLights.Add(new Light(light.Time, light.Action, light.Position, light.Color));
            }
            return myLights;
        }

        public static Dictionary<string, List<Light>> OperationLightDictionaryToMakerLightDictionary(Dictionary<string, List<Operation.Light>> lightDictionary)
        {
            Dictionary<string, List<Light>> myLightDictionary = new Dictionary<string, List<Light>>();
            foreach (var item in lightDictionary)
            {
                List<Light> myLights = new List<Light>();
                foreach (Operation.Light light in item.Value)
                {
                    myLights.Add(new Light(light.Time, light.Action, light.Position, light.Color));
                }
                myLightDictionary.Add(item.Key, myLights);
            }
            return myLightDictionary;
        }
    }
}
