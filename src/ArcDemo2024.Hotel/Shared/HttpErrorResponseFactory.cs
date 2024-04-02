using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArcDemo2024.Hotel.Shared;

public sealed class HttpErrorResponseFactory : IService<HttpErrorResponseFactory>
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HttpErrorResponseFactory(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
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