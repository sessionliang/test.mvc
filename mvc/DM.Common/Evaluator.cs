using System;
using System.CodeDom.Compiler;
using System.Reflection;

namespace DM.Common
{
    public class Evaluator
    {
        private static readonly string jscriptSource =
                @"class Evaluator
                { 
	                public function Eval(expr : String) 
	                {
		                return eval(expr);
	                }
                }";
        private static Type evaluatorType = null;
        private static object evaluator = null;

        static Evaluator()
        {
            CodeDomProvider provider = CodeDomProvider.CreateProvider("JScript");
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateInMemory = true;
            CompilerResults results = provider.CompileAssemblyFromSource(parameters, jscriptSource);
            evaluatorType = results.CompiledAssembly.GetType("Evaluator");
            evaluator = Activator.CreateInstance(evaluatorType);
        }

        public static object Eval(string expr)
        {
            return evaluatorType.InvokeMember("Eval", BindingFlags.InvokeMethod, null, evaluator, new object[] { expr });
        }
    }
}