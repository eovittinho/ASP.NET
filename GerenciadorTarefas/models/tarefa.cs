using System.ComponentModel.DataAnnotations;

namespace GerenciadorTarefas.Models
{
    public class Tarefa
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O título é obrigatório.")]
        [StringLength(100, ErrorMessage = "O título deve ter no máximo 100 caracteres.")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres.")]
        public string Descricao { get; set; } = string.Empty;

        public bool Concluida { get; set; } = false;
    }
}
