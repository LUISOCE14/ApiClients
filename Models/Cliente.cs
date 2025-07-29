// Models/Cliente.cs
using System.ComponentModel.DataAnnotations;

namespace ClientesApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string? Nombre { get; set; }

        [Required]
        [EmailAddress]
        public string? CorreoElectronico { get; set; }

        public string? Telefono { get; set; }
    }
}