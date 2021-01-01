using System.ComponentModel.DataAnnotations;

namespace agenda_web_api.Models.HttpMethods
{
    public class AuthenticateRequest
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Pass { get; set; }
    }
}