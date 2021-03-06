using OrderRequest.Domain.Products;
using OrderRequest.Infra.Data;
using OrderRequest.Endpoints;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace OrderRequest.Endpoints.Security;
 public class TokenPost {
  public static string Template => "/token";
  public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
  public static Delegate Handle => Action;
  
  [AllowAnonymous]
  public static IResult Action(LoginRequest loginRequest, 
                                IConfiguration configuration, 
                                UserManager<IdentityUser> userManager,
                                ILogger<TokenPost> log ) {
    log.LogInformation("Getting Token");
    var user = userManager.FindByEmailAsync(loginRequest.Email).Result;

    if (user == null)
      return Results.BadRequest("user not found");

    if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result) 
      return Results.BadRequest("user and password not pass");
    
    var claims = userManager.GetClaimsAsync(user).Result;
    var subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
        new Claim(ClaimTypes.Email, loginRequest.Email),
        new Claim(ClaimTypes.NameIdentifier, user.Id)
      });
    subject.AddClaims(claims);

    var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettings:SecretKey"]);
    var tokenDescriptor = new SecurityTokenDescriptor {
      Subject = subject,
      SigningCredentials = 
        new SigningCredentials(new SymmetricSecurityKey(key),
                                SecurityAlgorithms.HmacSha256Signature),
      Audience=configuration["JwtBearerTokenSettings:Audience"],
      Issuer=configuration["JwtBearerTokenSettings:Issuer"],
      Expires = DateTime.UtcNow.AddSeconds(60)
    };

    var tokenHandler = new JwtSecurityTokenHandler();
    var token = tokenHandler.CreateToken(tokenDescriptor);

    return Results.Ok( 
      new {
        token = tokenHandler.WriteToken(token)  
      });

  }
}