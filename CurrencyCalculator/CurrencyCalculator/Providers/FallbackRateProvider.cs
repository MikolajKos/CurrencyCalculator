using CurrencyCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculator.Providers
{
    public class FallbackRateProvider : IExchangeRateProvider
    {
        private readonly IReadOnlyList<IExchangeRateProvider> _providers;

        public FallbackRateProvider(IEnumerable<IExchangeRateProvider> providers)
        {
            ArgumentNullException.ThrowIfNull(providers);
            _providers = providers.ToList();
            if (_providers.Count == 0)
                throw new ArgumentException("Lista dostawców nie może być pusta.", nameof(providers));
        }

        public ExchangeRateTable LoadRates()
        {
            var errors = new List<Exception>();
            foreach (var provider in _providers)
            {
                try
                {
                    return provider.LoadRates();
                }
                catch (Exception ex)
                {
                    errors.Add(ex);
                }
            }

            throw new AggregateException("Nie udało się wczytać kursów z żadnego źródła.", errors);
        }
    }
}
