using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos
{
    public class StepTask
    {
        [Key]
        public int idStepTask { get; set; }

        [Required]
        public string nameStepTask { get; set; }

        public string detailStepTask { get; set; }

        [Required]
        public string statusStepTask { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ssZ}", ApplyFormatInEditMode = true)]
        public DateTime createStepTask { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm:ssZ}", ApplyFormatInEditMode = true)]
        public DateTime? updateStepTask { get; set; }

        [Required]
        public int idTask { get; set; }
        [ForeignKey("idTask")]
        public Modelos.Task Task { get; set; }
    }
}
