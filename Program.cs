using Codefast.Context;
using Codefast.Repository;
using Codefast.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://codefast-uninassau.netlify.app", "http://192.168.0.17", "http://localhost:3000")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});

//builder.Services. 
//    AddDbContext<CodefastContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ServerConnection")));

builder.Services.AddDbContext<CodefastContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IBaseRepository, BaseRepository>();
builder.Services.AddScoped<IEquipeRepository, EquipeRepository>();
builder.Services.AddScoped<ITorneioRepository, TorneioRepository>();
builder.Services.AddScoped<IControleEliminatoriaRepository, ControleEliminatoriaRepository>();
builder.Services.AddScoped<IControleMataMataRepository, ControleMataMataRepository>();
builder.Services.AddScoped<IRodadaRepository, RodadaRepository>();
builder.Services.AddScoped<ISementeRodadaRepository, SementeRodadaRepository>();


var app = builder.Build();
app.UseCors(MyAllowSpecificOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
