using System.ComponentModel.DataAnnotations;

namespace CodersParadise.DataAccess.Databases.CodersParadise.Models
{
    public class RefreshToken
    {
        [Key]
        public Guid JwtId { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
