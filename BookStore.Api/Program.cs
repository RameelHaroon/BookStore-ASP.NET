using BookStore.Api;
using BookStore.Api.Data;
using BookStore.Api.Endpoints;
using BookStore.Api.Entity;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

var connectionStr = builder.Configuration.GetConnectionString("BookStore");
builder.Services.AddSqlite<BookStoreDbContext>(connectionStr);

builder.Logging.AddJsonConsole(options =>
{
    options.JsonWriterOptions = new()
    {
        Indented = true
    };
});

var app = builder.Build();

app.MapBookEndpoints();
app.MapGenreEndpoints();
app.MapAuthorEndpoints();

app.UseMiddleware<GlobalExceptionMiddleware>();
await app.MigrateDbAsync();
app.Run();
