using apiTasks.Modelos;
using apiTasks.Modelos.Dtos;

namespace apiTasks.Repositorio.IRepositorio
{
    public interface ITaskRepositorio
    {
        Modelos.Task GetTask(int idTask);
        ICollection<Modelos.Task> GetTasksUser(int idUser);
        System.Threading.Tasks.Task<Modelos.Task> TaskRegistro(TaskRegistroDto taskRegistroDto);
        bool ExisteTaskId(int idTask);
        bool Guardar();
        System.Threading.Tasks.Task<Modelos.Task> GetByIdAsync(int id);
        System.Threading.Tasks.Task<Modelos.Task> GetTaskByIdAsync(int idTask);
        System.Threading.Tasks.Task UpdateStatusAsync(int taskId, string newStatus);
        System.Threading.Tasks.Task UpdatePriorityAsync(int taskId, string newPriority);
        System.Threading.Tasks.Task<List<Modelos.Task>> GetHighPriorityTasksByUserAsync(int userId);
        System.Threading.Tasks.Task UpdateTaskFieldsAsync(int taskId, TaskFieldsUpdateDto taskFieldsUpdateDto);
        System.Threading.Tasks.Task<List<Modelos.Task>> GetActiveTasksByUserAsync(int userId);
    }
}
