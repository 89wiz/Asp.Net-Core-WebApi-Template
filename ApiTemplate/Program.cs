using ApiTemplate.Config.Auth;
using ApiTemplate.Config.IoC;
using ApiTemplate.Context;
using ApiTemplate.Services.Auth;
using ApiTemplate.Services.Files;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

services.AddOpenApi("v1", opt => opt.AddDocumentTransformer<BearerSecuritySchemeTransformer>());

services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .WithOrigins(configuration.GetSection("Cors:Origins").Get<string[]>()!)
            .AllowAnyMethod()
            .AllowAnyHeader();
    }));

services.Configure<JwtOptions>(configuration.GetSection("Jwt"));

services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        var options = configuration.GetSection("Jwt").Get<JwtOptions>()!;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
            ValidateIssuer = true,
            ValidIssuer = options.Issuer,
            ValidateAudience = true,
            ValidAudience = options.Audience,
            ValidateLifetime = false
        };
    });

services
    .AddAuthorizationBuilder()
    .SetFallbackPolicy(new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());

services.AddDbContext<ApiContext>(opt =>
{
    opt.UseInMemoryDatabase("ApiContext");
});

services.AddMediator();
services.AddSingleton<ICsvService, CsvService>();

var app = builder.Build();

app.UseCors("CorsPolicy");

app.MapEndpoints();

app.MapOpenApi().AllowAnonymous();
app.MapScalarApiReference(opt =>
{
    opt.AddHttpAuthentication("Bearer", opt => opt.WithToken("your-bearer-token"));
    opt.WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
}).AllowAnonymous();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.Run();
