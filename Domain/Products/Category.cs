using Flunt.Validations;

namespace OrderRequest.Domain.Products;

public class Category : Entity
{
  public Category(string name, string email, string createdBy, string editedBy)
  {
    Name=name;
    Email=email;
    Active=true;
    EditedBy=editedBy;
    CreatedBy=createdBy;
    CreatedOn=DateTime.Now;
    EditedOn=DateTime.Now;

    Validate();
  }

  private void Validate(){
    var contract = new Contract<Category>()
      .IsNotNull(Name,"Name")
      .IsGreaterOrEqualsThan(Name,3,"Name")
      .IsEmail(Email,"Email")
      .IsNotNullOrEmpty(CreatedBy,"CreatedBy")
      .IsNotNullOrEmpty(EditedBy,"EditedBy");
    AddNotifications(contract);
  }

  public string Name { get; private set; }
  public bool Active { get; set; }
  public string Email { get; set; }

  public void EditInfo(string name, bool active) {
    Active = active;
    Name = name;

    Validate();

  }

}
