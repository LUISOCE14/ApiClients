// Controllers/ClientesController.cs
using ClientesApi.Models;
using ClientesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClientesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteRepository _repository;

        // Inyectamos nuestro repositorio en memoria
        public ClientesController(ClienteRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Cliente>> GetClientes()
        {
            return Ok(_repository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Cliente> GetCliente(int id)
        {
            var cliente = _repository.GetById(id);
            if (cliente == null)
            {
                return NotFound(); // 404
            }
            return Ok(cliente);
        }

        [HttpPost]
        public ActionResult<Cliente> PostCliente(Cliente cliente)
        {
            var nuevoCliente = _repository.Add(cliente);
            return CreatedAtAction(nameof(GetCliente), new { id = nuevoCliente.Id }, nuevoCliente); // 201
        }

        [HttpPut("{id}")]
        public IActionResult PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return BadRequest(); // 400
            }
            if (!_repository.Update(cliente))
            {
                return NotFound(); // 404
            }
            return NoContent(); // 204
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            if (!_repository.Delete(id))
            {
                return NotFound(); // 404
            }
            return NoContent(); // 204
        }
    }
}