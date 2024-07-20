using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Notebook.EntityFramework;
using Notebook.EntityFramework.Repositories;
using Notebook.Handler.Authentication.SignIn;
using Notebook.Services.CryptingServices;
using Notebook.Services.DocumentServices;
using Notebook.Services.EmailServices;
using Notebook.Services.Jwt;
using Notebook.Services.UserServices;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddAuthentication(options =>
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
});

services.AddMediatR(options =>
{
    options.RegisterServicesFromAssembly(typeof(SignInRequest).Assembly);
});

services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(@"./Keys"))
    .SetApplicationName("Notebook");

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

/*services.AddDistributedSqlServerCache(config =>
{
    config.ConnectionString = connectionString;
    config.SchemaName = "dbo";
    config.TableName = "Sessions";
});

services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});*/

services.AddDbContext<ApplicationDbContext>(options =>  options.UseSqlServer(connectionString));

services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));
services.Configure<SmtpOptions>(builder.Configuration.GetSection("SmtpOptions"));

services.AddHttpContextAccessor();
services.AddScoped<IRepositoryFactory, RepositoryFactory>();
services.AddTransient<IJwtService, JwtService>();
services.AddTransient<DocumentService>();
services.AddScoped<ICurrentUserContext, CurrentUserContext>();
services.AddSingleton<ICryptingManager, CryptingManager>();
services.AddTransient<IEmailService, SmtpEmailService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHsts();

app.UseAuthentication();
app.UseAuthorization();
/*app.UseSession();*/

app.MapControllers();

app.Run();