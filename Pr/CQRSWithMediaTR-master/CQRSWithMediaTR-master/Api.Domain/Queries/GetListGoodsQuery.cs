using Customer.Domain.Extension;
using Customer.Domain.Queries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shop.Domain.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Shop.Domain.Queries
{

    public class GetListGoodsQuery : QueryBase<List<GoodsDto>>
    {
        [JsonConstructor]
        public GetListGoodsQuery()
        {

        }
        [JsonConstructor]
        public GetListGoodsQuery(JObject PredicanteCollection)
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
