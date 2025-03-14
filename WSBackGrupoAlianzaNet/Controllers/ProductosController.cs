using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WSBackGrupoAlianzaNet.Models;
using WSBackGrupoAlianzaNet.Services;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class ProductosController : ControllerBase
{
    private readonly ApplicationDBContext context;

    public ProductosController(ApplicationDBContext context)
    {
        this.context = context;
    }

    [HttpGet("ObtenerTodos")]
    public IActionResult GetEvents()
    {
        try
        {
            var events = context.Productos.OrderByDescending(e => e.idproducto).ToList();
            if (events.Count == 0)
            {
                return NotFound(new Ansewer
                {
                    StatusCode = 404,
                    Message = "error",
                    Description = "La base de datos está vacía."
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
                Description = "Error al obtener los productos"
            });
        }
    }

    [HttpGet("obtenerProductosById/{id}")]
    public IActionResult GetProductById(int id)
    {
        try
        {
            var product = context.Productos.Find(id);
            if (product == null)
            {
                return NotFound(new Ansewer
                {
                    StatusCode = 404,
                    Message = "error",
                    Description = "Verifica el ID ingresado."
                });
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Ansewer
            {
                StatusCode = 500,
                Message = "error",
                Description = "Error al obtener el producto"
            });
        }
    }

    [HttpGet("obtenerProductosByNombre/{nombre}")]
    public IActionResult GetProductByName(string nombre)
    {
        try
        {
            var product = context.Productos.FirstOrDefault(u => u.nombre == nombre);
            if (product == null)
            {
                return NotFound(new Ansewer
                {
                    StatusCode = 404,
                    Message = "error",
                    Description = "Asegúrate de que el nombre sea correcto."
                });
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Ansewer
            {
                StatusCode = 500,
                Message = "error",
                Description = "Error al obtener el producto"
            });
        }
    }

    [HttpPost("InsertarProductos")]
    public IActionResult CreateProduct(ProductosDto productosDto)
    {
        try
        {
            var product = new Productos
            {
                nombre = productosDto.nombre,
                descripcion = productosDto.descripcion,
                precio = productosDto.precio,
                stock = productosDto.stock
            };

            context.Productos.Add(product);
            context.SaveChanges();

            return StatusCode(201, new Ansewer
            {
                StatusCode = 201,
                Message = "success",
                Description = "El producto fue registrado exitosamente."
            });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(400, new Ansewer
            {
                StatusCode = 400,
                Message = "error",
                Description = "Error al insertar el producto"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Ansewer
            {
                StatusCode = 500,
                Message = "error",
                Description = "Error interno al insertar el producto"
            });
        }
    }

    [HttpPut("actualizarProductos/{id}")]
    public IActionResult UpdateProduct(int id, ProductosDto productosDto)
    {
        try
        {
            var product = context.Productos.Find(id);
            if (product == null)
            {
                return NotFound(new Ansewer
                {
                    StatusCode = 404,
                    Message = "error",
                    Description = "Verifica el ID ingresado."
                });
            }

            product.nombre = productosDto.nombre;
            product.descripcion = productosDto.descripcion;
            product.precio = productosDto.precio;
            product.stock = productosDto.stock;

            context.SaveChanges();

            return Ok(new Ansewer
            {
                StatusCode = 200,
                Message = "success",
                Description = "El producto fue modificado exitosamente."
            });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(400, new Ansewer
            {
                StatusCode = 400,
                Message = "error",
                Description = "Error al actualizar el producto"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Ansewer
            {
                StatusCode = 500,
                Message = "error",
                Description = "Error interno al actualizar el producto"
            });
        }
    }

    [HttpDelete("eliminarProducto/{id}")]
    public IActionResult DeleteProduct(int id)
    {
        try
        {
            var product = context.Productos.Find(id);
            if (product == null)
            {
                return NotFound(new Ansewer
                {
                    StatusCode = 404,
                    Message = "error",
                    Description = "Verifica el ID ingresado."
                });
            }

            context.Productos.Remove(product);
            context.SaveChanges();

            return Ok(new Ansewer
            {
                StatusCode = 200,
                Message = "success",
                Description = "El producto fue eliminado exitosamente."
            });
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(400, new Ansewer
            {
                StatusCode = 400,
                Message = "error",
                Description = "Error al eliminar el producto"
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new Ansewer
            {
                StatusCode = 500,
                Message = "error",
                Description = "Error interno al eliminar el producto"
            });
        }
    }
}
