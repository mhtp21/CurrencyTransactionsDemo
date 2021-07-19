using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyTransactionsDemo.ConversionTransactions;

namespace CurrencyTransactionsDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        private readonly CurrencyConversionTransactions _currencyConversionTransactions;
        public ProcessController(CurrencyConversionTransactions currencyConversionTransactions)
        {
            this._currencyConversionTransactions = currencyConversionTransactions;
        }

        [HttpGet]
        [Route("convert/{from}/{to}/{value:decimal}")]
        public IActionResult Convert(string from, string to, decimal value)
        {
            decimal convertedValue = _currencyConversionTransactions.Get(from, to, value);
            return Ok(convertedValue);
        }
    } 
}
