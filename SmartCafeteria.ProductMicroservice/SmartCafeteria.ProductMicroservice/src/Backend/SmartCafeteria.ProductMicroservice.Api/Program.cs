using Microsoft.EntityFrameworkCore;
using SmartCafeteria.ProductMicroservice.DataAccess;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddDbContext<ProductServiceDbContext>(options =>
//	options.UseSqlServer(Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProductServiceDbContext>(options =>
	options.UseSqlServer(connectionString));


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<ProductServiceDbContext>();
	dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseRouting();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
