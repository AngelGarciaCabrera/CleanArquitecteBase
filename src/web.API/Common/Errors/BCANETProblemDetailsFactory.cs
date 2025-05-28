using System.Diagnostics;
using System.Net;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace web.API.Common.Errors;

public class BCANETProblemDetailsFactory : ProblemDetailsFactory
{
    
    private readonly ApiBehaviorOptions _options;
    public BCANETProblemDetailsFactory(ApiBehaviorOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }

    public override ProblemDetails CreateProblemDetails(HttpContext httpContext,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null, string? instance = null)
    {
        statusCode ??= 500;
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Type = type,
            Detail = detail,
            Instance = instance
        };
        ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);
        return problemDetails;
    }
    public override ValidationProblemDetails CreateValidationProblemDetails(
        HttpContext httpContext,
        ModelStateDictionary modelStateDictionary,
        int? statusCode = null,
        string? title = null,
        string? type = null,
        string? detail = null, string? instance = null)
    {
        if(modelStateDictionary is null)
            {
                throw new ArgumentNullException(nameof(modelStateDictionary));
            }
            statusCode ??= 400;

            var problemDetails = new ValidationProblemDetails(modelStateDictionary)
            {
                Status = statusCode,
                Title = title,
                Type = type,
                Detail = detail,
                Instance = instance
            };
            if(title != null){
                problemDetails.Title = title;
            }
             ApplyProblemDetailsDefaults(httpContext, problemDetails, statusCode.Value);
            return problemDetails;
    }
  
    private void ApplyProblemDetailsDefaults(HttpContext httpContext, ProblemDetails problemDetails, int? statusCode)
    {
        problemDetails.Status ??= statusCode;
        //mapear por si hay errores de cliente
        if(statusCode.HasValue && _options.ClientErrorMapping.TryGetValue(statusCode.Value, out var clientErrorData))
        {
            problemDetails.Title ??= clientErrorData.Title;
            problemDetails.Type ??= clientErrorData.Link; 
        }
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        if (traceId is not null)
        {
            problemDetails.Extensions["traceId"] = traceId;
        }
        var error = httpContext.Items[HttpContextItemKeys.Error] as List<Error>;
        if (error is not null)
        {
            problemDetails.Extensions.Add("errorCode", error.Select(x => x.Code));
        }
    }

}