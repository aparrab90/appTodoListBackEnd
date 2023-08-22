using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.ConstrainedExecution;

namespace apiTasks.Modelos
{
    public class Task
    {
        [Key]
        public int idTask { get; set; }

        [Required]
        public string nameTask { get; set; }

        public string detailTask { get; set; }

        [Required]
        public string statusTask { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ssZ}", ApplyFormatInEditMode = true)]
        public DateTime createTask { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime limitTask { get; set; }

        public string priorityTask { get; set; }

        [Required]
        public int idUser { get; set; }
        [ForeignKey("idUser")]
        public User User { get; set; }

        [Required]
        public int idCategory { get; set; }
        [ForeignKey("idCategory")]
        public User Category { get; set; }
    }
}
