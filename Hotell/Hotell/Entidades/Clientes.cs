using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotell.Entidades
{
    public class Clientes
    {
        [Key]
        public int ClienteId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Celular { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; }
        [Browsable(false)]
        public int UsuarioId { get; set; }
        [Browsable(false)]
        public Clientes()
        {
            ClienteId = 0;
            Nombres = string.Empty;
            Apellidos = string.Empty;
            Celular = string.Empty;
            Email = string.Empty;
            Direccion = string.Empty;
            UsuarioId = 0;
        }
    }
}