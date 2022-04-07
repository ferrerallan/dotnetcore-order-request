using Dapper;
using Microsoft.Data.SqlClient;
using OrderRequest.Endpoints.Employees;

namespace OrderRequest.Infra.Data;

public class QueryAllUsersWithClaimName {
  private readonly IConfiguration configuration;

  public QueryAllUsersWithClaimName(IConfiguration configuration) {
    this.configuration = configuration;
  }  

  public async Task<IEnumerable<EmployeeResponse>> Execute(int? page, int? rows) {
    var db = new SqlConnection(configuration["ConnectionString:OrderRequestDb"]);
    
    var query= 
      @"select usu.Email , claims.ClaimValue as Name
          from AspNetUsers usu
        inner join AspNetUserClaims claims
          on (usu.Id = claims.UserId and claims.ClaimType = 'Name')
        order by Name
        OFFSET(@page-1)*@rows ROWS FETCH NEXT @rows ROWS ONLY";
     
    var employess = await db.QueryAsync<EmployeeResponse>(
      query,
      new { page, rows}
    );

    return employess;
  }

}