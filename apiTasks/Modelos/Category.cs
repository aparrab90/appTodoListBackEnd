using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos
{
    public class Category
    {
        [Key]
        public int idCategory { get; set; }
        
        [Required]
        public string nameCategory { get; set; }

        [Required]
        public DateTime createCategory { get; set; }
    }
}