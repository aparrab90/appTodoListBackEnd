using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiTasks.Modelos.Dtos
{
    public class TaskRegistroDto
    {
        [Required(ErrorMessage = "El nombre de la Tarea es obligatoria")]
        [MaxLength(120, ErrorMessage = "El número máximo de caracteres es de 120!")]
        public string nameTask { get; set; }

        public string detailTask { get; set; }

        [Required(ErrorMessage = "El status de la tarea es obligatoria")]
        [MaxLength(5, ErrorMessage = "El número máximo de caracteres es de 5!")]
        public string statusTask { get; set; }

        public DateTime limitTask { get; set; }

        [Required(ErrorMessage = "La prioridad de la tarea es obligatoria")]
        [MaxLength(5, ErrorMessage = "El número máximo de caracteres es de 5!")]
        public string priorityTask { get; set; }

        [Required(ErrorMessage = "EL id de Usuario es obligatorio")]
        public int idUser { get; set; }

        [Required(ErrorMessage = "El id de la categoría es obligatoria")]
        public int idCategory { get; set; }
    }
}
