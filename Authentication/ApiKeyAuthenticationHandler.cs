using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace SimpleAPI.Authentication
{
    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public string? ApiKey { get; set; }
    }
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        public ApiKeyAuthenticationHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
        {
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue("ApiKey", out var apiKeyValues))
            {
                return AuthenticateResult.Fail("Missing API Key");
            }

            var providedApiKey = apiKeyValues.FirstOrDefault();
            var expectedApiKey = Options.ApiKey;

            if (string.IsNullOrEmpty(providedApiKey) || providedApiKey != expectedApiKey)
            {
                return AuthenticateResult.Fail("Invalid API Key");
            }

            var claims = new[] { new Claim(ClaimTypes.Name, "APIKeyUser") };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        public static string GenerateApiKey()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
