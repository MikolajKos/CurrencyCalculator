using CurrencyCalculator.Models;

namespace CurrencyCalculator.Collections
{
    public class CurrencyCollection : ICurrencyAggregate
    {
        private readonly List<Currency> _currencies;

        public CurrencyCollection(IEnumerable<Currency> currencies)
        {
            ArgumentNullException.ThrowIfNull(currencies);
            _currencies = currencies.ToList();
        }

        public ICurrencyIterator CreateIterator() => new CurrencyIterator(_currencies);

        public int Count => _currencies.Count;
    }
}
