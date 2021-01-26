using System.ComponentModel.DataAnnotations;

namespace agenda_web_api.Models.DTO
{
    public class AuthenticateRequestDTO
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Pass { get; set; }
    }
}