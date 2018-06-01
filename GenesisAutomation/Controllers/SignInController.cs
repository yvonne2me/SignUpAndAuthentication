using System;
using GenesisAutomation.Interfaces;
using GenesisAutomation.Logging;
using GenesisAutomation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace GenesisAutomation.Controllers
{
    [Route("api/[controller]")]
    public class SignInController : Controller
    {
        ICILogger logger;
        ISignInService signInService;

        public SignInController(ICILogger logger, ISignInService signInService)
        {
            this.logger = logger;
            this.signInService = signInService;
        }
      
        /// <summary>
        /// Use this endpoint and HTTP Method (POST) to sign in.
        /// Requires Name & Password
        /// </summary>
        /// <returns>Authorization Response </returns>
        /// <param name="signInRequest">Sign in request.</param>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Post([FromBody] SignInRequest signInRequest)
        {
            if(signInRequest == null)
            {
                logger.LogInfo("No Sign In Information provided - Returning Bad Request");
                return BadRequest("No Sign In Information provided");
            }

            var response = new AuthorizationResponse();
            try
            {
                response = signInService.LogIn(signInRequest);  
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }


            return Ok(response);
        }       
    }
}
