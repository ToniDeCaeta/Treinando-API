using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

namespace ApiEstudante;

public static class EstudanteRotas
{
    public static void AddRotasEstudantes(this WebApplication app) //app é do tipo WebAplication
    {
        app.MapPost(pattern: "estudantes", handler: async (AddEstudanteRequest request, AppDbContext context, CancellationToken ct)=>
        {
            var jaExiste = await context.Estudante.AnyAsync(estudante => estudante.Nome == request.nome, ct);
            if (jaExiste)
            return Results.Conflict("Já existe esse Aluno");

            var novoEstudante = new Estudante (request.nome);//nome pelo record AddEstudanteRequest

            await context.Estudante.AddAsync(novoEstudante, ct); //ef funciona como lista, e salva na tabela
        
            await context.SaveChangesAsync(ct); //o Ef salva todas as alterações feitas.


            //Ao retornar, desejo retornar somente os DTOs, por medidas de segurança

            var retornoEstudante = new EstudanteDto(novoEstudante.Id, novoEstudante.Nome);
            return Results.Ok(retornoEstudante);
        
        });
    

        app.MapGet (pattern: "estrudantes", handler: async (AppDbContext context, CancellationToken ct) =>
        {
            var estudantes = await context.Estudante
            .Where(estudante => estudante.Ativo == true)
            .Select(estudante => new EstudanteDto(estudante.Id, estudante.Nome))
            .ToListAsync(ct);
            return estudantes;
        });
    
        
        app.MapPut (pattern: "/estudantes/{id:guid}", handler: async (Guid id, UpdateEstudanteRequest request, AppDbContext context, CancellationToken ct)=>
        {
            var estudante = await context.Estudante.SingleOrDefaultAsync(estudante => estudante.Id == id, ct);

            if (estudante == null)
            return Results.NotFound();



            estudante.AtualizarNome(request.nome);
            await context.SaveChangesAsync(ct);
            return Results.Ok(new EstudanteDto (estudante.Id, estudante.Nome));
        });


        app.MapDelete(pattern: "/estudantes/{id:Guid}", handler: async (Guid id, AppDbContext context, CancellationToken ct)=>
        {
            var estudante = await context.Estudante.SingleOrDefaultAsync(estudante=> estudante.Id == id, ct);
            if (estudante == null)
            return Results.NotFound();

            estudante.Desativar(); //essa variavel se tornou do tipo estudante
            await context.SaveChangesAsync(ct);
            return Results.Ok($"O item foi deletado");
        });



        
    
    }
}
