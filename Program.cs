using FluentValidation;
using JWTAuthAPI.Extensions;
using JWTAuthAPI.Infrastructure.Data;
using JWTAuthAPI.Infrastructure.Repositories;
using JWTAuthAPI.Interfaces;
using JWTAuthAPI.Middlewares;
using JWTAuthAPI.Services;
using JWTAuthAPI.Validations;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddCorsPolicy();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("JWTAuthAPIDb"));

builder.Services.AddSwaggerCustom(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

builder.Services.AddValidatorsFromAssemblyContaining<AuthenticateRequestValidator>();

builder.Services.AddScoped<ITokenService,   JwtTokenService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddTransient<IRepositoryActivator, RepositoryActivator>();


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseRouting();
app.UseMyCorsPolicy();
app.UseJwtAuthorization();
app.MapControllers();
app.Run();
