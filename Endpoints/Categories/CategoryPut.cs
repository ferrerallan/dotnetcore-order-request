using Microsoft.AspNetCore.Mvc;
using OrderRequest.Domain.Products;
using OrderRequest.Infra.Data;

namespace OrderRequest.Endpoints.Categories;
 public class CategoryPut {
  public static string Template => "/categories/{id:guid}";
  public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
  public static Delegate Handle => Action;
  public static IResult Action([FromRoute] Guid id, 
                                CategoryRequest categoryRequest, 
                                ApplicationDbContext context ){
    var category = context.Categories.Where(c=>c.Id==id).FirstOrDefault();
    if (category == null)
      return Results.NotFound("category not found");
    category.Name = categoryRequest.Name;
    category.Active = categoryRequest.Active;

    context.SaveChanges();
    
    try {
      return Results.Ok("updated");
    }
    catch (Exception exc){
      return Results.Problem(exc.Message);
    }
  }
}