using SecureId.AccessControl.Persistence;
using Microsoft.EntityFrameworkCore;
using SecureId.AccessControl.API.Extensions;
using Microsoft.AspNetCore.Identity;
using SecureId.AccessControl.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using SecureId.AccessControl.Application.Interfaces;
using SecureId.AccessControl.Infrastructure.Security;
using MediatR;
using SecureId.AccessControl.API.IntegrationEvent.Account;
using SecureId.AccessControl.API.Services;
using AutoMapper;
using SecureId.AccessControl.API.AutoMapper;
using AspNetCoreRateLimit;
using SecureId.AccessControl.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(opt =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    opt.Filters.Add(new AuthorizeFilter(policy));
});

IMapper mapper = MapperConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerService();

builder.Services.AddRateLimitService(builder.Configuration);

builder.Services.AddDbContext<DataContext>(opt => {
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentityService(builder.Configuration);

builder.Services.AddHttpContextAccessor();


builder.Services.AddScoped<TokenService>();

builder.Services.AddScoped<IUserAccessor, UserAccessor>();

builder.Services.AddMediatR(typeof(AuthenticateEventHandler.Handler));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

app.UseIpRateLimiting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    await context.Database.MigrateAsync();
    await Seed.SeedData(context, userManager, roleManager);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

app.Run();
