using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class AuthenticationService(UserManager<User> _userManager,IOptions<JwtOptions> options) : IAuthenticationService
    {
        public async Task<UserResultDTO> LoginAsync(LoginDTO loginModel)
        {
            // Check if there's user for this email
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) 
                throw new UnAuthorizedException("Email Doesn't Exist");

            // Check if the Password is correct for this Email
            var result= await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if(!result)
                throw new UnAuthorizedException();

            // Create Token and return Response 
            var userDto=new UserResultDTO(user.DisplayName,user.Email!,await CreateTokenAsync(user));

            return userDto;

        }

        public async Task<UserResultDTO> RegisterAsync(UserRegisterDTO registerModel)
        {
            // Check if Email is Unique or No
            // Check if UserName is unique or No


            var user = new User()
            {
                Email = registerModel.Email,
                DisplayName = registerModel.DisplayName,
                PhoneNumber = registerModel.PhoneNumber,
                UserName = registerModel.UserName
            };
            var result=await _userManager.CreateAsync(user,registerModel.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                throw new ValidationException(errors);
            }

            var userDto = new UserResultDTO(user.DisplayName, user.Email!,await CreateTokenAsync(user));

            return userDto;
        }


      private async Task<string> CreateTokenAsync(User user)
        {
            var jwtOptions = options.Value;

            //Private Claims

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.Email, user.Email!)
            };
            
            // Add Role To Claims if exist
            var roles = await _userManager.GetRolesAsync(user);
            foreach(var role in roles) 
                authClaims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secretkey));

          // signing credentials => used in token generation     
            var signingCreds=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (audience: jwtOptions.Audience, //Url bta3 el Frontend  aw flutter ely ha3ml host ll Token 
                issuer: jwtOptions.Issuer,
                expires: DateTime.UtcNow.AddDays(jwtOptions.DuartionInDays),
                claims: authClaims,
                signingCredentials: signingCreds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
