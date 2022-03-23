using Flunt.Validations;

namespace OrderRequest.Domain.Products;

public class Category : Entity
{
  public Category(string name, string email, string createdBy, string editedBy)
  {
    var contract = new Contract<Category>()
      .IsNotNull(name,"Name")
      .IsGreaterOrEqualsThan(name,3,"Name")
      .IsEmail(email,"Email")
      .IsNotNullOrEmpty(createdBy,"CreatedBy")
      .IsNotNullOrEmpty(editedBy,"EditedBy");
    AddNotifications(contract);

    Name=name;
    Email=email;
    Active=true;
    EditedBy=editedBy;
    CreatedBy=createdBy;
    CreatedOn=DateTime.Now;
    EditedOn=DateTime.Now;
  }

  public string Name { get; set; }
  public bool Active { get; set; }
  public string Email { get; set; }

}
