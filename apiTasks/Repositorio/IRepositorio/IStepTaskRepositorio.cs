using apiTasks.Modelos;
using apiTasks.Modelos.Dtos;

namespace apiTasks.Repositorio.IRepositorio
{
    public interface IStepTaskRepositorio
    {
        StepTask GetStepTask(int idStepTask);
        ICollection<Modelos.StepTask> GetStepTasks(int idTask);
        System.Threading.Tasks.Task<StepTask> StepTaskRegistro(StepTaskRegistroDto steptaskRegistroDto);
        System.Threading.Tasks.Task<StepTask> GetByIdAsyncStep(int id);
        System.Threading.Tasks.Task<StepTask> GetStepTaskByIdAsync(int idStepTask);
        System.Threading.Tasks.Task UpdateStepTaskStatusAsync(int idStepTask, string statusStepTask);
        bool Guardar();
        System.Threading.Tasks.Task UpdateStepTaskFieldsAsync(int stepTaskId, StepTaskFieldsUpdateDto stepTaskFieldsUpdateDto);
        System.Threading.Tasks.Task<List<StepTask>> GetActiveStepTasksByTaskIdAsync(int taskId);

        
        

    }
}
