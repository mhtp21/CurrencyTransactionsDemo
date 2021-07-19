using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json;
using CurrencyTransactionsDemo.DataSource;

namespace CurrencyTransactionsDemo.ConversionTransactions
{
    public class CurrencyConversionTransactions
    {
        public decimal Get(string from, string to, decimal value)
        {
            if(from.ToLower() != "usd")
            {
                decimal BaseCurrency, UsdCurrency, TargetCurrency;
                CurrencyDataSource.Rates.TryGetValue(from.ToUpper(), out UsdCurrency);
                BaseCurrency = value / UsdCurrency;

                CurrencyDataSource.Rates.TryGetValue(to.ToUpper(), out TargetCurrency);
                return BaseCurrency * TargetCurrency;
            }

            decimal TargetCurrencySecond;
            CurrencyDataSource.Rates.TryGetValue(to.ToUpper(), out TargetCurrencySecond);
            return TargetCurrencySecond * value;
        }
    }
}
