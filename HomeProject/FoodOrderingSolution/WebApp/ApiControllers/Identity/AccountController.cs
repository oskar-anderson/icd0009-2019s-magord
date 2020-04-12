﻿using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Identity;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApp.ApiControllers.Identity
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(IConfiguration configuration, UserManager<AppUser> userManager,
            ILogger<AccountController> logger, SignInManager<AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogInformation($"Web-Api email change. User {model.Email} not found!");
                return StatusCode(403);
            }

            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Token generated for user {model.Email}");
                return Ok(new {token = jwt, status = "Password changed!"});
            }
            
            _logger.LogInformation("Your password is remains unchanged!");
            return StatusCode(403);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogInformation($"Web-Api email change. User {model.Email} not found!");
                return StatusCode(403);
            }

            var token = await _userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail);
            var result = await _userManager.ChangeEmailAsync(user, model.NewEmail, token);

            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Token generated for user {model.NewEmail}");
                return Ok(new {token = jwt, status = "Email changed!"});
            }
            
            _logger.LogInformation("Your email is remains unchanged!");
            return StatusCode(403);
        }
        
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return StatusCode(403);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser); //get the User analog
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"Token generated for user {model.Email}");
                return Ok(new {token = jwt, status = "Logged in"});
            }

            _logger.LogInformation($"Web-Api login. User {model.Email} attempted to log-in with bad password!");
            return StatusCode(403);
        }


        [HttpPost]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            
            var appUser = new AppUser {UserName = model.Email, Email = model.Email};
            var result = await _userManager.CreateAsync(appUser, model.Password);
            //await _userManager.AddToRoleAsync(appUser, "User");

            if (!result.Succeeded)
            {
                _logger.LogInformation($"Web-Api register. User {model.Email} registration failed!");
                return StatusCode(403);
            }

            _logger.LogInformation("New user created.");
            
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
            
            var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                _configuration["JWT:SigningKey"],
                _configuration["JWT:Issuer"],
                _configuration.GetValue<int>("JWT:ExpirationInDays")
            );

            _logger.LogInformation("Token generated for user: " + appUser.UserName);
            return Ok(new {token = jwt, status = "Successfully registered"});
        }

        public class LoginDTO
        {
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class RegisterDTO
        {
            public string Email { get; set; } = default!;
            public string Password { get; set; } = default!;
        }

        public class ChangeEmailDTO
        {
            public string Email { get; set; } = default!;
            public string NewEmail { get; set; } = default!;
        }

        public class ChangePasswordDTO
        {
            public string Email { get; set; } = default!;
            public string OldPassword { get; set; } = default!;
            public string NewPassword { get; set; } = default!;
        }
    }
}
