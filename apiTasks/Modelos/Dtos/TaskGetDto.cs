using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiTasks.Modelos.Dtos
{
    public class TaskGetDto
    {
        public int idTask { get; set; }
        public string nameTask { get; set; }
        public string detailTask { get; set; }
        public string statusTask { get; set; }
        public DateTime createTask { get; set; }
        public DateTime limitTask { get; set; }
        public string priorityTask { get; set; }
        public int idUser { get; set; }
        public int idCategory { get; set; }
    }
}
