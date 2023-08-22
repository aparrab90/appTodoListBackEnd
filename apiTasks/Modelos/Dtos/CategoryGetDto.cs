using System.ComponentModel.DataAnnotations;

namespace apiTasks.Modelos.Dtos
{
    public class CategoryGetDto
    {
        public int idCategory { get; set; }
        public string nameCategory { get; set; }
    }
}
