
using FluentValidation;

namespace Application.EnviarCorreo.UserCorreo;

public class EnviarUserCorreoCommandValidator : AbstractValidator<EnviarUserCorreo>
{
    public  EnviarUserCorreoCommandValidator()
    {
        RuleFor(v => v.userName)
            .NotEmpty()
            .MaximumLength(255);
        
        RuleFor(v=> v.recipientEmail)
            .NotEmpty()
            .MaximumLength(255);
    }
}

