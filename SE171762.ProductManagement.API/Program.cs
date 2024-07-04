using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NET171462.ProductManagement.Repo.Models;
using SE171462.ProductManagement.Repo.JwtService.Options;
using SE171762.ProductManagement.API.DependencyInjection;
using SE171462.ProductManagement.Repo.JwtService.Interface;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
{
    Name = "Authorization",
    Scheme = "Bearer",
    In = ParameterLocation.Header,
    Type = SecuritySchemeType.ApiKey,
    BearerFormat = "JWT"
});

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = JwtBearerDefaults.AuthenticationScheme
                        },
                    },
                    new List<string>{}
                }
            });
    });

builder.Services.AddConfigDbContext(builder.Configuration);
builder.Services.AddConfigRepository();
builder.Services.AddConfigurationMapper();

builder.Services.AddDbContext<MyStoreContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStrings"));
});


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(x =>
    {
        JwtOption jwtOption = new JwtOption();
        builder.Configuration.GetSection(nameof(JwtOption)).Bind(jwtOption);

        x.SaveToken = true;

        var key = Encoding.UTF8.GetBytes(jwtOption.SecretKey);
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,


            ValidIssuer = jwtOption.Issuer,
            ValidAudience = jwtOption.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
    });
}

app.UseStaticFiles();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
