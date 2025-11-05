using SQLite;

namespace NetflixRandomizer.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [NotNull]
        public string Username { get; set; }
        [NotNull]
        public string Pass { get; set; }

        public string Email { get; set; }
    }
}
