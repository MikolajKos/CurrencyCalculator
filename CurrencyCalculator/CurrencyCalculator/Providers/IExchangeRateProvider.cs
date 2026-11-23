using CurrencyCalculator.Models;

namespace CurrencyCalculator.Providers
{
    public interface IExchangeRateProvider
    {
        ExchangeRateTable LoadRates();
    }
}
