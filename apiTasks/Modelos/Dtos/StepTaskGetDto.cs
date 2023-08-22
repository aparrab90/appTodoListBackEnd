using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos.Dtos
{
    public class StepTaskGetDto
    {
        public int idStepTask { get; set; }
        public string nameStepTask { get; set; }
        public string detailStepTask { get; set; }
        public string statusStepTask { get; set; }
        public DateTime createStepTask { get; set; }
        public DateTime? updateStepTask { get; set; }
        public int idTask { get; set; }
    }
}
