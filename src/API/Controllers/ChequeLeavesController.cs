using ChequeMicroservice.API.Controllers;
using ChequeMicroservice.Application.ChequeLeaves.Commands;
using ChequeMicroservice.Application.ChequeLeaves.Queries;
using ChequeMicroservice.Application.Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChequeLeavesController : ApiController
    {
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public ChequeLeavesController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            accessToken = _httpContextAccessor.HttpContext.Request.Headers.Authorization.ToString();
            if (accessToken == null)
            {
                throw new UnauthorizedAccessException("You are not authorized!");
            }
        }

        /// <summary>
        /// This api allows you to stop a single cheque leaf.
        /// </summary>
        /// <param name="leafnumber">Leaf number to stop a single cheque leaf</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpPost("stopchequeleaf/{leafnumber}")]
        public async Task<ActionResult> StopChequeLeaf(string leafnumber)
        {
            Result result = await Mediator.Send(new StopChequeLeafCommand { LeafNumber = leafnumber });
            return Ok(result);
        }

        /// <summary>
        /// This api retrieves cheque leaves
        /// </summary>
        /// <param name="chequeId">The cheque cheque id</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpGet("getchequeleaves/{skip}/{take}/{chequeId}")]
        public async Task<ActionResult> GetChequeLeaves(int skip, int take, int chequeId)
        {
            Result checkLeavesRetrievalResult = await Mediator.Send(new GetChequeLeavesQuery { Skip = skip, Take = take, ChequeId = chequeId });
            return Ok(checkLeavesRetrievalResult);
        }

        /// <summary>
        /// This api retrieves a single cheque leaf
        /// </summary>
        /// <param name="leafNumber">The cheque leaf number</param>
        /// <returns>Returns the Result object either success/failure</returns>
        [HttpGet("confirmchequeleaf/{leafNumber}")]
        public async Task<ActionResult> ConfirmChequeLeaf(string leafNumber)
        {
            Result checkLeavesRetrievalResult = await Mediator.Send(new ConfirmChequeLeafQuery { LeafNumber = leafNumber });
            return Ok(checkLeavesRetrievalResult);
        }
    }
}
