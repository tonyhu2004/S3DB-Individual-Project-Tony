using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MySql.Data.MySqlClient;
using S3DB_Individual_Project_Tony.ViewModels;

namespace S3DB_Individual_Project_Tony.CustomFilter;

public class CustomExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if (context.Exception is MySqlException)
        {
            var error = new ErrorModel
            (
                500,
                context.Exception.Message,
                context.Exception.StackTrace
            );

            context.Result = new JsonResult(error);
        }
        else if (context.Exception is InvalidOperationException)
        {
            var error = new ErrorModel
            (
                400,
                context.Exception.Message,
                context.Exception.StackTrace
            );

            context.Result = new JsonResult(error);
        }
        else if (context.Exception is UnauthorizedAccessException)
        {
            var error = new ErrorModel
            (
                403,
                context.Exception.Message,
                context.Exception.StackTrace
            );

            context.Result = new JsonResult(error);
        }
        else
        {
            var error = new ErrorModel
            (
                404,
                context.Exception.Message,
                context.Exception.StackTrace
            );

            context.Result = new JsonResult(error);
        }

        context.ExceptionHandled = true;
    }
}