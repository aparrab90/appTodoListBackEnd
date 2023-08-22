using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos.Dtos
{
    public class UserDto
    {
        public int idUser { get; set; }

        [Required(ErrorMessage = "La identificación es obligatoria")]
        [MaxLength(13, ErrorMessage = "El número máximo de caracteres es de 13!")]
        public string identificationUser { get; set; }

        [MaxLength(120, ErrorMessage = "El número máximo de caracteres es de 120!")]
        public string nameUser { get; set; }

        [MaxLength(120, ErrorMessage = "El número máximo de caracteres es de 120!")]
        public string emailUser { get; set; }

        [Required(ErrorMessage = "El password es obligatorio")]
        public string passwordUser { get; set; }

        [Required(ErrorMessage = "La fecha de creación es obligatoria")]
        public DateTime createUser { get; set; }
    }
}
