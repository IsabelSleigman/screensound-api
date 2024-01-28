using ScreenSound.API.Endpoints;
using ScreenSound.Banco;
using ScreenSound.Modelos;
using ScreenSound.Service;
using System.Text.Json.Serialization;

var builder = WebApplication
    .CreateBuilder(args);

builder.Services.AddDbContext<ScreenSoundContext>();
builder.Services.AddTransient<ServiceBase<Artista>>();
builder.Services.AddTransient<ServiceBase<Musica>>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

app.AddEndPointArtistas();
app.AddEndPointMusicas();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
