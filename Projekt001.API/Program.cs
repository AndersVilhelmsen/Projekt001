using Projekt001.Repo;
using Projekt001.Repo.Interfaces;
using Projekt001.Repo.Models_DTO_;
using Projekt001.Repo.Repositories;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<DatabaseContext>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("personCors",
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<ICarRepository, CarRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("personCors");

app.UseAuthorization();

app.MapControllers();

app.Run();
