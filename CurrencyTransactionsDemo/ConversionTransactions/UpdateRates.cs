using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTransactionsDemo.DataSource;

namespace CurrencyTransactionsDemo.ConversionTransactions
{
    public class UpdateRates
    {
        public void Process()
        {
            _ = CurrencyDataSource.RateTransactions();
        }
    }
}
