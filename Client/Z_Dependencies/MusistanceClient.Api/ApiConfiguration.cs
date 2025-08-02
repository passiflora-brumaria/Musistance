namespace MusistanceClient.Api;

public class ApiConfiguration
{
    private string _baseUrl;
    private string _bearerToken;

    /// <summary>
    /// Base URL for the API.
    /// </summary>
    public string BaseUrl => _baseUrl;
    /// <summary>
    /// Auth bearer token.
    /// </summary>
    public string BearerToken
    {
        get => _bearerToken;
        set => _bearerToken = value;
    }

    /// <summary>
    /// Default constructor.
    /// </summary>
    /// <param name="baseUrl">Base URL for the HTTP client.</param>
    public ApiConfiguration (string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    /// <summary>
    /// Gets the config for local server testing.
    /// </summary>
    public static ApiConfiguration Local ()
    {
        return new ApiConfiguration ("https://localhost:7093/api/");
    }

    
}
