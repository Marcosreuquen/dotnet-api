using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security;
using System.Security.Claims;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using challenge.Models;
using challenge.Data;
namespace challenge.Controllers
{
    [ApiController]
    [Route("auth")]
    public class UserController : ControllerBase
    {
        private static List<User> users = new List<User>
        {
            new User {
                Id = 00,
                Email = "marcos@marcos.com",
                Token = "234h75f2374fry4hr5" //Ver JWT y DataAnnotations para crear tokens
            }
        };
        private SymmetricSecurityKey Secret()
        {
            string mySecret = Environment.GetEnvironmentVariable("SECRET");
            SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));
            return SecurityKey;
        }

        private string GenerateToken(int userId, string email)
        {
            SymmetricSecurityKey mySecurityKey = Secret();

            var target = new {
                Id = userId,
                Email = email,
            };

        //    Tranform it to Json object
            string jsonData = JsonConvert.SerializeObject(target);
            // Parse the json object
            // JObject jsonObject = JObject.Parse(jsonData);


            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, jsonData.ToString()),
                }),
                // Issuer = "self",
                // Audience = "https://www.mywebsite.com",
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static async Task SendWelcomeEmail([FromBody]string email)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("marcosreuquendiaz@gmail.com"),
                Subject = "Welcome to the movie/characters api.",
                HtmlContent = "<strong>Now you're allowed to send your requests</strong>"
            };
            msg.AddTo(new EmailAddress(email));
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        [HttpPost("/register")]
        public async Task<ActionResult<User>> Register([FromBody]string email)
        {
            //if the requested user already exists, send a bq
            if(users.Exists(u=> u.Email == email))
            {
                return BadRequest("User already exist.");
            }
            else
            {
                //create the user
                int userId = users.Count + 1;
                //Tokenize email and userId
                var Token = GenerateToken(userId,email);
                var newUser = new User {
                    Id = userId,
                    Email = email,
                    Token = Token,
                };
                //add the new user to list
                users.Add(newUser);
                //send welcome email
                await SendWelcomeEmail(email);
                //and return only the token
                return Ok(Token); 
            }
        }

        [HttpGet("/login")]
        public async Task<ActionResult<User>> Login(string token)
        {
            var user = users.Find(u => u.Token == token);
            if(user == null)
                return BadRequest("User not found.");
            
            return Ok(user);
        }
    };
}