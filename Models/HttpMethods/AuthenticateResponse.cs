namespace agenda_web_api.Models.HttpMethods
{
    public class AuthenticateResponse
    {

        public AuthenticateResponse(string token)
        {
            this.Token = token;
        }
        public string Token { get; set; }
    }
}