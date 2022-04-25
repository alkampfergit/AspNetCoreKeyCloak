using PictureStore.Common.Entities;
using Microsoft.EntityFrameworkCore;
using PictureStore.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
ConfigureConfiguration(builder);

ConfigureServices(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

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
}

void ConfigureConfiguration(WebApplicationBuilder builder)
{
    builder.Services
        .Configure<PictureStoreConfiguration>(
            options => builder.Configuration.GetSection("PictureStore").Bind(options));
}