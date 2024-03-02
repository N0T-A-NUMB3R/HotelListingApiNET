using HotelListing.API.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.Extensions.DependencyInjection;
using HotelListingAPI.Data;
using HotelListingAPI.Configurations;
using HotelListingAPI.Contracts;
using HotelListingAPI.Repository;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<HotelListingDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelListingAPIContext") ?? throw new InvalidOperationException("Connection string 'HotelListingAPIContext' not found.")));

// Add services to the container.
var cs = builder.Configuration.GetConnectionString("HotelListingDbConnectionString");
builder.Services.AddDbContext<HotelListingDbContext>(opt =>
{
    opt.UseSqlServer(cs,
        sqlServerOptionsAction: sqlOpt =>
        {
            sqlOpt.EnableRetryOnFailure(
                maxRetryCount: 10,
                maxRetryDelay: TimeSpan.FromSeconds(5),
                errorNumbersToAdd: null);
        });
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

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ICountriesRepository, CountriesRepository>();

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
