using ToolShopAPI.Models;
using ToolShopAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.Configure<ToolShopDatabaseSettings>(
    builder.Configuration.GetSection("ToolShopDatabase"));

builder.Services.AddSingleton<ToolsService>();
builder.Services.AddSingleton<PersonnelService>();
builder.Services.AddSingleton<BrandService>();

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToolShopAPI", Version = "v1" });
});

var app = builder.Build();

// Enable Swagger only in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToolShopAPI v1");
    });
}

// Redirect HTTP to HTTPS in production
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");
app.UseAuthorization();

app.MapControllers();

app.Run();
