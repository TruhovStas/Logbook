using Microsoft.AspNetCore.Identity;

namespace Logbook.DataAccess.Entities
{
    public class User : IdentityUser<int>
    {
        public string Fio { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
