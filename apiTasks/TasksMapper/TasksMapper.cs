using apiTasks.Modelos;
using apiTasks.Modelos.Dtos;
using AutoMapper;

namespace apiTasks.TasksMapper
{
    public class TasksMapper : Profile
    {
        public TasksMapper() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryGetDto>().ReverseMap();

            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserDatosDto>().ReverseMap();

            CreateMap<Modelos.Task, TaskDto>().ReverseMap();
            CreateMap<Modelos.Task, TaskGetDto>().ReverseMap();
            CreateMap<Modelos.Task, TaskRegistroDto>().ReverseMap();
            CreateMap<Modelos.Task, TaskFieldsUpdateDto>().ReverseMap();

            CreateMap<StepTask, StepTaskDto>().ReverseMap();
            CreateMap<StepTask, StepTaskGetDto>().ReverseMap();
            CreateMap<StepTask, StepTaskRegistroDto>().ReverseMap();
            CreateMap<StepTask, StepTaskFieldsUpdateDto>().ReverseMap();
        }
    }
}
