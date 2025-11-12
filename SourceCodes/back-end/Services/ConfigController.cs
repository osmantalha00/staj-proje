using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public ConfigController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("port")]
    public ActionResult<string> GetPort()
    {
        return _configuration.GetSection("ConnectionSettings:Port").Value;
    }
}
