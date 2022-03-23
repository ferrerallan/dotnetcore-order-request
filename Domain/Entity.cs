using Flunt.Notifications;

namespace OrderRequest.Domain;

public abstract class Entity: Notifiable<Notification>
{
  public Guid Id { get; set; }
  public string? CreatedBy { get; set; } 
  public DateTime CreatedOn { get; set; }= DateTime.Now;
  public string? EditedBy { get; set; }
  public DateTime EditedOn { get; set; }
}