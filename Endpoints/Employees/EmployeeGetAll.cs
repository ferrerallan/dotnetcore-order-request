using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using OrderRequest.Domain.Products;
using OrderRequest.Infra.Data;

namespace OrderRequest.Endpoints.Employees;
 public class EmployeeGetAll {
  public static string Template => "/employees";
  public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
  public static Delegate Handle => Action;

  [Authorize(Policy = "EmployeePolicy")]
  public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWithClaimName query) {
    
    return Results.Ok(await query.Execute(page.Value,rows.Value));

    /*****with Identity ****
    var users = userManager.Users.Skip((page - 1 ) * rows).Take(rows).ToList();
    var employees = new List<EmployeeResponse>();
    foreach (var item in users) {
      var claims = userManager.GetClaimsAsync(item).Result;
      var claimName = claims.FirstOrDefault(c=> c.Type =="Name");
      var userName = claimName != null ? claimName.Value : string.Empty;
      employees.Add( new EmployeeResponse(item.Email, userName));
    }

    var employess = users.Select(u=>new EmployeeResponse(u.Email, u.UserName));
    return Results.Ok(employess);
    */
  }
}