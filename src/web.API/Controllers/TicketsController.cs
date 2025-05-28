using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.Tickets.Create;
using Application.Tickets.Delete;
using Application.Tickets.GetAll;
using Application.Tickets.GetById;
using Application.Tickets.Update;
using Application.Usuario.OptenerPorUsuarioId;
using Application.Usuario.GetByDeparmentId;
using Application.Tickets.GetTicketsByPage;
using Application.Tickets.GetByDepartamentoYPagina;
using Application.Tickets.GetTickesByCreator;




namespace Web.API.Controller;
[Route("api/Tickets")]
[ApiController]
public class Tickets : ApiController
{
    private readonly ISender _mediator;

    public Tickets(ISender mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }

    [HttpPost("Crear")]
    public async Task<IActionResult> Crear([FromBody] CreateTicketsCommand command)
    {
        var createTicketsResult = await _mediator.Send(command);

        return createTicketsResult.Match(
            id => Ok(new { Id = id }), // ? ahora usamos directamente el int
            errors => Problem(detail: string.Join(", ", errors.Select(e => e.Description)))
        );
    }



    [HttpDelete("Delete({id})")] //recibo un id por parametro por la url y lo paso al comando
    public async Task<IActionResult> Eliminar(int id) //recibo el comando de eliminar 
    {
        var TicketsDeleted = await _mediator.Send(new DeleteTicketsCommand(id)); //mando el comando al mediator
        return TicketsDeleted.Match(
            Tickets => Ok(Tickets),
            errors => Problem(errors)
        );
    }

    [HttpGet("GetAll")] // no le paso parametros ya que es get 
    public async Task<IActionResult> OptenerTodo() //mando el comando al mediator
    {
        var Ticketss = await _mediator.Send(new GetAllTicketsCommand());
        return Ticketss.Match(
            Tickets => Ok(Tickets),
            errors => Problem(errors)
        );
    }



    [HttpGet("GetById/{id}")] // Asegúrate de que el parámetro esté entre llaves en la URL
    public async Task<IActionResult> ObtenerPorId(int id) // Usa el id como parámetro en el método
    {
        var Tickets = await _mediator.Send(new GetTicketsByIdCommand(id)); // Pasa solo el id
        return Tickets.Match(
            Tickets => Ok(Tickets),
            errors => Problem(errors)
        );
    }

    [HttpPut("Update({id})")] //recibo un id por parametro por la url y lo paso al comando
    public async Task<IActionResult> Actualizar(UpdateTicketsCommand command) //recibo el comando de actualizar 
    {
        var TicketsUpdated = await _mediator.Send(command); //mando el comando al mediator
        return TicketsUpdated.Match(
            Tickets => Ok(Tickets),
            errors => Problem(errors)
        );
    }

    [HttpGet("GetTicketsByUserId/{User_Id}")]
    public async Task<IActionResult> ObtenerTicketsPorUsuarioId([FromRoute] int User_Id)
    {
        var Tickets = await _mediator.Send(new GetByUserIdCommand(User_Id));

        return Tickets.Match<IActionResult>(
            res => Ok(res),
            errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }
    [HttpGet("GetByDeparmentsId/{Departamento_Id}")]
    public async Task<IActionResult> ObtenerPorDepartamentoID([FromRoute] int Departamento_Id)
    {
        var Tickets = await _mediator.Send(new GetByDeparmentIdCommand(Departamento_Id));

        return Tickets.Match<IActionResult>(
            res => Ok(res),
            errors => BadRequest(new { Errors = errors.Select(e => new { e.Code, e.Description }) })
        );
    }
    [HttpGet("GetByPage")] //un get que espesifico la paginas  y el limite
    public async Task<IActionResult> ObtenerPorPagina([FromQuery] int page, [FromQuery] int limit)
    {
        var tickets = await _mediator.Send(new GetTicketsByPageCommand(page, limit));

        return tickets.Match(
            resultado => Ok(resultado),
            errors => Problem(errors)
        );
    }
    [HttpGet("GetByDepartamentoAndPage")]
    public async Task<IActionResult> ObtenerPorDepartamentoYPagina([FromQuery] int departamentoId, [FromQuery] int page, [FromQuery] int limit)
    {
        if (departamentoId <= 0 || page < 1 || limit <= 0)
            return BadRequest("Parámetros inválidos.");

        var result = await _mediator.Send(new GetTicketsByDepartamentoYPaginaCommand(departamentoId, page, limit));

        return result.Match(
            tickets => Ok(tickets),
            errors => Problem(errors)
        );
    }

    [HttpGet("GetPagesByUserId")]
    public async Task<IActionResult> OptenerPaginasPorUserId([FromQuery] int user_Id, [FromQuery] int page, [FromQuery] int limit)
    {
        if (user_Id <= 0 || page < 1 || limit <= 0)
            return BadRequest("Parámetros inválidos.");

        var result = await _mediator.Send(new GetPageByUserIdCommand(user_Id, page, limit));

        return result.Match(
            tickets => Ok(tickets),
            errors => Problem(errors)
        );
    }


    [HttpGet("GetPagesByUserCreator")]
    public async Task<IActionResult> OptenerPaginaDeTicketPorUsuarioCreador([FromQuery] string usuarioCorreo, [FromQuery] int page, [FromQuery] int limit)
    {
        if (usuarioCorreo == "" || page < 1 || limit <= 0)
            return BadRequest("Parámetros inválidos.");

        var result = await _mediator.Send(new GetTicketsByCreatorCommand(usuarioCorreo, page, limit));

        return result.Match(
            tickets => Ok(tickets),
            errors => Problem(errors)
        );
    }








}

