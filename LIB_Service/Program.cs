using LIB.API.Persistence;
using LIB.API.Infrastructure;
using LIB.API.Application;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceService(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Retrieve JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

// Generate a 16-byte (128-bit) random key
var key = new byte[16];
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(key);
}

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ClockSkew = TimeSpan.Zero
        };
    });

// Add support for serving static files
builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "wwwroot";
});

// Configure CORS with specific options
builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCorsPolicy", builder =>
    {
        builder.WithOrigins("http://10.1.22.206:4060", "http://10.1.10.106:7070", "http://10.1.22.206:4200", "http://10.1.22.25:4200") // Allow multiple origins
               .WithMethods("GET", "POST", "PUT", "DELETE") // Allow specific methods
               .AllowCredentials() // Allow credentials (cookies, authorization headers, etc.)
               .AllowAnyHeader(); // Allow any header
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseSpaStaticFiles();

app.UseRouting();

// Use the defined CORS policy
app.UseCors("MyCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSpa(spa =>
{
    spa.Options.SourcePath = "wwwroot";
    if (app.Environment.IsDevelopment())
    {
        spa.UseProxyToSpaDevelopmentServer("http://10.1.22.206:4200");
    }
});

app.Run();
