using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();





// only for testing in swaagger 
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In  = ParameterLocation.Header,
        Description = "Enter 'Bearer {your token}'"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// cors policy for allowing frontend to send request on this server 

builder.Services.AddCors(Options => {Options.AddPolicy("AllowFrontend", policy => 
policy.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials());});




// Dependency Injection
builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();
builder.Services.AddSingleton<IMailService, EmailService>();

var app = builder.Build();

// Swagger UI in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware order matters
app.UseHttpsRedirection();

app.UseCors("AllowFrontend"); 

app.UseAuthorization(); 

app.MapControllers();

app.Run();
