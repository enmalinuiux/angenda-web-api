namespace agenda_web_api.Models.DTO
{
    public class AuthenticateResponseDTO
    {
        public AuthenticateResponseDTO(string token)
        {
            this.Token = token;
        }
        public string Token { get; set; }
    }
}