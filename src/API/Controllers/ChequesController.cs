using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ChequeMicroservice.API.Controllers;
using System;
using System.Threading.Tasks;
using ChequeMicroservice.Application.Cheques.CreateCheques;
using ChequeMicroservice.Application.Cheques.ApproveorRejectChequeRequests;
using ChequeMicroservice.Application.Common.Models;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChequesController : ApiController
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public ChequesController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;

            accessToken = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

            if (accessToken == null)
            {
                throw new UnauthorizedAccessException("You are not authorized!");
            }
        }

        /// <summary>
        /// This api allows you to create cheque request
        /// </summary>
        /// <param name="command">The create cheque request object</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpPost("createchequerequest")]
        public async Task<ActionResult> CreateChequeRequest(CreateChequeRequestCommand command)
        {
            command.AccessToken = accessToken;
            Result createdChequeRequest = await Mediator.Send(command);
            return Ok(createdChequeRequest);
        }


        /// <summary>
        /// This api allows you to approve or reject cheque request.
        /// </summary>
        /// <param name="command">The approve or reject cheque request object</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpPost("approveorrejectchequerequest")]
        public async Task<ActionResult> ApproveorRejectChequeRequest(ApproveorRejectChequeRequestCommand command)
        {
            command.AccessToken = accessToken;
            Result approveOrRejectResponse = await Mediator.Send(command);
            return Ok(approveOrRejectResponse);
        }
    }
}
