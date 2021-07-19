using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyTransactionsDemo.Core.Utilities
{
    public interface ICoreModule
    {
        void Load(IServiceCollection services);
    }
}
