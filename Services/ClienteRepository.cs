// Services/ClienteRepository.cs
using ClientesApi.Models;
using System.Collections.Concurrent;

namespace ClientesApi.Services
{
    // Esta clase simula el acceso a una base de datos usando una colección en memoria.
    public class ClienteRepository
    {
        // Usamos ConcurrentDictionary para que sea seguro en entornos con múltiples hilos (multi-threading).
        private static readonly ConcurrentDictionary<int, Cliente> _clientes = new();
        private static int _nextId = 0;

        public ClienteRepository()
        {
            // Constructor para inicializar con datos de ejemplo si la lista está vacía.
            if (_clientes.IsEmpty)
            {
                Add(new Cliente { Nombre = "Ana García", CorreoElectronico = "ana.garcia@example.com", Telefono = "600111222" });
                Add(new Cliente { Nombre = "Carlos Rodríguez", CorreoElectronico = "c.rodriguez@workplace.net", Telefono = "611222333" });
                Add(new Cliente { Nombre = "María Hernández", CorreoElectronico = "maria.h@email.org", Telefono = "622333444" });
                Add(new Cliente { Nombre = "José Martínez", CorreoElectronico = "jose.martinez@mail.com", Telefono = "633444555" });
                Add(new Cliente { Nombre = "Laura Gómez", CorreoElectronico = "laura.gomez@company.es", Telefono = "644555666" });
                Add(new Cliente { Nombre = "David Pérez", CorreoElectronico = "david.perez@inbox.com", Telefono = "655666777" });
                Add(new Cliente { Nombre = "Sofía Sánchez", CorreoElectronico = "sofia.sanchez@provider.net", Telefono = "666777888" });
                Add(new Cliente { Nombre = "Javier Romero", CorreoElectronico = "j.romero@domain.org", Telefono = "677888999" });
                Add(new Cliente { Nombre = "Elena Torres", CorreoElectronico = "elena.torres@email.es", Telefono = "688999000" });
                Add(new Cliente { Nombre = "Miguel Navarro", CorreoElectronico = "miguel.navarro@mail.net", Telefono = "699000111" });
            }
        }

        public IEnumerable<Cliente> GetAll() => _clientes.Values.ToList();

        public Cliente? GetById(int id)
        {
            _clientes.TryGetValue(id, out var cliente);
            return cliente;
        }

        public Cliente Add(Cliente cliente)
        {
            // Interlocked.Increment es una forma segura de aumentar el contador en un entorno de múltiples hilos.
            int newId = Interlocked.Increment(ref _nextId);
            cliente.Id = newId;
            _clientes[cliente.Id] = cliente;
            return cliente;
        }

        public bool Update(Cliente cliente)
        {
            if (_clientes.ContainsKey(cliente.Id))
            {
                _clientes[cliente.Id] = cliente;
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            return _clientes.TryRemove(id, out _);
        }
    }
}