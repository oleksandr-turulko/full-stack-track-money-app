using TrackMoney.Data.Models.Abstract;

namespace TrackMoney.Data.Models.Entities
{
    public class User : BaseEntity
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
