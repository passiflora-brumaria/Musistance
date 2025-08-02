using System;
using System.Net.Http;
using System.Threading.Tasks;
using MusistanceClient.Api.Dto.Auth;
using MusistanceClient.Api.Dto.Profile;
using Newtonsoft.Json;

namespace MusistanceClient.Api.Modules;

/// <summary>
/// Profile API connections.
/// </summary>
public class ProfileModule {
    private readonly HttpClient _http;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="config">API configuration.</param>
    public ProfileModule (ApiConfiguration config)
    {
        _http = new HttpClient()
        {
            BaseAddress = new Uri(config.BaseUrl + "profiles/")
        };
        if (!String.IsNullOrEmpty(config.BearerToken))
        {
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer",config.BearerToken);
        }
    }

    /// <summary>
    /// Gets the user's own profile.
    /// </summary>
    /// <returns>The found profile.</returns>
    public async Task<ProfileDto> GetOwnProfileAsync ()
    {
        HttpResponseMessage response = await _http.GetAsync("self");
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<ProfileDto>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// Gets a user's profile given their ID.
    /// </summary>
    /// <param name="id">ID of the user to search.</param>
    /// <returns>The found profile.</returns>
    public async Task<ProfileDto> GetProfileAsync (int id)
    {
        HttpResponseMessage response = await _http.GetAsync(id.ToString());
        if (response.IsSuccessStatusCode)
        {
            return JsonConvert.DeserializeObject<ProfileDto>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            return null;
        }
    }
}