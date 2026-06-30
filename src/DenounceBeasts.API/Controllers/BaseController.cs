using DenounceBeasts.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace DenounceBeasts.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    public readonly ApplicationDbContext Context;

    public BaseController(ApplicationDbContext dbContext)
    {
        Context = dbContext;
    }
}
