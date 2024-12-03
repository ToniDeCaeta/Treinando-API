namespace ApiEstudante;

public class Estudante
{
   public Guid Id {get; init;} //Guid gera o Id unico e init o inicia. 
   public string Nome {get; private set;}
   public bool Ativo {get; private set;}


   public Estudante (string nome)
   {
    Nome = nome;
    Id = Guid.NewGuid(); // função .net que gera novo ID
    Ativo = true;  // sempre gera um estudante ativo
   }
    
}
