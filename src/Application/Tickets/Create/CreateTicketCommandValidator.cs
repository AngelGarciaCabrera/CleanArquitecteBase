using System.Net;
using Application.Tickets.Create;
using FluentValidation;

namespace Application.Tickets.Create;

public class CreateTicketsCommandValidator : AbstractValidator<CreateTicketsCommand>
{
    public CreateTicketsCommandValidator()
    {
        RuleFor(v => v.Nombre)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Id)
            .NotEmpty();

        RuleFor(v => v.Descripcion)
          .MaximumLength(200)
          .NotEmpty();
          
        RuleFor(v => v.Estado)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Departamento_Id)
            .NotEmpty();
     
        RuleFor(v => v.FechaDeCreacion)
            .NotEmpty();

        RuleFor(v => v.Prioridad)
            .MaximumLength(50)
            .NotEmpty();
        RuleFor(v=> v.Id)
            .NotEmpty();

        RuleFor(v => v.CreadoPor)
            .NotEmpty();
            

           




    }
}
