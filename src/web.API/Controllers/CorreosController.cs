using Application.EnviarCorreo.InternalCorreo;
using Application.EnviarCorreo.NewTIcketEmail;
using Application.EnviarCorreo.UserCorreo;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.API.Controller;

[ApiController]
[Route("api/Correos")]
public class CorreosController : ControllerBase
{
    private readonly ISender _mediator;

    public CorreosController(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("EnviarUsuario")]  //Enviar Agradecimiento al usuario por su Sugerencia
    public async Task<IActionResult> EnviarCorreoUsuario([FromBody] EnviarUserCorreo command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(new { mensaje = "Correo enviado al usuario correctamente" }),
            errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
        );
    }
    [HttpPost("EnviarInternal")]  //Enviar Correo Interno Al recibir sugerencia
    public async Task<IActionResult> EnviarCorreoInternal([FromBody] EnviarInternalCorreo command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(new { mensaje = "Correo enviado al Correo Interno correctamente" }),
            errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
        );
    }
     [HttpPost("NewTicketEmail")]  //Enviar Correo Interno Al recibir sugerencia
    public async Task<IActionResult> EnviarCorreoAlDepartamento([FromBody] NewTIcketEmailCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match(
            _ => Ok(new { mensaje = "Correo se ha enviado al departamento correspondiente" }),
            errors => Problem(string.Join(", ", errors.Select(e => e.Description)))
        );
    }
}
 