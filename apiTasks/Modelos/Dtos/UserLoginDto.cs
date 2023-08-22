using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos.Dtos
{
    public class UserLoginDto
    {
        [Required(ErrorMessage = "La identificación del Usuario es obligatorio")]
        [MaxLength(13, ErrorMessage = "El número máximo de caracteres es de 13!")]
        public string identificationUser { get; set; }

        [Required(ErrorMessage = "El passrowd del Usuario es obligatorio")]
        public string passwordUser { get; set; }
    }
}
