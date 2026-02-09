using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Scalar.AspNetCore;
using TaskDispatcherApi.Data;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using TaskDispatcherApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(Options => Options.UseSqlite("Data Source=tasks.db"));

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter(policyName: "fixed", options =>
    {
        options.PermitLimit = 10; //max requests
        options.Window = TimeSpan.FromSeconds(10); //10 second window
        options.QueueLimit = 2; //max queued requests
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst; //process oldest requests first
    });
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("The Dispatcher API");
    });
}


app.MapHub<TaskHub>("/taskhub");

//app.UseHttpsRedirection();
app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
