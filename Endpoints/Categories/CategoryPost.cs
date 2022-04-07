using OrderRequest.Domain.Products;
using OrderRequest.Infra.Data;
using OrderRequest.Endpoints;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace OrderRequest.Endpoints.Categories;
 public class CategoryPost {
  public static string Template => "/categories";
  public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
  public static Delegate Handle => Action;

  [Authorize]
  public static IResult Action(CategoryRequest categoryRequest, HttpContext http, ApplicationDbContext context ) {
    var userId = http.User.Claims.First(c=>c.Type == ClaimTypes.NameIdentifier).Value;
    var category = new Category(categoryRequest.Name,
                                categoryRequest.Email,
                                userId,
                                userId);
    
    if (!category.IsValid) {
      var errors = category.Notifications.ConvertToProblemDetails();
      return Results.ValidationProblem(errors);
    }      

    context.Categories.Add(category);
    context.SaveChanges();

    return Results.Created($"/categories/{category.Id}", category.Id);
  
  }
}