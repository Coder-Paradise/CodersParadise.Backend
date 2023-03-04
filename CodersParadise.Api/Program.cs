using CodersParadise.Api;
using CodersParadise.Api.Extensions;
using CodersParadise.Core;
using CodersParadise.DataAccess;
using CodersParadise.DataAccess.JwtService;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//        options.AddDefaultPolicy(
//                   policy =>
//                      {
//                          policy.WithOrigins("https://localhost:3000").AllowAnyHeader()
//                                                  .AllowAnyMethod();
//                      });
//});

// Add services to the container.

builder.Services.AddControllers();
builder.Services.ConfigureApiDependencies();
builder.Services.AddCoreServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

//Add services to the container
builder.Services.AddDataAccessServices(builder.Configuration);

// JWT Configurations
JwtConfiguration jwtConfiguration = new JwtConfiguration();
builder.Configuration.Bind("JwtConfig", jwtConfiguration);
builder.Services.AddSingleton(jwtConfiguration);
builder.Services.AddTokenAuthentication(jwtConfiguration);

var app = builder.Build();
app.UseCors(options => options
    .WithOrigins("http://localhost:3000")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials() //Allow cookies to be sent to front-end
);

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
