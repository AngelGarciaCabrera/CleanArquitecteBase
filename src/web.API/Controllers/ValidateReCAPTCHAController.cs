using Application.ValidateReCAPTCHA;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.API.Controller
{
    [ApiController]
    [Route("api/Recaptcha")]
    public class ValidateReCAPTCHAController : ControllerBase
    {
        private readonly ISender _mediator;

        public ValidateReCAPTCHAController(ISender mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPost("Validate")]  // Ruta para validar el reCAPTCHA
        public async Task<IActionResult> ValidateRecaptcha([FromBody] ValidateReCAPTCHACommnad command)
        {
            var result = await _mediator.Send(command);

            return result.Match(
                _ => Ok(new { message = "reCAPTCHA validation successful" }),
                errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
            );
        }
    }
}
