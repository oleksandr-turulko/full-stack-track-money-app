namespace TrackMoney.Data.Models.Entities
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public decimal PriceInUAH { get; set; }
    }
}
