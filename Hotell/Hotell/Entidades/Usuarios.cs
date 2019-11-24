using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotell.Entidades
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        [Browsable(false)]
        public string Clave { get; set; }
        public string NivelAcceso { get; set; }
        public string Celular { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public DateTime FechaCreacion { get; set; }
        public decimal TotalVentas { get; set; }

        public Usuarios()
        {
            UsuarioId = 0;
            Usuario = string.Empty;
            Clave = string.Empty;
            Nombres = string.Empty;
            Apellidos = string.Empty;
            Email = string.Empty;
            FechaCreacion = DateTime.Now;
            NivelAcceso = string.Empty;
            TotalVentas = 0;
        }
    }
}