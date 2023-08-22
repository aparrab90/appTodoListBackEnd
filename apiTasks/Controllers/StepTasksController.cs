using apiTasks.Modelos.Dtos;
using apiTasks.Modelos;
using apiTasks.Repositorio.IRepositorio;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace apiTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StepTasksController : ControllerBase
    {
        private readonly IStepTaskRepositorio _stepTaskRepo;
        private readonly ITaskRepositorio _taskRepo;
        protected RespuestAPI _respuestaApi;
        private readonly IMapper _mapper;

        public StepTasksController(IStepTaskRepositorio stepTaskRepo, ITaskRepositorio taskRepo, IMapper mapper)
        {
            _stepTaskRepo = stepTaskRepo;
            _taskRepo = taskRepo;
            _mapper = mapper;
            this._respuestaApi = new();
        }


        [Authorize]
        [HttpGet("{idStepTask:int}", Name = "GetStepTask")]
        [ResponseCache(CacheProfileName = "PorDefecto30Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetStepTask(int idStepTask)
        {
            var itemStepTask = _stepTaskRepo.GetStepTask(idStepTask);

            if (itemStepTask == null)
            {
                return NotFound();
            }

            var itemStepTaskDto = _mapper.Map<StepTaskGetDto>(itemStepTask);
            return Ok(itemStepTaskDto);
        }

        [Authorize]
        [HttpGet("GetStepTasks/{idTask:int}")]
        [ResponseCache(CacheProfileName = "PorDefecto30Segundos")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetStepTasks(int idTask)
        {
            var listaStepTasks = _stepTaskRepo.GetStepTasks(idTask);

            if (listaStepTasks == null)
            {
                return NotFound();
            }

            var itemStepTask = new List<StepTaskGetDto>();

            foreach (var item in listaStepTasks)
            {
                itemStepTask.Add(_mapper.Map<StepTaskGetDto>(item));
            }
            return Ok(itemStepTask);
        }

        [Authorize]
        [HttpGet("GetStepTasksActive/{taskId}")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetActiveStepTasksByTaskId(int taskId)
        {
            var activeStepTasks = await _stepTaskRepo.GetActiveStepTasksByTaskIdAsync(taskId);
            return Ok(activeStepTasks);
        }

        [Authorize]
        [HttpPost("StepTaskRegistro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StepTaskRegistro([FromBody] StepTaskRegistroDto stepTaskRegistroDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (stepTaskRegistroDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_taskRepo.ExisteTaskId(stepTaskRegistroDto.idTask))
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("No existe la Tarea en la que se desea Registrar");
                return BadRequest(_respuestaApi);
            }
            var stepTask = await _stepTaskRepo.StepTaskRegistro(stepTaskRegistroDto);
            if (stepTask == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add($"Algo salió mal guardando el registro{stepTask.nameStepTask}");
                return BadRequest(_respuestaApi);
            }

            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return CreatedAtRoute("GetStepTask", new { idStepTask = stepTask.idStepTask }, stepTask);
        }

        [Authorize]
        [HttpPut("editStatusStepTask/{stepTaskId}")]
        [ProducesResponseType(201, Type = typeof(StepTaskDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStepTaskStatus(int stepTaskId, [FromBody] string statusStepTask)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (statusStepTask == null)
            {
                return BadRequest(ModelState);
            }

            var stepTaskEntity = await _stepTaskRepo.GetStepTaskByIdAsync(stepTaskId);

            if (stepTaskEntity == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("Algo salió mal cambiando es Status del paso de la Tarea");
                return NotFound(_respuestaApi);
            }

            await _stepTaskRepo.UpdateStepTaskStatusAsync(stepTaskId, statusStepTask);
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return Ok(_respuestaApi);
        }


        [Authorize]
        [HttpPut("updateStepTask/{stepTaskId}")]
        [ProducesResponseType(201, Type = typeof(StepTaskDto))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateStepTaskFields(int stepTaskId, [FromBody] StepTaskFieldsUpdateDto stepTaskFieldsUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (stepTaskFieldsUpdateDto == null)
            {
                return BadRequest(ModelState);
            }

            var stepTaskEntity = await _stepTaskRepo.GetStepTaskByIdAsync(stepTaskId);

            if (stepTaskEntity == null)
            {
                _respuestaApi.StatusCode = HttpStatusCode.BadRequest;
                _respuestaApi.IsSuccess = false;
                _respuestaApi.ErrorMessages.Add("Algo salió mal cambiando la Prioridad de la tarea");
                return NotFound(_respuestaApi);
            }

            await _stepTaskRepo.UpdateStepTaskFieldsAsync(stepTaskId, stepTaskFieldsUpdateDto);
            _respuestaApi.StatusCode = HttpStatusCode.OK;
            _respuestaApi.IsSuccess = true;
            return Ok(_respuestaApi);
        }
    }
}
