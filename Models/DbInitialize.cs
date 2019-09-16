using System.Collections.Generic;
using System.Data.Entity;

namespace Wall.Models
{
    public class DbInitialize : CreateDatabaseIfNotExists<EFContext>
    {
        protected override void Seed(EFContext context)
        {
            var frases = new List<FraseViewModel>()
            {
                new FraseViewModel(){Titulo = "Seu último dia na terra", Frase = "No seu último dia na terra, a pessoa que você se tornou vai encontrar a pessoa que você poderia ter se tornado.", Autor = "Anônimo"},
                new FraseViewModel(){Titulo = "Ler, Recordar e Aprender", Frase = "O que se lê se esquece;O que se vê se recorda;E o que se faz se aprende.", Autor = "Anônimo"},
                new FraseViewModel(){Titulo = "Crescimento do espírito", Frase = "Nada assenta melhor ao corpo que o crescimento do espírito.", Autor = "Anônimo"}
            };

            context.Frases.AddRange(frases);
            context.SaveChanges();
        }
    }
}