using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.Services
{
    public class CodeEngine: ICodeEngine
    {
        public CodeEngine()
        {

        }
        public async Task<object> RunCodeGetDataAsync(string Code, ScriptOptions options)
        {
            var scriptState = await CSharpScript.RunAsync(Code, options);
            return scriptState.ReturnValue;
        }
    }
}
