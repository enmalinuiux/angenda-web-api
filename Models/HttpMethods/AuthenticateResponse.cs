namespace agenda_web_api.Models.HttpMethods
{
    public class AuthenticateResponse
    {

        public AuthenticateResponse(string email, string token)
        {
            this.Email = email;
            this.Token = token;

        }
        public string Email { get; set; }
        public string Token { get; set; }
    }
}