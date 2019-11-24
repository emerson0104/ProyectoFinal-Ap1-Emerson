using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotell.Entidades
{
    public class Habitaciones
    {
        [Key]
        public int HabitacionId { get; set; }
        [Browsable(false)]
        public string Numero { get; set; }
        public string Tipo { get; set; }
        public string Descripcion { get; set; }
        public int Camas { get; set; }
        [Browsable(false)]
        public decimal Valor { get; set; }
        [Browsable(false)]
        public string Estado { get; set; }

        [Browsable(false)]
        public int UsuarioId { get; set; }
        public Habitaciones()
        {
            HabitacionId = 0;
            Numero = string.Empty;
            Tipo = string.Empty;
            Descripcion = string.Empty;
            Valor = 0;
            Estado = string.Empty;
            Camas = 0;
            UsuarioId = 0;
        }

    }


}