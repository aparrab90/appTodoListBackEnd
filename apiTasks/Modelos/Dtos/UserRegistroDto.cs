using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos.Dtos
{
    public class UserRegistroDto
    {
        [Required(ErrorMessage = "La identificación del Usuario es obligatorio")]
        [MaxLength(13, ErrorMessage = "El número máximo de caracteres es de 13!")]
        public string identificationUser { get; set; }

        [Required(ErrorMessage = "El nombre del Usuario es obligatorio")]
        [MaxLength(120, ErrorMessage = "El número máximo de caracteres es de 120!")]
        public string nameUser { get; set; }

        [Required(ErrorMessage = "El mail del Usuario es obligatorio")]
        [MaxLength(120, ErrorMessage = "El número máximo de caracteres es de 120!")]
        public string emailUser { get; set; }

        [Required(ErrorMessage = "El passrowd del Usuario es obligatorio")]
        public string passwordUser { get; set; }

    }
}