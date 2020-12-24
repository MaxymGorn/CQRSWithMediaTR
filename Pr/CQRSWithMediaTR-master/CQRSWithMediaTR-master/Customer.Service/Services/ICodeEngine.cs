using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.Services
{
    public interface ICodeEngine
    {
        Task<object> RunCodeGetDataAsync(string code,ScriptOptions options);
    }
}
