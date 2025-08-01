using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Musistance.AuthCallbacks;
using Musistance.Data;
using Musistance.Services.Implementations;
using Musistance.Services.Interfaces;
using Musistance.Settings;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddData().AddItch(builder.Configuration);

JwtSettings authSettings = builder.Configuration.GetSection("JwtAuth").Get<JwtSettings>();
builder.Services.AddSingleton<JwtSettings>(authSettings);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
        {
            ValidateIssuer = !String.IsNullOrEmpty(authSettings.Issuer),
            ValidIssuer = authSettings.Issuer,
            ValidateAudience = !String.IsNullOrEmpty(authSettings.Audience),
            ValidAudience = authSettings.Audience,
            ValidateLifetime = authSettings.LifetimeInHours.HasValue,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authSettings.Signature))
        };
    });

builder.Services.AddTransient<IAuthService,AuthService>();

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
