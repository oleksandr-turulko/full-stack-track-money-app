using System.Net.Http.Json;

namespace TrackMoney.Services.CurrencyConverter
{
    public static class CurrencyConverter
    {
        private const string ApiBaseUrl = "https://open.er-api.com/v6/latest/";
        private static readonly HttpClient HttpClient = new HttpClient();

        public static async Task<decimal> ConvertCurrency(decimal value, string fromCurrency, string toCurrency = "UAH")
        {
            if (string.IsNullOrWhiteSpace(fromCurrency) || string.IsNullOrWhiteSpace(toCurrency))
            {
                throw new ArgumentException("Invalid currency codes");
            }

            // Fetch exchange rates from the API
            var exchangeRates = await GetExchangeRates(toCurrency);

            // Convert the value to UAH first


            decimal uahValue = exchangeRates.Rates == null ? 0 : value / exchangeRates.Rates.UAH;

            // Convert to the target currency
            return uahValue;
        }


        private static async Task<ExchangeRatesApiResponse> GetExchangeRates(string baseCurrency)
        {
            var response = await HttpClient.GetFromJsonAsync<ExchangeRatesApiResponse>($"{ApiBaseUrl}{baseCurrency}");
            return response ?? throw new InvalidOperationException("Failed to fetch exchange rates");
        }
    }
}
