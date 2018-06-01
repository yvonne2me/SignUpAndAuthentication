using System;
using System.Diagnostics;
using GenesisAutomation.Interfaces;
using GenesisAutomation.Logging;
using GenesisAutomation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GenesisAutomation.Controllers
{

    [Route("api/[controller]")]
    public class SignUpController : Controller
    {
        ICILogger logger;
        ISignUpService signUpService;

        public SignUpController(ICILogger logger, ISignUpService signUpService)
        {
            this.logger = logger;
            this.signUpService = signUpService;
        }

        /// <summary>
        /// Use this endpoint and HTTP Method (POST) to Sign Up
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="user">User.</param>
        [AllowAnonymous]
        [HttpPost]
        [Produces("application/json")]
        public IActionResult Post([FromBody]User user)
        {
            var timer = new Stopwatch();
            timer.Start();
            logger.LogInfo("In Sign Up Controller");

            if(user == null)
            {
                logger.LogInfo("No User provided - Returning Bad Request");
                return BadRequest("No User Information provided");
            }

            if (user.Email == null || user.Password == null)
            {
                logger.LogInfo("Username OR Password not provided - Returning Bad Request");
                return BadRequest("Username and Password required for Sign Up");
            }

            var response = new AuthorizationResponse();

            try
            {
                response = signUpService.CreateNewUser(user);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }

            timer.Stop();
            logger.LogInfo("Sign Up Contoller Timer took: " + timer.ElapsedMilliseconds);

            return Ok(response);

        }
    }
}
