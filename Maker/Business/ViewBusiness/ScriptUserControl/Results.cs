using Maker.Business.ScriptUserControlBusiness;
using Maker.Model;
using Maker.View.LightScriptUserControl;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maker.Business.ViewBusiness.ScriptUserControl
{
    public static class Results
    {
        public static List<Light> Test(Dictionary<String, ScriptModel> scriptModelDictionary)
        {
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
            CompilerParameters objCompilerParameters = new CompilerParameters();

            //添加需要引用的dll
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Operation.dll");
            //是否生成可执行文件
            objCompilerParameters.GenerateExecutable = false;

            //是否生成在内存中
            objCompilerParameters.GenerateInMemory = true;
            //编译代码
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, Code.GetCode(scriptModelDictionary));
            if (cr.Errors.HasErrors)
            {
                var msg = string.Join(Environment.NewLine, cr.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
                MessageBox.Show(msg, "编译错误");
            }
            else
            {
                Assembly objAssembly = cr.CompiledAssembly;
                object objHelloWorld = objAssembly.CreateInstance("Test");
                MethodInfo objMI = objHelloWorld.GetType().GetMethod("Hello");
                List<Operation.Light> lights = (List<Operation.Light>)objMI.Invoke(objHelloWorld, new Object[] { });
                List<Light> mLights = new List<Light>();
               
                for (int i = 0; i < lights.Count; i++)
                {
                    mLights.Add(new Light(lights[i].Time, lights[i].Action, lights[i].Position, lights[i].Color));
                }
                return mLights;
            }
            return null;
        }
        public static List<Light> Test(Dictionary<String, ScriptModel> scriptModelDictionary,String stepName)
        {
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
            CompilerParameters objCompilerParameters = new CompilerParameters();

            //添加需要引用的dll
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            objCompilerParameters.ReferencedAssemblies.Add("Operation.dll");
            //是否生成可执行文件
            objCompilerParameters.GenerateExecutable = false;

            //是否生成在内存中
            objCompilerParameters.GenerateInMemory = true;
            //编译代码
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, Code.GetCode(scriptModelDictionary,stepName));
            if (cr.Errors.HasErrors)
            {
                var msg = string.Join(Environment.NewLine, cr.Errors.Cast<CompilerError>().Select(err => err.ErrorText));
                MessageBox.Show(msg, "编译错误");
            }
            else
            {
                Assembly objAssembly = cr.CompiledAssembly;
                object objHelloWorld = objAssembly.CreateInstance("Test");
                MethodInfo objMI = objHelloWorld.GetType().GetMethod("Hello");
                List<Operation.Light> lights = (List<Operation.Light>)objMI.Invoke(objHelloWorld, new Object[] { });
                List<Light> mLights = new List<Light>();
                for (int i = 0; i < lights.Count; i++)
                {
                    mLights.Add(new Light(lights[i].Time, lights[i].Action, lights[i].Position, lights[i].Color));
                }
                return mLights;
            }
            return null;
        }
    }
}
