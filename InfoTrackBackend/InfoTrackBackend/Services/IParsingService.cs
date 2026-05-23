using InfoTrackBackend.Contracts;

namespace InfoTrackBackend.Services;

public interface IParsingService
{
    public List<SolicitorResultsResponseDto> Parse(string input);
}

