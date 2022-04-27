using IdentityModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PictureStore.Common;
using PictureStore.Common.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
var (keyCloakConfig, pictureStoreConfig) = ConfigureConfiguration(builder);

ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();

void ConfigureServices(WebApplicationBuilder builder)
{
    // register the DbContext on the container, getting the connection string from
    // appSettings (note: use this during development; in a production environment,
    // it's better to store the connection string in an environment variable)
    builder.Services.AddDbContext<PictureStoreContext>(options =>
    {
        options.UseSqlServer(
            builder.Configuration["ConnectionStrings:PictureStore"]);
    });

    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
           {
               options.AccessDeniedPath = "/Authorization/AccessDenied";
           })
           .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
           {
               options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
               options.Authority = keyCloakConfig.Authority;
               options.ClientId = keyCloakConfig.ClientId;
               options.ClientSecret = keyCloakConfig.ClientSecret;
               options.RequireHttpsMetadata = false;
               options.ResponseType = "code";

               options.SaveTokens = true;
               options.GetClaimsFromUserInfoEndpoint = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   NameClaimType = JwtClaimTypes.GivenName,
                   RoleClaimType = JwtClaimTypes.Role
               };
           });
}

(KeyCloakConfiguration keycloak, PictureStoreConfiguration pictureStore) ConfigureConfiguration(WebApplicationBuilder builder)
{
    builder.Services
        .Configure<PictureStoreConfiguration>(
            options => builder.Configuration.GetSection("PictureStore").Bind(options));
    var pictureStoreConfig = new PictureStoreConfiguration();
    builder.Configuration.Bind("PictureStore", pictureStoreConfig);

    builder.Services.Configure<KeyCloakConfiguration>(options => builder.Configuration.GetSection("KeyCloak").Bind(options));
    var keyCloakConfig = new KeyCloakConfiguration();
    builder.Configuration.Bind("KeyCloak", keyCloakConfig);

    return (keyCloakConfig, pictureStoreConfig);
}