using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using CurrencyCalculator.Models;

namespace CurrencyCalculator.Providers;

public sealed class NbpXmlParser
{
    private static readonly CultureInfo PolishCulture = CultureInfo.GetCultureInfo("pl-PL");

    static NbpXmlParser()
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public ExchangeRateTable Parse(Stream xmlStream)
    {
        ArgumentNullException.ThrowIfNull(xmlStream);

        XDocument document;
        try
        {
            document = XDocument.Load(xmlStream);
        }
        catch (XmlException ex)
        {
            throw new FormatException("Plik z kursami nie jest poprawnym dokumentem XML.", ex);
        }

        XElement root = document.Root
            ?? throw new FormatException("Brak elementu głównego <tabela_kursow>.");

        string tableNumber = ReadValue(root, "numer_tabeli");
        DateOnly publicationDate = DateOnly.ParseExact(
            ReadValue(root, "data_publikacji"), "yyyy-MM-dd", CultureInfo.InvariantCulture);

        var currencies = new List<Currency> { Currency.Pln };
        foreach (XElement position in root.Elements("pozycja"))
            currencies.Add(ParseCurrency(position));

        return new ExchangeRateTable(tableNumber, publicationDate, currencies);
    }

    private static Currency ParseCurrency(XElement position)
    {
        string code = ReadValue(position, "kod_waluty");
        string name = ReadValue(position, "nazwa_waluty");

        if (!int.TryParse(ReadValue(position, "przelicznik"), NumberStyles.Integer, PolishCulture, out int multiplier))
            throw new FormatException($"Niepoprawny przelicznik waluty {code}.");

        if (!decimal.TryParse(ReadValue(position, "kurs_sredni"), NumberStyles.Number, PolishCulture, out decimal rate))
            throw new FormatException($"Niepoprawny kurs średni waluty {code}.");

        return new Currency(code, name, multiplier, rate);
    }

    private static string ReadValue(XElement parent, string elementName)
    {
        string? value = parent.Element(elementName)?.Value;
        if (string.IsNullOrWhiteSpace(value))
            throw new FormatException($"Brak wartości elementu <{elementName}>.");
        return value.Trim();
    }
}
