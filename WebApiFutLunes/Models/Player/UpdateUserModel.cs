using System.ComponentModel.DataAnnotations;

namespace WebApiFutLunes.Models.Player
{
    public class UpdateUserModel
    {
        [Required]
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}