using JBHiFi.ProductManagement.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;



namespace JBHiFi.ProductManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
            private IConfiguration _config;

            public TokenController(IConfiguration config)
            {
                _config = config;
            }

            [AllowAnonymous]
            [HttpPost]
            public IActionResult CreateToken([FromBody]LoginModel login)
            {
                IActionResult response = Unauthorized();
                var user = Authenticate(login);

                //create token if the user is authenticated
                if (user != null)
                {
                    var tokenStr = BuildToken(user);
                    //return a token to the user
                    response = Ok(new { token = tokenStr });
                }

                return response;
            }

            private string BuildToken(UserModel user)
            {

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                //create a new token
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                        _config["Jwt:Issuer"],
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: credentials
                    );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }

            /// <summary>
            /// Authenticate user
            /// only username: test and password: test123 will pass the authentication
            /// </summary>
            /// <param name="login"></param>
            /// <returns></returns>
            private UserModel Authenticate(LoginModel login)
            {
                UserModel user = null;

                if (login.Username == "test" && login.Password == "test123")
                {
                    user = new UserModel { Name = "testuser", Email = "jbhifi@test.com" };
                }

                return user;
            }
        }

}