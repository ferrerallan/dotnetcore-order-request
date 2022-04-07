using OrderRequest.Domain.Products;
using OrderRequest.Infra.Data;
using OrderRequest.Endpoints;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace OrderRequest.Endpoints.Employees;
 public class EmployeePost {
  public static string Template => "/employees";
  public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
  public static Delegate Handle => Action;
  public static async Task<IResult> Action(EmployeeRequest employeeRequest, UserManager<IdentityUser> userManager ){
    var user = new IdentityUser {
      UserName = employeeRequest.Email,
      Email = employeeRequest.Email
    };

    var result = await userManager.CreateAsync(user, employeeRequest.Password);

    if (!result.Succeeded)
      return Results.ValidationProblem(result.Errors.ConvertToProblemDetails());

    var userClaims = new List<Claim> {
      new Claim("EmployeeCode", employeeRequest.EmployeeCode),
      new Claim("Name", employeeRequest.Name)
    };

    var claimResult = await userManager.AddClaimsAsync(user, 
                                                       userClaims);
    if (!claimResult.Succeeded) {
      return Results.BadRequest(claimResult.Errors.First());
    }
      
    return Results.Created($"/employees/{user.Id}", user.Id);
  
  }
}