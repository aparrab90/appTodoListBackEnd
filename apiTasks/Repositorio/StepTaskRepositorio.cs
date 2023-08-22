using apiTasks.Data;
using apiTasks.Modelos;
using apiTasks.Modelos.Dtos;
using apiTasks.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace apiTasks.Repositorio
{
    public class StepTaskRepositorio : IStepTaskRepositorio
    {
        private readonly ApplicationDbContext _bd;
        public StepTaskRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public StepTask GetStepTask(int idStepTask)
        {
            return _bd.StepTask.FirstOrDefault(t => t.idStepTask == idStepTask);
        }

        public ICollection<StepTask> GetStepTasks(int idTask)
        {
            return _bd.StepTask.Include(t => t.Task).Where(t => t.idTask == idTask).ToList();
        }

        public async Task<StepTask> StepTaskRegistro(StepTaskRegistroDto steptaskRegistroDto)
        {
            StepTask stepTask = new StepTask()
            {
                nameStepTask = steptaskRegistroDto.nameStepTask,
                detailStepTask = steptaskRegistroDto.detailStepTask,
                statusStepTask = steptaskRegistroDto.statusStepTask,
                createStepTask = DateTime.Now,
                updateStepTask = steptaskRegistroDto.updateStepTask,
                idTask = steptaskRegistroDto.idTask
            };

            _bd.StepTask.Add(stepTask);
            await _bd.SaveChangesAsync();
            return stepTask;
        }

        public async Task<StepTask> GetByIdAsyncStep(int id)
        {
            return await _bd.StepTask.FindAsync(id);
        }

        public async Task<StepTask> GetStepTaskByIdAsync(int idStepTask)
        {
            return await _bd.StepTask.FindAsync(idStepTask);
        }

        public async System.Threading.Tasks.Task UpdateStepTaskStatusAsync(int idStepTask, string statusStepTask)
        {
            var stepTaskEntity = await _bd.StepTask.FindAsync(idStepTask);

            if (stepTaskEntity != null)
            {
                stepTaskEntity.statusStepTask = statusStepTask;
                stepTaskEntity.updateStepTask = DateTime.Now;

                await _bd.SaveChangesAsync();
            }
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        public async System.Threading.Tasks.Task UpdateStepTaskFieldsAsync(int idStepTask, StepTaskFieldsUpdateDto stepTaskFieldsUpdateDto)
        {
            var stepTaskEntity = await _bd.StepTask.FindAsync(idStepTask);

            if (stepTaskEntity != null)
            {
                stepTaskEntity.nameStepTask = stepTaskFieldsUpdateDto.nameStepTask;
                stepTaskEntity.detailStepTask = stepTaskFieldsUpdateDto.detailStepTask;
                stepTaskEntity.statusStepTask = stepTaskFieldsUpdateDto.statusStepTask;
                stepTaskEntity.updateStepTask = DateTime.Now;

                await _bd.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<List<StepTask>> GetActiveStepTasksByTaskIdAsync(int taskId)
        {
            return await _bd.StepTask
                .Where(stepTask => stepTask.idTask == taskId && stepTask.statusStepTask == "true")
                .OrderByDescending(stepTask => stepTask.updateStepTask)
                .ThenByDescending(stepTask => stepTask.createStepTask)
                .ToListAsync();
        }
    }
}
