using Customer.Domain.Dtos;
using Customer.Domain.Extension;
using Customer.Domain.Models;
using Customer.Domain.Other;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;

namespace Customer.Domain.Queries
{
    public class GetListCategoryQuery : QueryBase<List<CategoryDto>>
    {
        [JsonConstructor]
        public GetListCategoryQuery()
        {

        }
        [JsonConstructor]
        public GetListCategoryQuery(JObject PredicanteCollection)
        {
            this.PredicanteCollection = PredicanteCollection;
        }
        [JsonIgnore]
        public List<string> Props { get; set; }
        [JsonExtensionData]
        [JsonProperty("PredicanteCollection")]
        JObject PredicanteCollection;

        [OnSerializing]
        void OnSerializing(StreamingContext ctx)
        {
            VariablePropertyListExtensions.OnSerializing(Props, ref PredicanteCollection, false);
        }

        [OnSerialized]
        void OnSerialized(StreamingContext ctx)
        {
            VariablePropertyListExtensions.OnSerialized(Props, ref PredicanteCollection, false);
        }

        [OnDeserializing]
        void OnDeserializing(StreamingContext ctx)
        {
            VariablePropertyListExtensions.OnDeserializing(Props, ref PredicanteCollection, false);
        }

        [OnDeserialized]
        void OnDeserialized(StreamingContext ctx)
        {
            if (Props == null)
                Props = new List<string>();
            VariablePropertyListExtensions.OnDeserialized(Props, ref PredicanteCollection, false);
        }
    }
}
