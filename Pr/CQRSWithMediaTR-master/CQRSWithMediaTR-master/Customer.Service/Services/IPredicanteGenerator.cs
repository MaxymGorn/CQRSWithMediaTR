using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Service.Services
{
    public interface IPredicanteGenerator
    {
        string GenerateCode(Type TypePredicante, List<string> ElementPredicante);
    }
}
