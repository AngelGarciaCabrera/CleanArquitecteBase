
using FluentValidation;

namespace Application.EnviarCorreo.InternalCorreo;

public class EnviarInternalCorreoCommandValidator : AbstractValidator<EnviarInternalCorreo>
{
    public  EnviarInternalCorreoCommandValidator()
    {
        RuleFor(v => v.CentroId)
            .NotEmpty();

        
        RuleFor(v=> v.correo)
            .NotEmpty()
            .MaximumLength(255);
        
        RuleFor(v=> v.Descripcion)
            .NotEmpty()
            .MaximumLength(400)
            .MinimumLength(10);
            
            RuleFor(v => v.Fecha)
            .NotEmpty()
            .MaximumLength(50);
        
        RuleFor(v=> v.Seccion)
            .NotEmpty()
            .MaximumLength(100);
        RuleFor(v=> v.CentroNombre)
            .NotEmpty();
        
        RuleFor(v=> v.Telefono)
            .NotEmpty()
            .MaximumLength(12);
        
            RuleFor(v => v.Usuario_Nombre)
            .NotEmpty()
            .MaximumLength(255);
    }
}

