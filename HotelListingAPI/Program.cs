using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using HotelListingAPI.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HotelListingAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelListingAPIContext") ?? throw new InvalidOperationException("Connection string 'HotelListingAPIContext' not found.")));

// Add services to the container.
var cs = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
builder.Services.AddDbContext<HotelListingDbContext>(opt =>
{
    opt.UseSqlServer(cs, sqlServer
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt => {
    opt.AddPolicy("AllowAll", 
    b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

builder.Host.UseSerilog(
    (ctx, lc) => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseSerilogRequestLogging();
app.UseSwagger();
app.UseSwaggerUI();
    

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
