using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CurrencyTransactionsDemo.Entities
{
    public class SetupFeatures
    {
        public string disclaimer { get; set; }
        public string license { get; set; }
        public int timestamp { get; set; }
        public string @base { get; set; }

        [JsonProperty("rates")]
        public Dictionary<string, decimal> Proportions { get; set; }
    }
}
