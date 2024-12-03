using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace ApiEstudante;

public static class EstudanteRotas
{
    public static void AddRotasEstudantes(this WebApplication app) //app é do tipo WebAplication
    {
        app.MapPost(pattern: "estudantes", handler: async (AddEstudanteRequest request, AppDbContext context)=>
        {
            var jaExiste = await context.estudante.AnyAsync(estudante => estudante.Nome == request.nome);
            if (jaExiste)
            return Results.Conflict("Já existe esse Aluno");

            var novoEstudante = new Estudante (request.nome);//nome pelo record AddEstudanteRequest

            await context.estudante.AddAsync(novoEstudante); //ef funciona como lista, e salva na tabela
        
            await context.SaveChangesAsync(); //o Ef salva todas as alterações feitas.

            return Results.Ok(novoEstudante);
        
        });
    }
}
