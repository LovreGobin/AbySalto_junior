namespace Domain.Constants
{
    /// <summary>
    /// Contains default values and static conversion rates used across the application.
    /// Note: Conversion rates are hardcoded for simplicity. In a production environment,
    /// these should be fetched from a live exchange rate API
    /// </summary>
    public static class OrderConstants
    {
        public const string DefaultCurrency = "EUR";

        public static readonly Dictionary<string, decimal> ConversionRates = new()
        {
            { "EUR", 1.0m },
            { "USD", 0.92m },
            { "HRK", 0.13m },
            { "GBP", 1.17m }
            // Add more currencies as needed
        };
    }
}