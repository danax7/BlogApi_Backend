using System.Data.Common;
using BlogApi.DTO;
using BlogApi.Exception;
using Microsoft.AspNetCore.Http.Extensions;


namespace FoodDeliveryApplication.Service;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }

        catch (NotAuthorizedException e)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            MakeResponse("error", e.Message, context);
        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            MakeResponse("error", e.Message, context);
        }
        catch (BadRequestException e)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            MakeResponse("error", e.Message, context);
        }

        catch (ForbiddenException e)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            MakeResponse("error", e.Message, context);
        }

        catch (System.Exception e)
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            MakeResponse("error", e.Message, context);
        }
    }

    private async void MakeResponse(String status, String message, HttpContext context)
    {
        await context.Response.WriteAsJsonAsync(new ResponseDto()
        {
            message = message,
            status = status
        });
    }
}