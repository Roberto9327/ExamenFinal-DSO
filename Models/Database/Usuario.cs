using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace inventario.Models.Database
{
    public class Usuario
    {
           [Key]
        public int Id { get; set; }
        public string Correo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Estado { get; set; }
    }
}
