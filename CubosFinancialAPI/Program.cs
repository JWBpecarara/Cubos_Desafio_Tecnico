using CubosFinancialApi.Infrastructure.Intregrations.Clientes;
using CubosFinancialAPI.Infrastructure;
using CubosFinancialAPI.Infrastructure.Intregrations.Services;
using CubosFinancialAPI.Infrastructure.Repository.Interface;
using CubosFinancialAPI.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Refit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using CubosFinancialAPI.Infrastructure.Extensions;
using System.Text.Json.Serialization;
using System.Text.Json;
using CubosFinancialAPI.Application.Services.Interfaces;
using CubosFinancialAPI.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const string COMPLICEAPI_URL = "https://compliance-api.cubos.io";

builder.Services.AddRefitClient<IComplianceApi>().ConfigureHttpClient(c => c.BaseAddress = new Uri(COMPLICEAPI_URL));


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
        );
    });



builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<IPeopleRepository, PeopleRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
builder.Services.AddScoped<ICriptografiaHelper, CriptografiaHelper>();
builder.Services.AddSingleton<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<ComplianceService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddDbContext<ConnectionContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]!)),
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
