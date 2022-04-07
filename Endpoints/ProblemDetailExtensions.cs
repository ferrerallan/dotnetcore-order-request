using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;

namespace OrderRequest.Endpoints;
public static class ProblemDetailExtensions {
  public static Dictionary<string, string[]> ConvertToProblemDetails(this IReadOnlyCollection<Notification> notifications) {
    return notifications
          .GroupBy(global=>global.Key)
          .ToDictionary(global=>global.Key, g=> g.Select(x => x.Message).ToArray());
  }

  public static Dictionary<string, string[]> ConvertToProblemDetails(this IEnumerable<IdentityError> errors) {
    return errors
          .GroupBy(global=>global.Code)
          .ToDictionary(global=>global.Key, g=> g.Select(x => x.Description).ToArray());
  }

}