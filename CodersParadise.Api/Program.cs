using CodersParadise.Api;
using CodersParadise.Api.Extensions;
using CodersParadise.Core;
using CodersParadise.DataAccess;
using CodersParadise.DataAccess.JwtService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureApiDependencies();
builder.Services.AddCoreServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add services to the container
builder.Services.AddDataAccessServices(builder.Configuration);

// JWT Configurations
JwtConfiguration jwtConfiguration = new JwtConfiguration();
builder.Configuration.Bind("JwtConfig", jwtConfiguration);
builder.Services.AddSingleton(jwtConfiguration);
builder.Services.AddTokenAuthentication(jwtConfiguration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
