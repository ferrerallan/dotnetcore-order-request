using OrderRequest.Domain.Products;
using OrderRequest.Infra.Data;

namespace OrderRequest.Endpoints.Categories;
 public class CategoryGetAll {
  public static string Template => "/categories";
  public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
  public static Delegate Handle => Action;

  public static IResult Action(ApplicationDbContext context ){    

    var categories = context.Categories.ToList();
    //Select is similar to Map(javascript)
    var response = categories.Select(c=> new CategoryResponse{
        Name = c.Name, 
        Active=c.Active,
        Id = c.Id
      });
    try {
      return Results.Ok( response);
    }
    catch (Exception exc){
      return Results.Problem(exc.Message);
    }
  }
}