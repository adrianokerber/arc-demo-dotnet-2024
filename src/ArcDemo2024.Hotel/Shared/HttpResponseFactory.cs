using ArcDemo2024.Hotel.Shared.ResultPattern.CSharpFuncionalExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcDemo2024.Hotel.Shared;

// TODO: Un improvement could be split the methods of success and failure. Ex: SuccessWith200() instead of Create200().
public sealed class HttpResponseFactory : IService<HttpResponseFactory>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpResponseFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public IActionResult Create200(string message, object body)
    {
        return new OkObjectResult(
            new
            {
                Status = StatusCodes.Status200OK,
                Mensagem = message,
                Data = body
            });
    }

    public IActionResult Create400(string title, string detail)
    {
        return new BadRequestObjectResult(new ProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = title,
            Type = "Failure",
            Detail = detail,
            Instance = _httpContextAccessor.HttpContext!.Request.Path
        });
    }

    public IActionResult Create400WithMessages(string title, string detail, ResultAggregate errors)
    {
        var problems = new ProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = title,
            Type = "Failure",
            Detail = detail,
            Instance = _httpContextAccessor.HttpContext!.Request.Path,
        };
        foreach (var error in errors.NestedResults)
        {
            problems.Extensions.Add(error.Key, error.Value.Error);
        }

        return new BadRequestObjectResult(problems);
    }

    public IActionResult Create400WithMessages<T>(string title, string detail, ResultAggregate<T> errors)
    {
        var problems = new ProblemDetails()
        {
            Status = StatusCodes.Status400BadRequest,
            Title = title,
            Type = "Failure",
            Detail = detail,
            Instance = _httpContextAccessor.HttpContext!.Request.Path,
        };
        foreach (var error in errors.NestedResults)
        {
            problems.Extensions.Add(error.Key, error.Value.Error);
        }

        return new BadRequestObjectResult(problems);
    }
}