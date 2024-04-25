using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VShop.DiscountApi.Context;
using VShop.DiscountApi.DTOs.Mappings;
using VShop.DiscountApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DiscountApiDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});


var mappingConfig = new MapperConfiguration(config =>
{
    config.AddProfile(new MappingProfile());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped<ICouponRepository, CouponRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod()
                                                      .AllowAnyOrigin()
                                                      .AllowAnyOrigin());
});

builder.Services.AddAuthentication()

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
