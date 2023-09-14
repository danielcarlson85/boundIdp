using AutoMapper;
using Bound.EventBus;
using Bound.IDP.Abstractions.Interfaces;
using Bound.IDP.Abstractions.Models;
using Bound.IDP.Abstractions.Models.AzureADB2C.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace Bound.IDP.Runtime.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IADTokenService _azureTokenService;
        private readonly IMapper _mapper;
        //private readonly IUserEventBusHandler _userEventBusHandler;

        public AuthController(ILogger<AuthController> logger, IADTokenService azureTokenService, IMapper mapper, IUserEventBusHandler userEventBusHandler)
        {
            _logger = logger;
            _azureTokenService = azureTokenService;
            _mapper = mapper;
         //   _userEventBusHandler = userEventBusHandler;
        }

        // To call through API Gateway use https://localhost:44375/login
        /// <summary>
        /// Easy test endpoint.
        /// </summary>
        [Produces(typeof(string))]
        [HttpGet("api/v1/auth/login")]
        public async Task<IActionResult> GetAsync()
        {
           // await _userEventBusHandler.SendMessageAsync("Bajsa");

            return Ok("Bajsa");
        }

        // To call from API Gateway use POST https://localhost:44375/login

        /// <summary>
        /// Used to login user and return the bearer token.
        /// </summary>
        /// <param name="loginCredentials">The login model to pass username and password.</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces(typeof(CleanADUserResponse))]
        [HttpPost("api/v1/auth/login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginCredentials loginCredentials)
        {
            var result = await _azureTokenService.LoginAsync(loginCredentials);
            var cleanADUserResponse = _mapper.Map<ADUserResponse, CleanADUserResponse>(result);
            return Ok(cleanADUserResponse);
        }

        // To call from API Gateway use POST https://localhost:44375/refreshtoken

        /// <summary>
        /// Used to get a new bearer token from refresh token.
        /// </summary>
        /// <param name="refreshToken">The refreshToken.</param>
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [Produces(typeof(string))]
        [HttpPost("api/v1/auth/refreshtoken")]
        public async Task<IActionResult> RedeemRefreshTokenAsync([FromBody] string refreshToken)
        {
            var result = await _azureTokenService.GetRefreshTokenAsync(refreshToken);
            return Ok(result);
        }
    }
}
