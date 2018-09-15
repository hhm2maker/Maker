using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Maker.View.LightScriptUserControl
{
    /// <summary>
    /// CodeUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class CodeUserControl : UserControl
    {
        public CodeUserControl(NewMainWindow mw)
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CSharpCodeProvider objCSharpCodePrivoder = new CSharpCodeProvider();
            ICodeCompiler objICodeCompiler = objCSharpCodePrivoder.CreateCompiler();

            CompilerParameters objCompilerParameters = new CompilerParameters();

            //添加需要引用的dll
            objCompilerParameters.ReferencedAssemblies.Add("System.dll");
            objCompilerParameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");

            //是否生成可执行文件
            objCompilerParameters.GenerateExecutable = false;

            //是否生成在内存中
            objCompilerParameters.GenerateInMemory = true;

            //编译代码
            CompilerResults cr = objICodeCompiler.CompileAssemblyFromSource(objCompilerParameters, GetCode());

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
                objMI.Invoke(objHelloWorld, null);
            }
        }
        public String GetCode() {
            StringBuilder sb = new StringBuilder();
            sb.Append("using System.Windows.Forms;");
            sb.Append("public class Test{");
            sb.Append("public void Hello(){");
            sb.Append("MessageBox.Show(\"HelloWorld!\");}}");
            Console.WriteLine(sb.ToString());


            return sb.ToString();
        }

}
}
