using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using CurrencyTransactionsDemo.Entities;

namespace CurrencyTransactionsDemo.DataSource
{
    public static class CurrencyDataSource
    {
        private static HttpClient httpClient;
        public static Dictionary<string, decimal> Rates;
        static CurrencyDataSource()
        {
            httpClient = new HttpClient();
            _ = RateTransactions();
            Debug.WriteLine("asdfgh");
        }
        public static async Task RateTransactions()
        {
            HttpResponseMessage response = await httpClient.GetAsync("https://openexchangerates.org/api/latest.json?app_id=d8b98a988f17484d91bcf48b3a80384d");
            if (response.IsSuccessStatusCode)
            {
                string rates = await response.Content.ReadAsStringAsync();
                var exchangeRates = JsonConvert.DeserializeObject<SetupFeatures>(rates);

                Rates = exchangeRates.Proportions;

            }
        }
    }
}
