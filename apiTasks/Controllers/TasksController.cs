using apiTasks.Modelos;
using apiTasks.Modelos.Dtos;
using apiTasks.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace apiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepositorio _taskRepo;
        private readonly ICategoryRepositorio _ctRepo;
        private readonly IUserRepositorio _usRepo;
        protected RespuestAPI _respuestaApi;
        private readonly IMapper _mapper;

        public TasksController(ITaskRepositorio taskRepo, ICategoryRepositorio ctRepo, IUserRepositorio usRepo, IMapper mapper)
        {
            _taskRepo = taskRepo;
            _ctRepo = ctRepo;
            _usRepo = usRepo;
            _mapper = mapper;
            this._respuestaApi = new();
        }

        [Authorize]
        [HttpGet("{idTask:int}", Name = "GetTask")]
        [ResponseCache(CacheProfileName = "PorDefecto30Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetTask(int idTask)
        {
            var itemTask = _taskRepo.GetTask(idTask);

            if (itemTask == null)
            {
                return NotFound();
            }

            var itemTaskDto = _mapper.Map<TaskGetDto>(itemTask);
            return Ok(itemTaskDto);
        }

        [Authorize]
        [HttpGet("GetTasksUser/{idUser:int}")]
        [ResponseCache(CacheProfileName = "PorDefecto30Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetTasksUser(int idUser)
        {
            var listaTasks = _taskRepo.GetTasksUser(idUser);

            if (listaTasks == null)
            {
                return NotFound();
            }

            var itemTask = new List<TaskGetDto>();

            foreach (var item in listaTasks)
            {
                itemTask.Add(_mapper.Map<TaskGetDto>(item));
            }
            return Ok(itemTask);
        }

        [Authorize]
        [HttpGet("GetHightPriorityTaskUser/{userId}")]
        [ResponseCache(CacheProfileName = "PorDefecto30Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetHighPriorityTasksByUser(int userId)
        {
            var highPriorityTasks = await _taskRepo.GetHighPriorityTasksByUserAsync(userId);
            return Ok(highPriorityTasks);
        }

        [Authorize]
        [HttpGet("GetActiveTaskUser/{userId}")]
        [ResponseCache(CacheProfileName = "PorDefecto30Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetActiveTasksByUser(int userId)
        {
            var activeTasks = await _taskRepo.GetActiveTasksByUserAsync(userId);
            return Ok(activeTasks);
        }

        [Authorize]
        [HttpPost("TaskRegistro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> TaskRegistro([FromBody] TaskRegistroDto taskRegistroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (taskRegistroDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_ctRepo.ExisteCategoriaId(taskRegistroDto.idCategory))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("No existe la Categoría que se desea Registrar");
                return BadRequest(_respuestaApi);
            }
            if (!_usRepo.ExisteUsuarioId(taskRegistroDto.idUser))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("No existe el Usuario que se desea Registrar");
                return BadRequest(_respuestaApi);
            }
            var task = await _taskRepo.TaskRegistro(taskRegistroDto);
            if (task == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add($"Algo salió mal guardando el registro{task.nameTask}");
                return BadRequest(_respuestaApi);
            }

            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return CreatedAtRoute("GetTask", new { idTask = task.idTask }, task);
        }

        [Authorize]
        [HttpPut("editStatusTask/{taskId}")]
        [ProducesResponseType(201, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStatus(int taskId, [FromBody] string newStatus)
        {
            var taskEntity = await _taskRepo.GetByIdAsync(taskId);

            if (taskEntity == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("Algo salió mal cambiando es Status de de la tarea");
                return NotFound(_respuestaApi);
            }

            await _taskRepo.UpdateStatusAsync(taskId, newStatus);
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return Ok(_respuestaApi);
        }

        [Authorize]
        [HttpPut("editPriorityTask/{taskId}")]
        [ProducesResponseType(201, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePriority(int taskId, [FromBody] string newPriority)
        {
            var taskEntity = await _taskRepo.GetByIdAsync(taskId);

            if (taskEntity == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("Algo salió mal cambiando la Prioridad de la tarea");
                return NotFound(_respuestaApi);
            }

            await _taskRepo.UpdatePriorityAsync(taskId, newPriority);
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return Ok(_respuestaApi);
        }

        [Authorize]
        [HttpPut("updateTask/{taskId}")]
        [ProducesResponseType(201, Type = typeof(TaskDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTaskFields(int taskId, [FromBody] TaskFieldsUpdateDto taskFieldsUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (taskFieldsUpdateDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_ctRepo.ExisteCategoriaId(taskFieldsUpdateDto.idCategory))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("No existe la Categoría que se desea Modificar");
                return BadRequest(_respuestaApi);
            }
            if (!_usRepo.ExisteUsuarioId(taskFieldsUpdateDto.idUser))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("No existe el Usuario que se desea Modificar");
                return BadRequest(_respuestaApi);
            }

            var taskEntity = await _taskRepo.GetTaskByIdAsync(taskId);

            if (taskEntity == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("Algo salió mal cambiando la Prioridad de la tarea");
                return NotFound(_respuestaApi);
            }

            await _taskRepo.UpdateTaskFieldsAsync(taskId, taskFieldsUpdateDto);
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return Ok(_respuestaApi);
        }       
    }
}
