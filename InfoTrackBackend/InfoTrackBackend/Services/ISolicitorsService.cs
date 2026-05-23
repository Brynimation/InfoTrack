using InfoTrackBackend.Contracts;

namespace InfoTrackBackend.Services;

public interface ISolicitorsService
{
    public Task<List<SolicitorResultsResponseDto>> GetSolicitorsResultsAsync(string speciality, string location);
}
