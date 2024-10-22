using AutoMapper;
using Domain.Entities.SecurityEntities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shared.BasketModels;
using Shared.OrderModels;
using Shared.SecurityModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    internal class AuthenticationService(UserManager<User> _userManager,IOptions<JwtOptions> options,IMapper mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailExist(string email)
        {
            var user= await _userManager.FindByEmailAsync(email);

            return user != null;
        }

        public async Task<AddressDTO> GetUserAddress(string email)
        {
            var user= await _userManager.Users.Include(u=>u.Address).FirstOrDefaultAsync(u=>u.Email==email)
                ??throw new UserNotFoundException(email);
          
            return mapper.Map<AddressDTO>(user.Address);
            //return new AddressDTO
            //{
            //    City = user.Address.City,
            //    Country = user.Address.Country,
            //    FirstName = user.Address.FirstName,
            //    LastName = user.Address.LastName,
            //};

        }

        public async Task<UserResultDTO> GetUserByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email) ?? throw new UserNotFoundException(email);

            var userDto = new UserResultDTO(user.DisplayName, user.Email!, await CreateTokenAsync(user));

            return userDto;
        }
   
        public async Task<AddressDTO> UpdateUserAddress(AddressDTO address ,string email)
        {
            var user = await _userManager.Users.Include(u => u.Address)
                .FirstOrDefaultAsync(u => u.Email == email)
                ?? throw new UserNotFoundException(email);


            if (user.Address != null)
            {

                user.Address.FirstName = address.FirstName;
                user.Address.LastName = address.LastName;
                user.Address.Street = address.Street;
                user.Address.City = address.City;
                user.Address.Country = address.Country;
            }

            else
            {
                var userAddress = mapper.Map<Address>(address);
                user.Address = userAddress;
            }

                await _userManager.UpdateAsync(user);

            return address;
        }
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
