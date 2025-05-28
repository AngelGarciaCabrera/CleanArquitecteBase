
using FluentValidation;

namespace Application.Excel;

public class GenerateExcelCommandValidator : AbstractValidator<GenerateExcelCommand>
{
    public  GenerateExcelCommandValidator()
    {
        RuleFor(v => v.Data)
            .NotEmpty();
        
        RuleFor(v=> v.Columns)
            .NotEmpty();
    }
}

