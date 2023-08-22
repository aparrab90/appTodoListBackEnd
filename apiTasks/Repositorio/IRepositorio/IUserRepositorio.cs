using apiTasks.Modelos;
using apiTasks.Modelos.Dtos;

namespace apiTasks.Repositorio.IRepositorio
{
    public interface IUserRepositorio
    {
        ICollection<User> GetUsers();
        User GetUserId(int idUser);
        bool IsUniqueUser(string identificationUser);
        Task<UserLoginRespuestaDto> Login(UserLoginDto userLoginDto);
        Task<User> Registro(UserRegistroDto userRegistroDto);
        bool ExisteUsuarioId(int idUser);
    }
}
