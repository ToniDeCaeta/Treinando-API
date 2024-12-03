using ApiEstudante;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AppDbContext>(); // Assim, com a injeção de dependencia, o program cs, pode usar nossa AppDbContext.
//Com o ultimo builder Service, por fim o EF está conectado. 


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// Aqui ficarão as rotas

app.AddRotasEstudantes();// app é um obj statico de EstudanteRotas, chamando o método da rota.  

app.Run();


