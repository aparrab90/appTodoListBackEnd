using apiTasks.Modelos;

namespace apiTasks.Repositorio.IRepositorio
{
    public interface ICategoryRepositorio
    {
        ICollection<Category> GetCategorias();
        Category GetCategoria(int idCategory);
        bool ExisteCategoriaNombre(string nameCategory);
        bool ExisteCategoriaId(int idCategory);
    }
}
