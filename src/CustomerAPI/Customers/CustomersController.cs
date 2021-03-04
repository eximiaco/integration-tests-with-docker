using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CustomerAPI.Customers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<OkResult> ChangeName(ChangeCustomerNameModel model)
        {
            var command = new ChangeCustomerNameCommand(model.Id, model.Name);
            var result = await _mediator.Send(command);
            if (result.IsFailure) BadRequest();
            return Ok();
        }
    }
}
