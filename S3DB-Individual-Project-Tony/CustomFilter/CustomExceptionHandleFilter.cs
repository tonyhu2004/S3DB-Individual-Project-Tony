using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using S3DB_Individual_Project_Tony.ViewModels;

namespace S3DB_Individual_Project_Tony.CustomFilter;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is NotFoundException)
        {
            var error = new ErrorModel
            (
                404,
                context.Exception.Message,
                context.Exception.StackTrace
            );

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(error);
        }
        else if (context.Exception is BadRequestException)
        {
            var error = new ErrorModel
            (
                400,
                context.Exception.Message,
                context.Exception.StackTrace
            );

            context.HttpContext.Response.StatusCode = 400;
            context.Result = new JsonResult(error);
        }
        else if (context.Exception is ForbiddenException)
        {
            var error = new ErrorModel
            (
                403,
                context.Exception.Message,
                context.Exception.StackTrace
            );

            context.HttpContext.Response.StatusCode = 403;
            context.Result = new JsonResult(error);
        }
        else
        {
            var error = new ErrorModel
            (
                500,
                context.Exception.Message,
                context.Exception.StackTrace
            );

            context.HttpContext.Response.StatusCode = 500;
            context.Result = new JsonResult(error);
        }

        context.ExceptionHandled = true;
    }
}