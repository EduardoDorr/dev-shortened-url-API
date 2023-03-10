using Microsoft.EntityFrameworkCore;
using DevEncurtaUrl.API.Persistence;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
  options.AddDefaultPolicy(
      policy =>
      {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
      }
  );
});
var connectionString = builder.Configuration.GetConnectionString("DevShortenedDb");
// builder.Services.AddDbContext<DevShortenedLinkDbContext>(o => o.UseSqlServer(connectionString));
builder.Services.AddDbContext<DevShortenedLinkDbContext>(o => o.UseInMemoryDatabase("DevShortenedUrl"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo
  {
    Title = "DevShortenedUrl.API",
    Version = "v1",
    Contact = new OpenApiContact
    {
      Name = "Eduardo Dörr",
      Email = "edudorr@hotmail.com",
      Url = new Uri("https://eduardodorr-portfolio.vercel.app/")
    }
  });

  var xmlFile = "DevEncurtaUrl.API.xml";
  var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
  c.IncludeXmlComments(xmlPath);
});

// builder.Host.ConfigureAppConfiguration((hostingContext, config) => {
//   Serilog.Log.Logger = new LoggerConfiguration()
//       .Enrich.FromLogContext()
//       .WriteTo.MSSqlServer(connectionString,
//                            sinkOptions: new MSSqlServerSinkOptions() {
//                            AutoCreateSqlTable = true,
//                            TableName = "Logs"
//       })
//       .WriteTo.Console()
//       .CreateLogger();
// }).UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true)
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
