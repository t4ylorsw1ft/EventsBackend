using Events.Application.Common.Mappings;
using Events.Application.Interfaces;
using Events.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Events.Application;
using Events.WebApi.Middleware;
using Events.Persistence.Auth;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Builder;


var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);
var app = builder.Build();


app.MapGet("/", () => "Hello World!");



void ConfigureServices(IServiceCollection services)
{
    services.AddAutoMapper(config =>
    {
        config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
        config.AddProfile(new AssemblyMappingProfile(typeof(IEventsDbContext).Assembly));
    });

    services.AddApplication();
    services.AddPersistence(builder.Configuration);
    services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
    services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    });
    var jwtOptions = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();
    services.AddApiAuthentication(Options.Create(jwtOptions));

    services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy =>
        {
            policy.RequireClaim("Admin", "True");
        });
    });
    services.AddSwaggerGen(config =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        config.IncludeXmlComments(xmlPath);
    });
}


app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;
    config.SwaggerEndpoint("swagger/v1/swagger.json", "Events API");
});
app.UseRouting();
app.UseStaticFiles();
app.UseCustomExceptionHandler();
app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseCors("AllowAll");
app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<EventsDbContext>();
        DbInitializer.Initialize(context);
    }
    catch(Exception ex)
    {
        Console.WriteLine(":(");
    }
}
app.Run();
