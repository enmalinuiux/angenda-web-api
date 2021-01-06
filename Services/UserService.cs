using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using agenda_web_api.Models;
using agenda_web_api.Models.HttpMethods;
using System.Threading.Tasks;
using agenda_web_api.Helpers;
using agenda_web_api.Tools;

namespace agenda_web_api.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        IEnumerable<User> GetAll();
        User GetById(string id);
    }

    public class UserService : IUserService
    {
        
        private readonly AppSettings _appSettings;

        agendaContext _context;
        public UserService(agendaContext context, IOptions<AppSettings> appSettings){
            _context = context;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var encryptedPass = Encryptor.GetSHA256(model.Pass);
            var user = _context.User.SingleOrDefault(x => x.Email == model.Email && x.Pass == encryptedPass);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(token);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList();
        }

        public User GetById(string id)
        {
            return _context.User.FirstOrDefault(x => x.Id == id);
        }

        // helper methods
        private string generateJwtToken(User user){
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}