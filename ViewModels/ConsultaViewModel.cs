using System.ComponentModel.DataAnnotations;

namespace AgendaUVV.ViewModels
{
    public class ConsultaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A especialidade é obrigatória.")]
        [StringLength(100)]
        public string Especialidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        [DataType(DataType.DateTime)]
        public DateTime DataHora { get; set; }

        [StringLength(500)]
        public string? Descricao { get; set; }
    }
}