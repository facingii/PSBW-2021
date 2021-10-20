using System;
using System.Linq;
using System.Collections.Generic;
using EmployeesMicroService.Models;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EmployeesMicroService.Helpers
{
    public class UserService : IUserService
    {
        private List<User> _users = new List<User>()
        {
            new User ()
            {
                Id = "1",
                FirstName = "Daenarys",
                LastName = "Targaryen",
                UserName = "danytargaryen",
                Password = "fireandblood",
                Token = ""
            }
        };

        private readonly JwtSettings _jwtSettings;

        public UserService (IOptions<JwtSettings> options)
        {
            _jwtSettings = options.Value;
        }

        public User Authenticate (string username, string password)
        {
            var user = _users.SingleOrDefault (u => u.UserName == username && u.Password == password);
            if (user == null) return user;

            var tokenHandler = new JwtSecurityTokenHandler ();
            var key = System.Text.Encoding.ASCII.GetBytes (_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity (
                    new Claim [] {
                        new Claim (ClaimTypes.Name, user.UserName)
                    }
                ),
                Expires = DateTime.Now.AddHours (1),
                NotBefore = DateTime.Now,
                SigningCredentials = new SigningCredentials (new SymmetricSecurityKey (key), SecurityAlgorithms.HmacSha256Signature) 
            };

            var token = tokenHandler.CreateToken (tokenDescriptor);
            user.Token = tokenHandler.WriteToken (token);
            user.Password = null;

            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.Select (u => {
                u.Password = "";
                return u;
            });
        }
    }
}
