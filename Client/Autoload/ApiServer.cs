using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Godot;
using MusistanceClient.Api;
using MusistanceClient.Api.Dto.Auth;
using MusistanceClient.Api.Dto.Profile;
using MusistanceClient.Api.Modules;
using Newtonsoft.Json;

namespace MusistanceClient.Autoload;

/// <summary>
/// API access.
/// </summary>
public partial class ApiServer: Node
{
    #region Public API

    /// <summary>
    /// Gets this singleton from autoload.
    /// </summary>
    /// <param name="context">Node in the scene tree.</param>
    /// <returns>The singleton instance.</returns>
    public static ApiServer GetInstance (Node context)
    {
        return context.GetNode<ApiServer>("/root/ApiServer");
    }

    /// <summary>
    /// Configures the API connection. It must be done before calling any other mention.
    /// </summary>
    /// <param name="config">API configuration.</param>
    public void Configure (ApiConfiguration config)
    {
        _config = config;
    }

    /// <summary>
    /// Signs up or logs in through itch.
    /// </summary>
    /// <returns>Whether the operation was successful.</returns>
    public async Task<bool> InitialiseThroughItchAsync ()
    {
        AuthModule mod = new AuthModule(_config);
        AuthSessionDto session = await mod.ItchSignupAsync();
        if (session == null)
        {
            return false;
        }
        OS.ShellOpen(session.ChallengeUrl);
        int sourceId = 0;
        while (sourceId == 0)
        {
            session = await mod.LoginAsync(session.UserId,session.NextValidationCode);
            if (String.IsNullOrEmpty(session.AccessToken))
            {
                await Task.Delay(1000);
            }
            else
            {
                _latestSession = session;
                JwtSecurityToken token = new JwtSecurityToken(session.AccessToken);
                if (token.Claims.Any(c => c.Type == "sri"))
                {
                    sourceId = int.Parse(token.Claims.Where(c => c.Type == "sri").First().Value);
                }
            }
        }
        _config.BearerToken = session.AccessToken;
        return true;
    }

    /// <summary>
    /// Gets one's own profile. The API must be initialised beforehand.
    /// </summary>
    /// <returns>The user's profile.</returns>
    public async Task<ProfileDto> GetProfileAsync ()
    {
        if (_latestSession == null)
        {
            return null;
        }
        else
        {
            return await new ProfileModule(_config).GetOwnProfileAsync();
        }
    }

    #endregion

    #region Events

    #endregion

    #region Fields

    private ApiConfiguration _config;
    private AuthSessionDto _latestSession;

    #endregion
}