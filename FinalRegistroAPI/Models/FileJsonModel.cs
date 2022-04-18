using System.ComponentModel.DataAnnotations;

namespace FinalRegistroAPI.Models
{
    public class FileJsonModel
    {
        [Required]
        public DateTime DiaMesAno { get; set; }

        [Required]
        public string? Categoria { get; set; }

        [Required]
        public double Preco { get; set; }

        public string? Descricao { get; set; }
    }
}
