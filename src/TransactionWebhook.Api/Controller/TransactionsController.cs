using Microsoft.AspNetCore.Mvc;
using TransactionWebhook.Application.DTO;
using TransactionWebhook.Application.Service;

namespace TransactionWebhook.Api.Controller
{
    [ApiController]
    [Route("webhooks/transactions")]
    //[Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _service;

        public TransactionsController(
            ITransactionService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            TransactionRequest request)
        {         

            try
            {
                await _service.ProcessAsync(request);

                return Ok(new
                {
                    Success = true,
                    Message = "Transaction processed successfully."
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }

            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    Success = false,
                    Message = ex.Message
                });
            }
        }
    }
}
