using System.Threading.Tasks;
using Domain.App.Identity;
using Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PublicApi.DTO.v1.Identity;

namespace WebApp.ApiControllers._1._0.Identity
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<Domain.App.Identity.AppUser> _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<Domain.App.Identity.AppUser> _signInManager;
        
        public AccountController(IConfiguration configuration, UserManager<Domain.App.Identity.AppUser> userManager,
            ILogger<AccountController> logger, SignInManager<Domain.App.Identity.AppUser> signInManager)
        {
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _signInManager = signInManager;
        }
        
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
            {
                _logger.LogInformation($"Web-Api login. User {model.Email} not found!");
                return NotFound("User not found!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);
            if (result.Succeeded)
            {
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                    _configuration["JWT:SigningKey"],
                    _configuration["JWT:Issuer"],
                    _configuration.GetValue<int>("JWT:ExpirationInDays")
                );
                _logger.LogInformation($"WebApi login. User {model.Email} logged in");
                return Ok(new {token = jwt, status = "Logged in", firstName = appUser.FirstName, lastName = appUser.LastName});
            }

            _logger.LogInformation($"Web-Api login. User {model.Email} attempted to log-in with bad password!");
            return NotFound("User not found!");
        }
        
        
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            
            var appUser = await _userManager.FindByEmailAsync(model.Email);
            if (appUser != null)
            {
                _logger.LogInformation($"Web-Api register. User {model.Email} already registered!");
                return StatusCode(404, new {message = "User already registered!"});
            }
            
            appUser = new Domain.App.Identity.AppUser() 
            {
                UserName = model.Email, 
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };
            
            var result = await _userManager.CreateAsync(appUser, model.Password);
            
            // Adds the registered user to the User role
            await _userManager.AddToRoleAsync(appUser, "User");

            if (!result.Succeeded)
            {
                _logger.LogInformation($"Web-Api register. User {model.Email} registration failed!");
                return StatusCode(400, new {message = "Registration failed!"});
            }

            _logger.LogInformation("New user created.");
            
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
            
            var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                _configuration["JWT:SigningKey"],
                _configuration["JWT:Issuer"],
                _configuration.GetValue<int>("JWT:ExpirationInDays")
            );

            _logger.LogInformation($"Web-Api register. User {appUser.Email} registration successful!");
            return Ok(new {token = jwt, status = $"User {appUser.Email} created and logged in!", firstName = appUser.FirstName, lastName = appUser.LastName});
        }
        
        
        /// <summary>
        /// Endpoint for user change-password (jwt generation)
        /// </summary>
        /// <param name="model">user data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogInformation($"Web-Api password change. User {model.Email} not found!");
                return StatusCode(404, new {message = "Changing user password failed! User not found!"});
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
            return StatusCode(400, new {message = "User password not changed!"});
        }

        /// <summary>
        /// Endpoint for user change-email (jwt generation)
        /// </summary>
        /// <param name="model">user data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogInformation($"Web-Api email change. User {model.Email} not found!");
                return StatusCode(404, new {message = "Changing user email failed! User not found!"});
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
                _logger.LogInformation($"New token generated for user {model.NewEmail}");
                return Ok(new {token = jwt, status = "Email changed!"});
            }
            
            _logger.LogInformation("Your email remains unchanged!");
            return StatusCode(400, new {message = "User email not changed!"});
        }
        
        /// <summary>
        /// Endpoint for user change-names (jwt generation)
        /// </summary>
        /// <param name="model">user data</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeNames([FromBody] ChangeNamesDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                _logger.LogInformation($"Web-Api email change. User {model.Email} not found!");
                return StatusCode(404, new {message = "Changing user email failed! User not found!"});
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            await _userManager.UpdateAsync(user);
            
            
            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user); //get the User analog
            var jwt = IdentityExtensions.GenerateJWT(claimsPrincipal.Claims,
                _configuration["JWT:SigningKey"],
                _configuration["JWT:Issuer"],
                _configuration.GetValue<int>("JWT:ExpirationInDays"));
            _logger.LogInformation($"New token generated for user {model.Email}");
            return Ok(new {token = jwt, status = "Names changed!", firstName = model.FirstName, lastName = model.LastName});
        }
    }
}