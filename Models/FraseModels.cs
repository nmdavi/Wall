using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Wall.Models
{
    public class FraseViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Titulo obrigatório")]
        [Column(TypeName = "varchar")]
        [StringLength(maximumLength: 30, MinimumLength = 2, ErrorMessage = "Titulo precisa estar entre 2 e 30 caractéres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Frase obrigatória")]
        [Column(TypeName = "varchar")]
        [StringLength(maximumLength: 150, MinimumLength = 10, ErrorMessage = "Frase precisa estar entre 10 e 100 caractéres")]
        public string Frase { get; set; }

        [ScaffoldColumn(false)]
        public string Autor { get; set; }

        [ScaffoldColumn(false)]
        [Display(Name = "Data da Criação")]
        public DateTime DataDaCriacao { get; set; } = DateTime.Now;
    }
}