using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaEncode.Entities;

namespace PruebaTecnicaEncode.Repositories
{
    public class UsuarioRepository: IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> GetUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario> Insert(Usuario usuario)
        {
            if (await _context.Usuarios.AnyAsync(x => x.Email == usuario.Email))
            {
                throw new Exception("Ya existe un usuario con ese email");
            }

            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();

            usuario.PasswordTextoPlano = null;
            return usuario;
        }

        public async Task<Usuario> UpdateUsuario(Usuario usuario)
        {
            var usuarioExistente = await _context.Usuarios.FindAsync(usuario.Id);

            if (usuarioExistente == null)
            {
                throw new Exception("No existe usuario con ese Id");
            }

            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Email = usuario.Email;

            if (!string.IsNullOrEmpty(usuario.PasswordTextoPlano))
            {
                usuarioExistente.SetPassword(usuario.PasswordTextoPlano);
            }

            _context.Usuarios.Update(usuarioExistente);

            await _context.SaveChangesAsync();
            return usuarioExistente;
        }

        public async Task<Usuario> Delete(int id)
        {
            var existe = await _context.Usuarios.FindAsync(id);

            if (existe == null)
            {
                throw new Exception("No existe usuario con ese Id");
            }

            _context.Usuarios.Remove(existe);
            await _context.SaveChangesAsync();          
            
            return existe;
        }
    }
}
