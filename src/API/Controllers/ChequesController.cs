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
using ChequeMicroservice.Application.Cheques.Queries;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChequesController : ApiController
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public ChequesController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            accessToken = _httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString();
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

        /// <summary>
        /// This api retrieves a single cheque
        /// </summary>
        /// <param name="chequeId">The cheque Id</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpGet("getchequebyid/{chequeId}")]
        public async Task<ActionResult> GetChequeById(int chequeId)
        {
            Result result = await Mediator.Send(new GetChequeByIdQuery { ChequeId = chequeId });
            return Ok(result);
        }

        /// <summary>
        /// This api retrieves all cheques
        /// </summary>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpGet("getchequelist")]
        public async Task<ActionResult> GetChequeList()
        {
            Result result = await Mediator.Send(new GetAllChequesQuery());
            return Ok(result);
        }
    }
}
