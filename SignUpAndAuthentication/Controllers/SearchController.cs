using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using SignUpAndAuthentication.Exceptions;
using SignUpAndAuthentication.Interfaces;
using SignUpAndAuthentication.Logging;
using SignUpAndAuthentication.Models;
using SignUpAndAuthentication.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace SignUpAndAuthentication.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        ICILogger logger;
        ISearchService searchService;

        public SearchController(ICILogger logger, ISearchService searchService)
        {
            this.logger = logger;
            this.searchService = searchService;
        }
      
        /// <summary>
        /// Use this endpoint and HTTP Method (GET) to get User Information
        /// </summary>
        /// <returns>User</returns>
        /// <param name="userId">User identifier.</param>
        [HttpGet, Authorize]
        [Produces("application/json")]
        public IActionResult Get(string userId)
        {
            var token = String.Empty;
            var user = new User();

            if(Request.Headers.TryGetValue("Authorization", out StringValues authToken))
            {
                string authHeader = authToken.First();
                token = authHeader.Substring("Bearer ".Length).Trim();
            }

            try
            {
                user = searchService.GetUser(Guid.Parse(userId), token);
            }
            catch (BusinessException ex)
            {
                logger.LogError("SearchController - GET - Exception when getting User", ex);
                return Unauthorized();
            }

            if(user == null)
            {
                logger.LogError("SearchController - GET - User Not Found");
                return NotFound();
            }

            return Ok(user);

        }     
    }
}
