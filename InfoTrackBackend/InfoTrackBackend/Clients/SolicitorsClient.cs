namespace InfoTrackBackend.Clients;

public class SolicitorsClient
{

    #region Public Constructors

    public SolicitorsClient(HttpClient httpClient) 
    {
        this._httpClient = httpClient;
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
            return "FAILED!";
        }
    }

    #endregion

    #region Private Members

    private readonly HttpClient _httpClient;

    #endregion
}
