using System.Data;
using FluentValidation;

namespace Application.Tickets.Update;

public class UpdateTicketsCommandValidation : AbstractValidator<UpdateTicketsCommand>
{
    public UpdateTicketsCommandValidation()
    {
        RuleFor(v => v.Nombre)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Descripcion)
            .MaximumLength(200)
            .NotEmpty();

        RuleFor(v => v.Estado)
            .MaximumLength(200)
            .NotEmpty();
        
        RuleFor(v => v.Id)
            .NotEmpty();
        
        RuleFor(v => v.Departamento_Id)
            .NotEmpty();

        RuleFor(v => v.FechaDeCreacion)
            .NotEmpty()
            .WithMessage("La fecha de creación no puede estar vacía o inválida."); 
        RuleFor(v => v.Prioridad)
            .MaximumLength(50)
            .NotEmpty()
            .WithMessage("La prioridad no puede estar vacía o inválida.");
        RuleFor(v => v.CreadoPor)
            .NotEmpty()
            .WithMessage("El correo proporcionado no es válido.");

    }
}
