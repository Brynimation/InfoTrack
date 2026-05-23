namespace InfoTrackBackend.Controllers;

using InfoTrackBackend.Contracts;
using InfoTrackBackend.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class SolicitorResultsController(ISolicitorsService solicitorsService) : ControllerBase
{
    [HttpGet]
    public async Task<List<SolicitorResultsResponseDto>> Get([FromQuery]string location)
    {
        var scrapedData = await solicitorsService.GetSolicitorsResultsAsync("conveyancing", location.ToLowerInvariant());
        return scrapedData;
    }
}



