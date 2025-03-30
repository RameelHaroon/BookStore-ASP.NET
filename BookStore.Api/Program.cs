using BookStore.Api.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionStr = builder.Configuration.GetConnectionString("BookStore");
builder.Services.AddSqlite<BookStoreDbContext>(connectionStr);
var app = builder.Build();
await app.MigrateDbAsync();
app.MapGet("/", () => "Hello World!");

app.Run();
