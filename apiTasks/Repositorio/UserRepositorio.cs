using apiTasks.Data;
using apiTasks.Modelos;
using apiTasks.Modelos.Dtos;
using apiTasks.Repositorio.IRepositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace apiTasks.Repositorio
{
    public class UserRepositorio : IUserRepositorio
    {
        private readonly ApplicationDbContext _bd;
        private string claveSecreta;
        public UserRepositorio(ApplicationDbContext bd, IConfiguration config)
        {
            _bd = bd;
            claveSecreta = config.GetValue<string>("ApiSettings:Secreta");
        }

        public bool ExisteUsuarioId(int idUser)
        {
            return _bd.User.Any(u => u.idUser == idUser);
        }

        public User GetUserId(int idUser)
        {
            return _bd.User.FirstOrDefault(u => u.idUser == idUser);
        }

        public ICollection<User> GetUsers()
        {
            return _bd.User.OrderBy(u => u.nameUser).ToList();
        }

        public bool IsUniqueUser(string identificationUser)
        {
            var usuariobd = _bd.User.FirstOrDefault(u => u.identificationUser == identificationUser);
            if (usuariobd == null)
            {
                return true;
            }

            return false;
        }

        public async Task<User> Registro(UserRegistroDto userRegistroDto)
        {
            var passwordEncriptado = obtenermd5(userRegistroDto.passwordUser);

            User usuario = new User()
            {
                identificationUser = userRegistroDto.identificationUser,
                nameUser = userRegistroDto.nameUser.ToUpper(),
                emailUser = userRegistroDto.emailUser,
                passwordUser = passwordEncriptado,
                createUser = DateTime.Now
            };

            _bd.User.Add(usuario);
            await _bd.SaveChangesAsync();
            usuario.passwordUser = passwordEncriptado;
            return usuario;
        }

        public async Task<UserLoginRespuestaDto> Login(UserLoginDto userLoginDto)
        {
            var passwordEncriptado = obtenermd5(userLoginDto.passwordUser);

            var usuario = _bd.User.FirstOrDefault(
                u => u.identificationUser == userLoginDto.identificationUser 
                && 
                u.passwordUser == passwordEncriptado
                );

            //Validamos si el usuario no existe con la combinación de usuario y contraseña correcta
            if (usuario == null)
            {
                return new UserLoginRespuestaDto()
                {
                    Token = "",
                    Usuario = null
                };
            }

            //Aquí existe el usuario entonces podemos procesar el login
            var manejadorToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuario.identificationUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manejadorToken.CreateToken(tokenDescriptor);

            UserLoginRespuestaDto usuarioLoginRespuestaDto = new UserLoginRespuestaDto()
            {
                Token = manejadorToken.WriteToken(token),
                Usuario = usuario
            };

            return usuarioLoginRespuestaDto;
        }

        //Método para encriptar contraseña con MD5 se usa tanto en el Acceso como en el Registro
        public static string obtenermd5(string valor)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(valor);
            data = x.ComputeHash(data);
            string resp = "";
            for (int i = 0; i < data.Length; i++)
                resp += data[i].ToString("x2").ToLower();
            return resp;
        }

    }
}
