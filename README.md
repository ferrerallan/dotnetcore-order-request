
# .NET Core Order Request Example

## Description

This repository provides an example of an Order Request application built with .NET Core. It demonstrates how to set up and use .NET Core to create a web API for managing order requests, which is useful for developers looking to build robust and scalable backend services.

## Requirements

- .NET Core SDK
- Visual Studio or any preferred IDE
- Docker (optional, for containerization)

## Mode of Use

1. Clone the repository:
   ```bash
   git clone https://github.com/ferrerallan/dotnetcore-order-request.git
   ```
2. Navigate to the project directory:
   ```bash
   cd dotnetcore-order-request
   ```
3. Build the project:
   ```bash
   dotnet build
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

## Implementation Details

- **Controllers/**: Contains the API controllers for handling order requests.
- **Models/**: Contains the model classes representing the data structures.
- **Services/**: Contains the business logic for processing orders.
- **appsettings.json**: Configuration file for application settings.
- **Dockerfile**: Configuration file for containerizing the application (if using Docker).

### Example of Use

Here is an example of a simple API controller in .NET Core for handling order requests:

```csharp
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace OrderRequestExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private static readonly List<string> Orders = new List<string>();

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(Orders);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] string order)
        {
            Orders.Add(order);
            return Ok();
        }
    }
}
```

This code defines an API controller with endpoints to get and create orders, demonstrating basic CRUD operations.

## License

This project is licensed under the MIT License.

You can access the repository [here](https://github.com/ferrerallan/dotnetcore-order-request).
