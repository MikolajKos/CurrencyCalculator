using CurrencyCalculator.Models;

namespace CurrencyCalculator.Providers
{
    public class LocalFileRateProvider : IExchangeRateProvider
    {
        private readonly string _filePath;
        private readonly NbpXmlParser _parser;

        public LocalFileRateProvider(NbpXmlParser parser, string filePath)
        {
            ArgumentNullException.ThrowIfNull(parser);
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentException("Ścieżka do pliku nie może być pusta.", nameof(filePath));

            _parser = parser;
            _filePath = filePath;
        }

        public ExchangeRateTable LoadRates()
        {
            using var stream = File.OpenRead(_filePath);
            return _parser.Parse(stream);
        }
    }
}
