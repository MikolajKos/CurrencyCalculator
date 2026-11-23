using CurrencyCalculator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyCalculator.Providers
{
    public class NbpWebRateProvider : IExchangeRateProvider
    {
        private readonly NbpXmlParser _parser;
        private readonly HttpClient _httpClient;
        private const string Url = "https://static.nbp.pl/dane/kursy/xml/LastA.xml";

        public NbpWebRateProvider(HttpClient httpClient, NbpXmlParser parser)
        {
            ArgumentNullException.ThrowIfNull(httpClient);
            ArgumentNullException.ThrowIfNull(parser);

            _httpClient = httpClient;
            _parser = parser;
        }

        public ExchangeRateTable LoadRates()
        {
            using var response = _httpClient.Send(new HttpRequestMessage(HttpMethod.Get, Url));
            response.EnsureSuccessStatusCode();
            using var stream = response.Content.ReadAsStream();
            return _parser.Parse(stream);
        }
    }
}
