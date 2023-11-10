using System.Reflection;
using API.Extension;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(builder.Configuration)
                    .Enrich.FromLogContext()
                    .CreateLogger();

// Add services to the container
builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers(options => 
{
    options.RespectBrowserAcceptHeader = true;
    options.ReturnHttpNotAcceptable = true;
}).AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First()); });
builder.Services.AddAplicacionServices();
builder.Services.ConfigureRateLimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());
builder.Services.ConfigureCors();
builder.Services.AddJwt(builder.Configuration);
builder.Services.AddDbContext<UniversityContext>(options => 
{
    string connectionString = builder.Configuration.GetConnectionString("ConexMysql");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseIpRateLimiting();
app.UseAuthorization();

app.MapControllers();

app.Run();
