using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notebook.Api.MiddleWares;
using Notebook.EntityFramework;
using Notebook.EntityFramework.GenericRepository;
using Notebook.Handler.Authentication.SignIn;
using Notebook.Services.AuthenticationServices;
using Notebook.Services.CacheService;
using Notebook.Services.CryptingServices;
using Notebook.Services.EmailServices;
using Notebook.Services.Flows;
using Notebook.Services.Jwt;
using Notebook.Services.UserServices;
using StackExchange.Redis;
using System.Text;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
IServiceCollection services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

/*services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    string key = builder.Configuration.GetSection("JwtOptions:Key").Value ??
        throw new InvalidOperationException("Credentials not found.");

    byte[] byteKey = Encoding.UTF8.GetBytes(key);
    SecurityKey securityKey = new SymmetricSecurityKey(byteKey);

    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = securityKey,
    };
});*/

services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy
        .WithOrigins("http://localhost:3000", "https://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials());
});

services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(SignInRequest).Assembly);
});

services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"./Keys"))
    .SetApplicationName("Notebook");

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

services.AddStackExchangeRedisCache(options => options.Configuration = "localhost");

services.AddDistributedSqlServerCache(config =>
{
    config.ConnectionString = connectionString;
    config.SchemaName = "dbo";
    config.TableName = "Sessions";
});

services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(15);
    options.IOTimeout = TimeSpan.FromHours(6);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.Cookie.SameSite = SameSiteMode.None;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

services.AddDbContext<ApplicationDbContext>(options =>  options.UseSqlServer(connectionString));
services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpOptions"));

services.AddSingleton<ICryptingManager, Protector>();
services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
services.AddScoped<ICurrentUserContext, CurrentUserContext>();
services.AddScoped<ISessionManager, SessionManager>();
services.AddScoped<ICacheManager, RedisCacheManager>();
services.AddScoped<UserAccessFlow>();
services.AddScoped<UrlBuilder>();
services.AddScoped<EmailMessageManager>();
services.AddTransient<IJwtService, JwtService>();
services.AddTransient<IEmailService, SmtpEmailService>();
services.AddHttpContextAccessor();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();
app.UseHsts();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();