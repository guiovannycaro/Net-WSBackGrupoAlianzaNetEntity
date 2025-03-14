using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSBackGrupoAlianzaNet.Models;
using WSBackGrupoAlianzaNet.Services;

namespace WSBackGrupoAlianzaNet.Controllers
{
    [Route("api/alianza/Seguridad")]
    [ApiController]
    public class SeguridadController : ControllerBase
    {
        private readonly ApplicationDBContext context;

        public SeguridadController(ApplicationDBContext context)
        {
            this.context = context;
        }

        [HttpPost("Autenticacion")]
        public IActionResult Autenticacion(SecurityUDtos u)
        {
            try
            {
                if (string.IsNullOrEmpty(u.email) || string.IsNullOrEmpty(u.password))
                {
                    return BadRequest(new Ansewer
                    {
                        StatusCode = 400,
                        Message = "error",
                        Description = "El correo y la contraseña son obligatorios."
                    });
                }

                var usuario = context.Usuarios.FirstOrDefault(user => user.email == u.email && user.password == u.password);

                if (usuario != null)
                {
                    return Ok(true);
                }
                else
                {
                    return Unauthorized(false);
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"SQLException: {e.Message}");
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = "Error durante la autenticación."
                });
            }
        }

        [HttpGet("GetRol")]
        public IActionResult GetPerfil(string datos)
        {
            try
            {
                var usuario = context.Usuarios.FirstOrDefault(u => u.email == datos);

                if (usuario == null)
                {
                    return NotFound(new Ansewer
                    {
                        StatusCode = 404,
                        Message = "error",
                        Description = "Usuario no encontrado."
                    });
                }

                // Retornar el rol del usuario
                return Ok(usuario);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"SQLException: {e.Message}");
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = "Error al obtener el perfil."
                });
            }
        }

        [HttpGet("ObtNombreUsuario")]
        public IActionResult ObtenerNombreUsuario(string u)
        {
            try
            {
                var usuario = context.Usuarios.FirstOrDefault(user => user.email == u);

                if (usuario == null)
                {
                    return NotFound(new Ansewer
                    {
                        StatusCode = 404,
                        Message = "error",
                        Description = "Usuario no encontrado."
                    });
                }

                return Ok(usuario);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"SQLException: {e.Message}");
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = "Error al obtener el nombre del usuario."
                });
            }
        }

        [HttpPost("ChangePassword")]
        public IActionResult ChangePassword(ChangePasswordDto u)
        {
            try
            {
                if (string.IsNullOrEmpty(u.email) || string.IsNullOrEmpty(u.newPassword))
                {
                    return BadRequest(new Ansewer
                    {
                        StatusCode = 400,
                        Message = "error",
                        Description = "El correo y la nueva contraseña son obligatorios."
                    });
                }

                var usuario = context.Usuarios.FirstOrDefault(user => user.email == u.email);

                if (usuario == null)
                {
                    return NotFound(new Ansewer
                    {
                        StatusCode = 404,
                        Message = "error",
                        Description = "Usuario no encontrado."
                    });
                }

                usuario.password = u.newPassword;
                context.SaveChanges();

                return Ok(new Ansewer
                {
                    StatusCode = 200,
                    Message = "success",
                    Description = "Contraseña actualizada correctamente."
                });
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"SQLException: {e.Message}");
                return StatusCode(500, new Ansewer
                {
                    StatusCode = 500,
                    Message = "error",
                    Description = "Error al actualizar la contraseña."
                });
            }
        }
    }
}
