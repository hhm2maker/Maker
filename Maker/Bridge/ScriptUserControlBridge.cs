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

namespace Maker.Bridge
{
    public class ScriptUserControlBridge
    {
        private ScriptUserControl view;
        public ScriptUserControlBridge(ScriptUserControl view) {
            this.view = view;
        }
        /// <summary>
        /// 根据模型集合返回完整代码
        /// </summary>
        /// <param name="scriptModelDictionary"></param>
        /// <returns></returns>
        public String GetCode() {
           return Code.GetCode(view.scriptModelDictionary);
        }
        /// <summary>
        /// 根据模型集合返回指定步骤名完整代码
        /// </summary>
        /// <param name="scriptModelDictionary"></param>
        /// <returns></returns>
        public String GetCode(String stepName)
        {
            return Code.GetCode(view.scriptModelDictionary, stepName);
        }
        public void Test()
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
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, GetCode());
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
                view.mLightList.Clear();
                for (int i = 0; i < lights.Count; i++)
                {
                    mLights.Add(new Light(lights[i].Time, lights[i].Action, lights[i].Position, lights[i].Color));
                }
                view.mLightList = mLights;
                view.UpdateData(mLights);
                view.SaveFile();
            }
        }
        public void Test(String stepName)
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
            CompilerResults cr = objCSharpCodePrivoder.CompileAssemblyFromSource(objCompilerParameters, GetCode(stepName));
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
                view.mBlockLightList.Clear();
                for (int i = 0; i < lights.Count; i++)
                {
                    mLights.Add(new Light(lights[i].Time, lights[i].Action, lights[i].Position, lights[i].Color));
                }
                view.mBlockLightList = mLights;
            }
        }
    }
}
