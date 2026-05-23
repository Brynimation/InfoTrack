using InfoTrackBackend.Clients;
using InfoTrackBackend.Contracts;

namespace InfoTrackBackend.Services;

public class SolicitorsService(SolicitorsClient client, IParsingService parser) : ISolicitorsService
{
    #region Public Methods

    public async Task<List<SolicitorResultsResponseDto>> GetSolicitorsResultsAsync(string speciality, string location)
    {
        var httpContent = await client.GetSolicitorsBySpeciality(speciality, location);
        var parsed = parser.Parse(httpContent);
        return parsed;
    }

    #endregion
}

