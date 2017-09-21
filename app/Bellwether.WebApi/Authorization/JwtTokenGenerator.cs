using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Bellwether.WebApi.Authorization
{
  public class JwtTokenGenerator
  {
    private readonly IUserAuthorizationService _userAuthorizationService;
    private readonly JwtOptions _options;

    public JwtTokenGenerator(
        IUserAuthorizationService userAuthorizationService,
        IOptions<JwtOptions> options
        )
    {
      _userAuthorizationService = userAuthorizationService;
      _options = options.Value;
    }

    public async Task GenerateToken(HttpContext context, string userEmail, string password, bool rememberMe = false)
    {
      var identity = await GetIdentity(userEmail, password);

      if (identity == null)
      {
        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid email or password.");
        return;
      }

      var now = DateTime.UtcNow;

      var dateClaim = new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64);
      identity.AddClaim(dateClaim);

      var jwt = new JwtSecurityToken(
          issuer: _options.Issuer,
          audience: _options.Audience,
          claims: identity.Claims,
          notBefore: now,
          expires: _options.Expiration,
          signingCredentials: _options.SigningCredentials);
      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      var cookieOptions = new CookieOptions { };

      if (!rememberMe)
      {
        cookieOptions.Expires = _options.Expiration;
      }
      await context.Response.WriteAsync(text: encodedJwt);//todo change it !
      //context.Response.Cookies.Append(_options.TokenName, encodedJwt, cookieOptions);
    }

    private async Task GetJsonJwt(string encodedJwt, HttpContext context)
    {
      var response = new
      {
        access_token = encodedJwt,
        expires_in = _options.Expiration.Ticks
      };
      context.Response.ContentType = "application/json";
      await context.Response.WriteAsync(JsonConvert.SerializeObject(response, new JsonSerializerSettings { Formatting = Formatting.Indented }));
    }

    private async Task<ClaimsIdentity> GetIdentity(StringValues userEmail, StringValues password)
    {
      var user = await _userAuthorizationService.FindByEmailAsync(userEmail);
      if (user == null)
      {
        return null;
      }
      var userValidatedPassword = await _userAuthorizationService.CheckPasswordAsync(user, password);
      //var userValidatedEmail = await _userAuthorizationService.IsEmailConfirmedAsync(user);
      if (userValidatedPassword)
      {
        var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, userEmail),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
        //todo !!!!
        //var roles = await _userAuthorizationService.GetRolesAsync(user);

        //claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return new ClaimsIdentity(new GenericIdentity(userEmail, "Token"), claims);
      }

      return null;
    }

    private static long ToUnixEpochDate(DateTime date) => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
  }
}
