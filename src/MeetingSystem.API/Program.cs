using MeetingSystem.API.Extensions;
using MeetingSystem.API.Middlewares;
using MeetingSystem.Application;
using MeetingSystem.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Configuration.AddEnvironmentVariables();

builder
         .Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters =
                 new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["JWT_ISSUER"],
                     ValidAudience = builder.Configuration["JWT_AUDIENCE"],
                     IssuerSigningKey = new SymmetricSecurityKey(
                         Encoding.UTF8.GetBytes(builder.Configuration["JWT_SECRET_KEY"])
                     ),
                 };
         });

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT token into field",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(document =>
    {
        return new OpenApiSecurityRequirement
        {
            {

                new OpenApiSecuritySchemeReference("Bearer", document),
                new List<string>()
            }
        };
    });

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.IgnoreObsoleteActions();
    options.IgnoreObsoleteProperties();
    options.CustomSchemaIds(type => type.FullName);
});


builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("Blazor", policy =>
    policy.WithOrigins("http://localhost:3005", "http://localhost:5256", "https://localhost:7000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

var app = builder.Build();

await app.InitializeDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("Blazor");

app.UseAuthentication();

app.UseAuthorization();
app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
