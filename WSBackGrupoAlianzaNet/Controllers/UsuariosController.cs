using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using WSBackGrupoAlianzaNet.Models;
using WSBackGrupoAlianzaNet.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WSBackGrupoAlianzaNet.Controllers
{
    [ApiController]
    [Route("api/alianza/Usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UsuariosController(ApplicationDBContext context)
        {
            _context = context;
        }

     
        /// Obtener todos los usuarios.
     
        [HttpGet("ObtenerTodos")]
        public IActionResult GetEvents()
        {
            try
            {
                var events = _context.Usuarios.OrderByDescending(e => e.idusuario).ToList();
                if (events.Count == 0)
                {
                    return NotFound(new Ansewer
                    {
                        StatusCode = 404,
                        Message = "error",
                        Description = "No se encontraron usuarios en la base de datos."
                    });
                }
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = "Error al obtener los usuarios."
                });
            }
        }

     
        /// Obtener un usuario por su ID.
      
        [HttpGet("obtenerUsuariosById/{idusuario}")]
        public IActionResult GetEvents(int idusuario)
        {
            try
            {
                var usuario = _context.Usuarios.Find(idusuario);
                if (usuario == null)
                {
                    return NotFound(new Ansewer
                    {
                        StatusCode = 404,
                        Message = "error",
                        Description = "El usuario no existe en la base de datos."
                    });
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = "Error al obtener el usuario."
                });
            }
        }

    
        /// Obtener un usuario por su nombre.
    
        [HttpGet("obtenerUsuariosByNombre/{nombre}")]
        public IActionResult GetUserByName(string nombre)
        {
            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(u => u.nombre == nombre);
                if (usuario == null)
                {
                    return NotFound(new Ansewer
                    {
                        StatusCode = 404,
                        Message ="error",
                        Description = "No existe un usuario con ese nombre en la base de datos."
                    });
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = "Error al obtener el usuario."
                });
            }
        }

    
        /// Insertar un nuevo usuario.
       
        [HttpPost("InsertarUsuarios")]
        public IActionResult CreateEvents([FromBody] UsuariosDto usuariosDto)
        {
            try
            {
                var usuario = new Usuarios
                {
                    nombre = usuariosDto.nombre,
                    tipodocumento = usuariosDto.tipodocumento,
                    numerodocumento = usuariosDto.numerodocumento,
                    rol = usuariosDto.rol,
                    email = usuariosDto.email,
                    password = usuariosDto.password
                };

                _context.Usuarios.Add(usuario);
                _context.SaveChanges();

                return Ok(new Ansewer
                {
                    StatusCode = 201,
                    Message = "success",
                    Description = "El usuario ha sido creado exitosamente."
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new Ansewer
                {
                    StatusCode = 400,
                    Message = "error",
                    Description = "Error al insertar el usuario."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = ex.Message
                });
            }
        }

  
        /// Actualizar los datos de un usuario.
      
        [HttpPut("actualizarUsuarios/{id}")]
        public IActionResult EditEvent(int id, [FromBody] UsuariosDto usuariosDto)
        {
            try
            {
                var usuario = _context.Usuarios.Find(id);
                if (usuario == null)
                {
                    return NotFound(new Ansewer
                    {
                        StatusCode = 404,
                        Message = "error",
                        Description = "El usuario no existe en la base de datos."
                    });
                }

                usuario.nombre = usuariosDto.nombre;
                usuario.tipodocumento = usuariosDto.tipodocumento;
                usuario.numerodocumento = usuariosDto.numerodocumento;
                usuario.rol = usuariosDto.rol;
                usuario.email = usuariosDto.email;
                usuario.password = usuariosDto.password;

                _context.SaveChanges();

                return Ok(new Ansewer
                {
                    StatusCode = 200,
                    Message = "success",
                    Description = "Los datos del usuario han sido actualizados exitosamente."
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new Ansewer
                {
                    StatusCode = 400,
                    Message = "error",
                    Description = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = "hubo un error y no se pudo realisar la accion"
                });
            }
        }

    
        /// Eliminar un usuario por su ID.
  
        [HttpDelete("eliminarUsuarios/{id}")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                var usuario = _context.Usuarios.Find(id);
                if (usuario == null)
                {
                    return NotFound(new Ansewer
                    {
                        StatusCode = 404,
                        Message = "error",
                        Description = "El usuario no existe en la base de datos."
                    });
                }

                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();

                return Ok(new Ansewer
                {
                    StatusCode = 200,
                    Message = "success",
                    Description = "El usuario ha sido eliminado exitosamente."
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(new Ansewer
                {
                    StatusCode = 400,
                    Message = "error",
                    Description = ex.Message
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = ex.Message
                });
            }
        }
    }
}