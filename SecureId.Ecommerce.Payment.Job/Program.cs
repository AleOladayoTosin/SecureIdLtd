using Hangfire;
using Hangfire.SQLite;
using Microsoft.EntityFrameworkCore;
using SecureId.Ecommerce.Payment.Job.Workers;
using SecureId.Ecommerce.ShoppingCart.Persistence;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(opt =>
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddHangfire(configuration => configuration.UseSqlServerStorage(connectionString));


builder.Services.AddScoped<PaymentProcessor>();

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHangfireDashboard("/hangfire");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;

var payment = services.GetRequiredService<PaymentProcessor>();

RecurringJob.AddOrUpdate("jobId", () => payment.ExecutePayment(), builder.Configuration["ExecutePayment"]);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
