using PruebaTecnicaEncode.Entities;

namespace PruebaTecnicaEncode.Repositories
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> Insert(Usuario usuario);
        Task<Usuario?> GetById(int id);
        Task<Usuario?> UpdateUsuario(Usuario usuario);
        Task<Usuario?> Delete(int id);
    }
}
