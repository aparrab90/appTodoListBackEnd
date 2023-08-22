using apiTasks.Data;
using apiTasks.Modelos;
using apiTasks.Modelos.Dtos;
using apiTasks.Repositorio.IRepositorio;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace apiTasks.Repositorio
{
    public class TaskRepositorio : ITaskRepositorio
    {
        private readonly ApplicationDbContext _bd;
        public TaskRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public ICollection<Modelos.Task> GetTasksUser(int idUser)
        {
            return _bd.Task.Include(us => us.User).Where(us => us.idUser == idUser).ToList();
        }

        public Modelos.Task GetTask(int idTask)
        {
            return _bd.Task.FirstOrDefault(t => t.idTask == idTask);
        }

        public async Task<Modelos.Task> TaskRegistro(TaskRegistroDto taskRegistroDto)
        {
            Modelos.Task task = new Modelos.Task()
            {
                nameTask = taskRegistroDto.nameTask,
                detailTask = taskRegistroDto.detailTask,
                statusTask = taskRegistroDto.statusTask,
                createTask = DateTime.Now,
                limitTask = taskRegistroDto.limitTask,
                priorityTask = taskRegistroDto.priorityTask,
                idUser = taskRegistroDto.idUser,
                idCategory = taskRegistroDto.idCategory
            };

            _bd.Task.Add(task);
            await _bd.SaveChangesAsync();
            return task;
        }

        public bool Guardar()
        {
            return _bd.SaveChanges() >= 0 ? true : false;
        }

        public bool ExisteTaskId(int idTask)
        {
            return _bd.Task.Any(t => t.idTask == idTask);
        }

        public async Task<Modelos.Task> GetByIdAsync(int id)
        {
            return await _bd.Task.FindAsync(id);
        }

        public async Task<Modelos.Task> GetTaskByIdAsync(int idTask)
        {
            return await _bd.Task.FindAsync(idTask);
        }

        public async System.Threading.Tasks.Task UpdateStatusAsync(int taskId, string newStatus)
        {
            var taskEntity = await _bd.Task.FindAsync(taskId);

            if (taskEntity != null)
            {
                if (newStatus == "false")
                {
                    taskEntity.statusTask = "false";

                    // Update related StepTasks
                    var relatedStepTasks = _bd.StepTask
                        .Where(st => st.idTask == taskId)
                        .ToList();

                    foreach (var stepTask in relatedStepTasks)
                    {
                        stepTask.statusStepTask = "false";
                        stepTask.updateStepTask = DateTime.Now;
                    }
                }
                else if (newStatus == "true")
                {
                    taskEntity.statusTask = "true";
                }

                await _bd.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task UpdatePriorityAsync(int taskId, string newPriority)
        {
            var taskEntity = await _bd.Task.FindAsync(taskId);

            if (taskEntity != null)
            {
                taskEntity.priorityTask = newPriority;
                await _bd.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<List<Modelos.Task>> GetHighPriorityTasksByUserAsync(int userId)
        {
            return await _bd.Task
                .Where(task => task.idUser == userId && task.priorityTask == "true" && task.statusTask == "true")
                .OrderByDescending(task => task.createTask)
                .ToListAsync();
        }

        public async System.Threading.Tasks.Task UpdateTaskFieldsAsync(int idTask, TaskFieldsUpdateDto taskFieldsUpdateDto)
        {
            var taskEntity = await _bd.Task.FindAsync(idTask);

            if (taskEntity != null)
            {
                taskEntity.nameTask = taskFieldsUpdateDto.nameTask;
                taskEntity.detailTask = taskFieldsUpdateDto.detailTask;
                taskEntity.statusTask = taskFieldsUpdateDto.statusTask;
                taskEntity.limitTask = taskFieldsUpdateDto.limitTask;
                taskEntity.priorityTask = taskFieldsUpdateDto.priorityTask;
                taskEntity.idUser = taskFieldsUpdateDto.idUser;
                taskEntity.idCategory = taskFieldsUpdateDto.idCategory;

                await _bd.SaveChangesAsync();
            }
        }

        public async System.Threading.Tasks.Task<List<Modelos.Task>> GetActiveTasksByUserAsync(int userId)
        {
            return await _bd.Task
                .Where(task => task.idUser == userId && task.statusTask == "true")
                .OrderByDescending(task => task.createTask)
                .ToListAsync();
        }
    }
}
