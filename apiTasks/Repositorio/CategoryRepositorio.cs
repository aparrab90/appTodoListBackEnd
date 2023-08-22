using apiTasks.Data;
using apiTasks.Modelos;
using apiTasks.Repositorio.IRepositorio;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace apiTasks.Repositorio
{
    public class CategoryRepositorio : ICategoryRepositorio
    {
        private readonly ApplicationDbContext _bd;

        public CategoryRepositorio(ApplicationDbContext bd)
        {
            _bd = bd;
        }

        public ICollection<Category> GetCategorias()
        {
            return _bd.Category.OrderBy(c => c.idCategory).ToList();
        }

        public Category GetCategoria(int idCategory)
        {
            return _bd.Category.FirstOrDefault(c => c.idCategory == idCategory);
        }

        public bool ExisteCategoriaId(int idCategory)
        {
            return _bd.Category.Any(c => c.idCategory == idCategory);
        }

        public bool ExisteCategoriaNombre(string nameCategory)
        {
            bool valor = _bd.Category.Any(c => c.nameCategory.ToLower().Trim() == nameCategory.ToLower().Trim());
            return valor;
        }
    }
}
