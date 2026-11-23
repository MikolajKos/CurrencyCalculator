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

        public Currency? FindByCode(string code)
        {
            var it = CreateIterator();
            while (it.HasNext())
            {
                var currency = it.Next();
                if (currency.Code == code.Trim().ToUpperInvariant())
                    return currency;
            }
            return null;
        }
    }
}
