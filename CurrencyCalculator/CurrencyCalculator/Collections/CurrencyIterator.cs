using CurrencyCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculator.Collections
{
    public class CurrencyIterator : ICurrencyIterator
    {
        private readonly IReadOnlyList<Currency> _currencies;
        private int _position;

        public CurrencyIterator(IReadOnlyList<Currency> currencies)
        {
            ArgumentNullException.ThrowIfNull(currencies);
            _currencies = currencies;
            _position = 0;
        }

        public bool HasNext() => _position < _currencies.Count;

        public Currency Next()
        {
            if (!HasNext())
                throw new InvalidOperationException("Brak kolejnych elementów.");
            return _currencies[_position++];
        }

        public void Reset() => _position = 0;
    }
}
