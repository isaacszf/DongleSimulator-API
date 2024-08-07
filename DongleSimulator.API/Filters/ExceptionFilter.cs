using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shared.Exceptions;
using Shared.Exceptions.Base;
using Shared.Responses;

namespace DongleSimulator.Filters;

public class ExceptionFilter: IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is DongleSimulatorException) HandleProjectException(context);
        else HandleUnknownException(context);
    }

    // TO-DO - make this better
    private static void HandleProjectException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ErrorOnValidation exp:
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                context.Result = new BadRequestObjectResult(new ResponseErrorJson(exp.ErrorsMessages));
                break;
            
            case NotFoundException exp:
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.NotFound;
                context.Result = new NotFoundObjectResult(new ResponseErrorJson(exp.Message));
                break;
            
            case UnauthorizedException:
                context.HttpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;
                context.Result = new EmptyResult();
                break;
        }
    }
    
    private static void HandleUnknownException(ExceptionContext context)
    {
        var exp = context.Exception;
        Console.WriteLine(exp.ToString());
        
        context.HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        context.Result = new ObjectResult(new ResponseErrorJson(ResourceExceptionMessages.UNKNOWN_ERR));
    }
}