namespace CurrencyCalculator.Models;

public sealed class Currency : IEquatable<Currency>
{
    public static readonly Currency Pln = new("PLN", "złoty polski", 1, 1m);

    public string Code { get; }
    public string Name { get; }
    public int Multiplier { get; }
    public decimal AverageRate { get; }

    public decimal RatePerUnit => AverageRate / Multiplier;

    public Currency(string code, string name, int multiplier, decimal averageRate)
    {
        if (string.IsNullOrWhiteSpace(code) || code.Trim().Length != 3)
            throw new ArgumentException("Kod waluty musi mieć 3 znaki.", nameof(code));
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nazwa waluty nie może być pusta.", nameof(name));
        if (multiplier <= 0)
            throw new ArgumentOutOfRangeException(nameof(multiplier), "Przelicznik musi być dodatni.");
        if (averageRate <= 0)
            throw new ArgumentOutOfRangeException(nameof(averageRate), "Kurs musi być dodatni.");

        Code = code.Trim().ToUpperInvariant();
        Name = name.Trim();
        Multiplier = multiplier;
        AverageRate = averageRate;
    }

    public bool Equals(Currency? other) => other is not null && Code == other.Code;
    public override bool Equals(object? obj) => Equals(obj as Currency);
    public override int GetHashCode() => Code.GetHashCode();

    public override string ToString() => $"{Code} – {Name}";
}
