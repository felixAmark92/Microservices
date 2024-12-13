using Dataccess;
using Dataccess.Repositories.Feedbacks;
using FeedbackService.Services.Feedbacks;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IFeedbackService, FeedbackService.Services.Feedbacks.FeedbackService>();
builder.Services.AddScoped<FeedbackRepository>();

// Add DbContext with SQL Server or any other provider
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<FeedbackDbContext>(options =>
   options.UseSqlServer(connectionString));

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FeedbackDbContext>();
    dbContext.Database.Migrate();
}

app.MapControllers();

app.Run();