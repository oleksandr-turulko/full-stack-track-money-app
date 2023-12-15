namespace TrackMoney.Services.CurrencyConverter
{
    internal class ExchangeRatesApiResponse
    {
        public string Base { get; set; }
        public DateTime Date { get; set; }
        public Rates Rates { get; set; }
    }
}
