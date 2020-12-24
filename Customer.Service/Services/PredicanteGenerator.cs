using System;
using System.Collections.Generic;
using System.Text;

namespace Customer.Service.Services
{
    public class PredicanteGenerator: IPredicanteGenerator
    {
        public PredicanteGenerator()
        {

        }

        public string GenerateCode(Type TypePredicante, List<string> ElementPredicante)
        {
            StringBuilder sb = new StringBuilder();
            //predicate.And(element=>element.CategoryId>0);
            sb.AppendLine($"var predicate = LinqKit.PredicateBuilder.New<{TypePredicante}>(true);");
            foreach (var elementProps in ElementPredicante)
            {
                sb.AppendLine($"predicate.{elementProps};");
            }
            sb.AppendLine($"return predicate;");
            return sb.ToString();
        }
    }
}
