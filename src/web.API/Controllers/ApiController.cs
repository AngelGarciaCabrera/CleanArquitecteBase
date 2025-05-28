

using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using web.API.Common.Errors;

namespace Web.API.Controller;

public class ApiController : ControllerBase
{
    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count == 1)
        {
            return Problem();
        }
        if (errors.All(e => e.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }
        HttpContext.Items[HttpContextItemKeys.Error] = errors;
        return Problem(errors[0]);
    }

    private IActionResult Problem(Error error)
    {
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError,

        };
        return Problem(statusCode: statusCode, title: error.Description);
    }
    private IActionResult ValidationProblem(List<Error> errors)
    {
       var modelStateDictionary = new ModelStateDictionary();
       foreach (var error in errors)
       {
           modelStateDictionary.AddModelError(error.Code, error.Description);
       }
       return ValidationProblem(modelStateDictionary);
    }
    

}
