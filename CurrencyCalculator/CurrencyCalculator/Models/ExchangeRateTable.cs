namespace CurrencyCalculator.Models
{
    public sealed class ExchangeRateTable
    {
        public string TableNumber { get; }
        public DateOnly PublicationDate { get; }
        public IReadOnlyList<Currency> Currencies { get; }

        public ExchangeRateTable(string tableNumber, DateOnly publicationDate, IEnumerable<Currency> currencies)
        {
            if (string.IsNullOrWhiteSpace(tableNumber))
                throw new ArgumentException("Numer tabeli nie może być pusty.", nameof(tableNumber));
            ArgumentNullException.ThrowIfNull(currencies);

            TableNumber = tableNumber.Trim();
            PublicationDate = publicationDate;
            Currencies = currencies.ToList();
        }
    }
}
