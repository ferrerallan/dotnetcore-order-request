using OrderRequest.Endpoints.Categories;
using OrderRequest.Infra.Data;

var builder = WebApplication.CreateBuilder(args);
var conn =builder.Configuration["ConnectionString:OrderRequestDb"];
Console.WriteLine(conn);
builder.Services.AddSqlServer<ApplicationDbContext>(conn);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapMethods(CategoryPost.Template, CategoryPost.Methods, CategoryPost.Handle);
app.MapMethods(CategoryGetAll.Template, CategoryGetAll.Methods, CategoryGetAll.Handle);
app.MapMethods(CategoryPut.Template, CategoryPut.Methods, CategoryPut.Handle);

app.Run();
