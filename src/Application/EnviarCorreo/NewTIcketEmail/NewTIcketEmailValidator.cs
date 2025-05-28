
using Application.EnviarCorreo.NewTIcketEmail;
using FluentValidation;

namespace Application.EnviarCorreo.NewTIcketEmail;

public class NewTIcketEmailCommandValidator : AbstractValidator<NewTIcketEmailCommand>
{
    public  NewTIcketEmailCommandValidator()
    {
        RuleFor(v => v.id)
            .NotEmpty();
        RuleFor(v=> v.recipientEmail)
            .NotEmpty()
            .MaximumLength(255);
        RuleFor(v=> v.FechaDeCreacion)
            .NotEmpty();
        RuleFor(v=> v.CreadoPor)
            .NotEmpty();
    }
}

