namespace InfoTrackBackend.Controllers;

using InfoTrackBackend.Contracts;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SolicitorResultsController : ControllerBase
{
    [HttpGet]
    public SolicitorResultsResponseDto Get([FromQuery]string location)
    {
        return new SolicitorResultsResponseDto($"Big gay dance: {location} ");
    }
}



