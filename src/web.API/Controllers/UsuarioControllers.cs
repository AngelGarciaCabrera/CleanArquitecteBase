using Application.Usuario.Create;
using Application.Usuario.Delete;
using Application.Usuario.GetAll;
using Application.Usuario.GetById;
using Application.Usuario.GetUserByDeparmentId;
using Application.Usuario.Login;
using Application.Usuario.Update;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controller;

[ApiController]
[Route("api/Usuario")]
public class Usuario : ApiController
{
    private readonly ISender _mediator;

    public Usuario(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("Crear")]       // espesifico el metodo que llamara esta funcion por httpPost
    // aqui definimos y tomamos el tipo de comando y se lo pasamos al mediator, si falla lo enviamos a ErorsProblemDetailsFactory
    public async Task<IActionResult> Crear([FromBody] CreateUsuarioCommand command)
    {
        var createUsuarioResult = await _mediator.Send(command);
        // le mando el query al mediator y si falla lo mando a ErorsProblemDetailsFactory

        return createUsuarioResult.Match<IActionResult>(
        usuario => Ok(),
        // dependiendo la respusta pues devuelvo un Ok o un Problem
        errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }

    [HttpDelete("Delete({id})")] //recibo un id por parametro por la url y lo paso al comando
    public async Task<IActionResult> Delete(int id) //recibo el comando de eliminar 
    {
        var UsuarioDeleted = await _mediator.Send(new DeleteUsuarioCommand(id)); //mando el comando al mediator
        return UsuarioDeleted.Match<IActionResult>(
            usuario => Ok(usuario),
            errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }

    [HttpGet("GetAll")]
    public async Task<IActionResult> OptenerTodo()
    {
        var usuarios = await _mediator.Send(new GetAllUsuarioCommand());
        return usuarios.Match<IActionResult>(
            usuario => Ok(usuario),
            errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }
    [HttpGet("GetById/{id}")] // Asegúrate de que el parámetro esté entre llaves en la URL
    public async Task<IActionResult> ObtenerPorId(int id) // Usa el id como parámetro en el método
    {
        var Usuario = await _mediator.Send(new GetUsuarioByIdCommand(id)); // Pasa solo el id
        return Usuario.Match<IActionResult>(
            res => Ok(res),
            errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }

    [HttpPut("Update({id})")] //recibo un id por parametro por la url y lo paso al comando
    public async Task<IActionResult> Actualizar(UpdateUsuarioCommand command) //recibo el comando de actualizar 
    {
        var UsuarioUpdated = await _mediator.Send(command); //mando el comando al mediator
        return UsuarioUpdated.Match<IActionResult>(
            res => Ok(res),
            errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUsuarioCommand command)
    {
        var result = await _mediator.Send(command);

        return result.Match<IActionResult>( //para que devuelva los errores que espero
            usuario => Ok(usuario),
            errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }

    [HttpGet("GetUserByDeparmentId/{Departamento_Id}")]
    public async Task<IActionResult> ObtenerUsuariosPorDepartamentoId([FromRoute] int Departamento_Id)
    {
        var usuarios = await _mediator.Send(new GetUserByDeparmentIdCommand(Departamento_Id));

        return usuarios.Match<IActionResult>(
            res => Ok(res),
            errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }





}