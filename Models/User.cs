using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        //Had to add APIKey even though it wasnt specificed, since it was needed for the Authentication
        public required string ApiKey { get; set; }

    }
}
