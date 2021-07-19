using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTransactionsDemo.ConversionTransactions;
using Hangfire;

namespace CurrencyTransactionsDemo.LoggingOperation
{
    public static class UpdatesHangfire
    {
        public static void UpdateCurrency()
        {
            RecurringJob.AddOrUpdate<UpdateRates>(nameof(UpdateRates),
                   job => job.Process(), "00 10 * * *", TimeZoneInfo.Local);
        }

        public static void UpdateCurrencySecond()
        {
            RecurringJob.AddOrUpdate<UpdateRates>(nameof(UpdateRates),
                   job => job.Process(), "00 15 * * *", TimeZoneInfo.Local);
        }
    }
}