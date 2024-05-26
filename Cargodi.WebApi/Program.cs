using System.Reflection;
using Cargodi.Business.Extensions.AutomapperProfiles;
using Cargodi.Business.Extensions;
using Cargodi.DataAccess.Data;
using Cargodi.WebApi;
using Microsoft.EntityFrameworkCore;
using Cargodi.WebApi.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("Default"),
        sqlBuilder =>
            sqlBuilder.MigrationsAssembly(
                Assembly.GetAssembly(typeof(DatabaseContext))?.GetName().Name)
            );
});

//builder.Configuration.ConfigureDatabase();

builder.Services.AddAutoMapper(Assembly.GetAssembly(typeof(IdentityProfile)));

builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CORS", policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddControllers();

builder.Services.AddIdentityService(builder.Configuration);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();

builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandlerMiddleware();
app.UseCors("CORS");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
