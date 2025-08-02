using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using MusistanceClient.Api.Dto.Auth;
using Newtonsoft.Json;

namespace MusistanceClient.Api.Modules;

/// <summary>
/// Auth API connections.
/// </summary>
public class AuthModule {
    private readonly HttpClient _http;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="config">API configuration.</param>
    public AuthModule (ApiConfiguration config)
    {
        _http = new HttpClient()
        {
            BaseAddress = new Uri(config.BaseUrl + "auth/")
        };
        if (!String.IsNullOrEmpty(config.BearerToken))
        {
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",config.BearerToken);
        }
    }

    /// <summary>
    /// Creates a new account using itch OAuth.
    /// </summary>
    /// <returns>An auth session with a challenge URL for profile import.</returns>
    public async Task<AuthSessionDto> ItchSignupAsync ()
    {
        HttpResponseMessage response = await _http.GetAsync("signup/itch");
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<AuthSessionDto>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Performs a login operation. If the profile is not yet ready for use, returns back the login data for future retry.
    /// </summary>
    /// <param name="id">User ID, used as a username.</param>
    /// <param name="code">Validation code obtained at signup (or previous login), used as a password.</param>
    /// <returns>The auth session, which includes an access token and the next validation code.</returns>
    public async Task<AuthSessionDto> LoginAsync (int id, string code)
    {
        HttpResponseMessage response = await _http.PostAsync($"login/{id}",JsonContent.Create(code));
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<AuthSessionDto>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            return null;
        }
    }
}