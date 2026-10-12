using CP5_.NET.Repositories;
using CP5_.NET.Services;
using CP5_.NET.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection(MongoDbSettings.SectionName)
);

// MUDANÇA AQUI: De Singleton para Scoped
builder.Services.AddScoped<JogoRepository>(); 
builder.Services.AddScoped<JogoService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();