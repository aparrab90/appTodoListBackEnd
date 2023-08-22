using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos.Dtos
{
    public class StepTaskRegistroDto
    {
        [Required(ErrorMessage = "El nombre del Paso es Obligatorio")]
        [MaxLength(120, ErrorMessage = "El número máximo de caracteres es de 120!")]
        public string nameStepTask { get; set; }

        public string detailStepTask { get; set; }

        [Required(ErrorMessage = "El Status del Paso es Obligatorio")]
        [MaxLength(5, ErrorMessage = "El número máximo de caracteres es de 5!")]
        public string statusStepTask { get; set; }

        public DateTime? updateStepTask { get; set; }

        [Required(ErrorMessage = "EL código de la Tarea es Obligatoria")]
        public int idTask { get; set; }
    }
}
