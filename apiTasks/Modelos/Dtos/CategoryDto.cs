using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos.Dtos
{
    public class CategoryDto
    {
        public int idCategory { get; set; }

        [Required(ErrorMessage = "El nombre de la categoría es obligatoria")]
        [MaxLength(120, ErrorMessage = "El número máximo de caracteres es de 120!")]
        public string nameCategory { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        public DateTime createCategory { get; set; }
    }
}
