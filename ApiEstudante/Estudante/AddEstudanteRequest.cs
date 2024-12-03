public record AddEstudanteRequest(string nome); // é como uma classe generica, para transferir
//responsabilidade, onde o nome passa por aqui, e nao direto pela classe Estudante. 

/*Na rota, ao instanciarmos estudante, devemos por um nome como parametro, e passaremos
ao invés da string nome solicitada no contrutor de Estudante, Este record.nome*/