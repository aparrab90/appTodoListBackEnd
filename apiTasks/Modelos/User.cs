using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos
{
    public class User
    {
        [Key]
        public int idUser { get; set; }

        [Required]
        public string identificationUser { get; set; }

        public string nameUser { get; set; }
        public string emailUser { get; set; }
        
        [Required]
        public string passwordUser { get; set; }
        
        [Required]
        public DateTime createUser { get; set; }
    }
}
