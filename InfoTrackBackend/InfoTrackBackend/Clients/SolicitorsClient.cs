namespace InfoTrackBackend.Clients;

public class SolicitorsClient
{

    #region Public Constructors

    public SolicitorsClient(HttpClient httpClient, ILogger<SolicitorsClient> logger)
    {
        this._httpClient = httpClient;
        this._logger = logger;
    }

    #endregion

    #region Public Methods

    public async Task<string> GetSolicitorsBySpeciality(string speciality, string location)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{speciality}+{location}.html");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        catch (HttpRequestException e)
        {
            _logger.LogError($"Get request failed with code {e.StatusCode}. Exception: {e.Message}");
            return "";
        }
    }

    #endregion

    #region Private Members

    private readonly HttpClient _httpClient;
    private readonly ILogger<SolicitorsClient> _logger;

    #endregion
}
