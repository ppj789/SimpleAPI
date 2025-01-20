using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using SimpleAPI.Authentication.Service;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SimpleAPI.Authentication
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly CacheService _cacheService;

        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder, CacheService cacheService) : base(options, logger, encoder)
        {
            _cacheService = cacheService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(ApiKeyAuthenticationOptions.HeaderName, out var apiKey) || apiKey.Count != 1)
            {
                Logger.LogWarning("An API request was received without the x-api-key header");
                return AuthenticateResult.Fail("Invalid parameters");
            }

            var clientId = await _cacheService.GetClientIdFromApiKey(apiKey);

            if (clientId == null)
            {
                Logger.LogWarning($"An API request was received with an invalid API key: {apiKey}");
                return AuthenticateResult.Fail("Invalid parameters");
            }

            Logger.BeginScope("{ClientId}", clientId);
            Logger.LogInformation("Client authenticated");

            var claims = new[] { new Claim(ClaimTypes.Name, clientId.ToString()) };
            var identity = new ClaimsIdentity(claims, ApiKeyAuthenticationOptions.DefaultScheme);
            var identities = new List<ClaimsIdentity> { identity };
            var principal = new ClaimsPrincipal(identities);
            var ticket = new AuthenticationTicket(principal, ApiKeyAuthenticationOptions.DefaultScheme);

            return AuthenticateResult.Success(ticket);
        }
    }
}
