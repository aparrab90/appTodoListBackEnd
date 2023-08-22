namespace apiTasks.Modelos.Dtos
{
    public class TaskFieldsUpdateDto
    {
        public string nameTask { get; set; }
        public string detailTask { get; set; }
        public string statusTask { get; set; }
        public DateTime limitTask { get; set; }
        public string priorityTask { get; set; }
        public int idUser { get; set; }
        public int idCategory { get; set; }
    }
}
