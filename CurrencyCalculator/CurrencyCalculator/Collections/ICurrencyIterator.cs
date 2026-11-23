using CurrencyCalculator.Models;

namespace CurrencyCalculator.Collections
{
    public interface ICurrencyIterator
    {
        bool HasNext();
        Currency Next();
        void Reset(); // back to start position
    }
}
