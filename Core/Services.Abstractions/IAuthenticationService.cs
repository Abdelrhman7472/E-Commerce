﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.OrderModels;
using Shared.SecurityModels;

namespace Services.Abstractions
{
    public interface IAuthenticationService
    {
        public Task<UserResultDTO> LoginAsync(LoginDTO loginModel);// kan momken 3ady a3ml return l string w tkon Token 3shan da ely harg3o asln bas za3dt kaza 7aga fel return ma3 el Token fe 3amlt UserResultDTO 3shan kda w hatkon heya el return 

        public Task<UserResultDTO> RegisterAsync(UserRegisterDTO registerModel);

        // Get Current User 

        public Task<UserResultDTO> GetUserByEmail(string email);

        //Check Email Exist
        public Task<bool> CheckEmailExist(string email);
        // Get User Address
        public Task<AddressDTO> GetUserAddress(string email);

        // Update User Address
        public Task<AddressDTO> UpdateUserAddress(AddressDTO address, string email);




    }
}
