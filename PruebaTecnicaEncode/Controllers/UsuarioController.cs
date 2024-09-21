using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaEncode.Entities;
using PruebaTecnicaEncode.Repositories;

namespace PruebaTecnicaEncode.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController: ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet(Name = "ObtenerUsuarios")]
        public async Task<ActionResult<List<Usuario>>> Get()
        {
            return await _usuarioRepository.GetUsuarios();
        }

        [HttpGet("{id:int}", Name = "ObtenerUsuario")]
        public async Task<ActionResult> GetById(int id)
        {
            var usuario = await _usuarioRepository.GetById(id);
            if(usuario == null)
            {
                return NotFound();  
            }

            return Ok(usuario);
        }

        [HttpPost(Name = "CrearUsuario")]
        public async Task<ActionResult> Create([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El usuario no puede ser nulo");
            }

            if (string.IsNullOrEmpty(usuario.PasswordTextoPlano))
            {
                return BadRequest("La contraseña no puede estar vacía");
            }

            usuario.SetPassword(usuario.PasswordTextoPlano);

            try
            {
                var nuevoUsuario = await _usuarioRepository.Insert(usuario);
                return CreatedAtRoute("ObtenerUsuario", new { id = nuevoUsuario.Id }, nuevoUsuario);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al crear el usuario: " + ex.Message);
            }
        }

        [HttpPut("{id:int}", Name = "ActualizarUsuario")]
        public async Task<ActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if(usuario == null)
            {
                return BadRequest("Los datos del usuario no son válidos, por favor ingrese datos válidos. ");
            }

            try
            {
                var usuarioDb = await _usuarioRepository.GetById(id);
                if(usuarioDb == null)
                {
                    return NotFound("No se encuentra el usuario");
                }
                usuario.Id = id;
                var usuarioActualizado = await _usuarioRepository.UpdateUsuario(usuario);
                return Ok(usuarioActualizado);
            }
            catch (Exception ex)
            {
                return BadRequest("No pudimos actualizar el usaurio. " + ex.Message);
            }
        }

        [HttpDelete("{id:int}", Name = "EliminarUsuario")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _usuarioRepository.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("No se pudo eliminar. " + ex.Message);
            }
        }
    }
}
